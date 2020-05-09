using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public struct LevelInfo
{
    public int Id;
    public bool Completed;
    public float3 Pos;
}

public struct NeighbourInfo
{
    public NeighbourInfo(MapLevel.MoveDirection dir)
    {
        Direction = dir;
        Offset = MapLevel.GetMoveDirOffset(dir);
    }

    public MapLevel.MoveDirection Direction;
    public int2 Offset;
}

public class LevelManager : MonoBehaviour
{
    public static readonly int GridSize = 3;

    [Header("Prefabs")]
    public GameObject MapPrefab;

    public GameObject[] BotPrefabs;

    [Header("Refs")]
    public LevelCamera MainCamera;

    public PlayerController Player;

    [Header("Map Settings")]
    public float MapXOffset = 14.5f;

    public float MapYOffset = -8f;

    public int2 PlayerStart = new int2(1, 1);

    [Header("Win Condition")]
    public float CoverageWinPerc = 0.5f;

    public enum State
    {
        Init,
        Play,
        Travel
    }

    public State GameState { get; private set; } = State.Init;
    public GameObject Map { get; private set; }
    public PaintableSurfaceTexture MapSurface { get; private set; }
    public MapLevel MapLevel { get; private set; }

    public LevelInfo GetCurrentLevel()
    {
        return levelGrid[playerPos.x, playerPos.y];
    }

    public float PlayerCoverage { get; private set; }
    public float PlayerWinPerc => PlayerCoverage / CoverageWinPerc;

    private LevelInfo[,] levelGrid = new LevelInfo[GridSize, GridSize];

    private int2 playerPos = new int2(1, 1);
    private int levelsCompleted = 0;
    private int2 playerMove = int2.zero;
    private List<GameObject> spawnedBots = new List<GameObject>();

    private void PopulateGrid()
    {
        for (int i = 0; i < GridSize * GridSize; i++)
        {
            int x = i % GridSize;
            int y = GetY(i);

            levelGrid[x, y] = new LevelInfo
            {
                Id = i,
                Completed = false,
                Pos = MapPositionForId(i)
            };
        }
    }

    private void AvailableNeighbourLevels(out List<MapLevel.MoveDirection> available)
    {
        NeighbourInfo[] neighbourOffsets = new NeighbourInfo[]
        {
           new NeighbourInfo(MapLevel.MoveDirection.Up),
           new NeighbourInfo(MapLevel.MoveDirection.Right),
           new NeighbourInfo(MapLevel.MoveDirection.Down),
           new NeighbourInfo(MapLevel.MoveDirection.Left)
        };

        available = new List<MapLevel.MoveDirection>();
        foreach (var info in neighbourOffsets)
        {
            var neighbourPos = playerPos + info.Offset;
            // Inbounds
            if ((neighbourPos.x < GridSize && neighbourPos.x >= 0) && (neighbourPos.y < GridSize && neighbourPos.y >= 0))
            {
                if (levelGrid[neighbourPos.x, neighbourPos.y].Completed) continue;
                available.Add(info.Direction);
            }
        }
    }

    private float3 MapPositionForId(int id)
    {
        return new float3((id % 3) * MapXOffset, GetY(id) * MapYOffset, 0);
    }

    // TODO: calc this in a better way
    private int GetY(int i)
    {
        if (i > 2 && i <= 5)
        {
            return 1;
        }
        else if (i > 5 && i <= 8)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

    public void Awake()
    {
        playerPos = PlayerStart;
        Map = Instantiate(MapPrefab, MapPositionForId(0), quaternion.identity, transform);
        MapSurface = Map.GetComponentInChildren<PaintableSurfaceTexture>();
        MapLevel = Map.GetComponent<MapLevel>();

        MapLevel.Init();
        MapLevel.ArrowEvent.AddListener(PlayerTravelInDirection);

        MainCamera.Target = Map.transform;

        PopulateGrid();

        Game.Find().UIManager.SetCurrentLevel(levelGrid[playerPos.x, playerPos.y].Id);

        StartPlayState();
    }

    public void PlayerTravelInDirection(MapLevel.MoveDirection dir)
    {
        if (GameState == State.Travel)
        {
            playerMove = MapLevel.GetMoveDirOffset(dir);

            MoveToNextLevel();

            StartPlayState();
        }
    }

    // Update is called once per frame
    public void Update()
    {
        switch (GameState)
        {
            case State.Init:
                break;

            case State.Play:

                if (Player == null)
                {
                    GameOver(false);
                }

                foreach (var bot in spawnedBots)
                {
                    if (bot == null) continue;

                    var other = bot.GetComponentInChildren<FruitEntity>();

                    if (other == null || other.Squishes == null || Player == null
                        || Player.FruitEntity == null || Player.FruitEntity.Squishes == null)
                        continue;

                    if (Player.FruitEntity.Squishes.Eatable(other))
                    {
                        other.DisplaySquishIndicator(false);
                    }
                    else if (other.Squishes.Eatable(Player.FruitEntity))
                    {
                        other.DisplaySquishIndicator(true);
                    }
                    else
                    {
                        other.HideSquishIndicator();
                    }
                }

                PlayerCoverage = MapSurface.PaintCoverage(Player.FruitEntity.Color);
                // Player has enough coverage
                if (PlayerCoverage >= CoverageWinPerc)
                {
                    KillAllBots();

                    // Complete Level
                    var levelInfo = levelGrid[playerPos.x, playerPos.y];
                    levelGrid[playerPos.x, playerPos.y].Completed = true;
                    levelsCompleted++;

                    Game.Find().UIManager.SetLevelCompleted(levelInfo.Id);

                    if (levelsCompleted == GridSize * GridSize)
                    {
                        GameOver(true);
                    }

                    Player.FruitEntity.transform.position = levelInfo.Pos;

                    StartTravelState();
                }
                break;

            case State.Travel:
                PlayerCoverage = MapSurface.PaintCoverage(Player.FruitEntity.Color);
                break;
        }
    }

    public void StartTravelState()
    {
        GameState = State.Travel;
        AvailableNeighbourLevels(out var available);
        foreach (var dir in available)
        {
            MapLevel.EnableArrow(dir);
        }
    }

    private void MoveToNextLevel()
    {
        playerPos += playerMove;

        Game.Find().UIManager.SetCurrentLevel(levelGrid[playerPos.x, playerPos.y].Id);

        playerMove = int2.zero;
    }

    private void StartPlayState()
    {
        GameState = State.Init;

        MainCamera.ZoomInOut();

        // TODO: remove shake? MainCamera.ShakeCamera(1.1f, 1f);

        ResetMap();
        StartCoroutine(SpawnBots());

        GameState = State.Play;
    }

    private void ResetMap()
    {
        MapLevel.ResetArrows();

        var levelInfo = levelGrid[playerPos.x, playerPos.y];

        // Set new positions
        Map.transform.position = levelInfo.Pos;
        Player.FruitEntity.transform.position = levelInfo.Pos;

        MapSurface.ClearSurface();
    }

    private IEnumerator SpawnBots()
    {
        yield return new WaitForSeconds(1.0f);

        var bots = UnityEngine.Random.Range(3, 5);
        var levelInfo = levelGrid[playerPos.x, playerPos.y];

        for (int i = 0; i < bots; i++)
        {
            var idx = UnityEngine.Random.Range(0, BotPrefabs.Length);

            Vector3 offset = new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), UnityEngine.Random.Range(-1.5f, 1.5f), 0);
            var pos = ((Vector3)levelInfo.Pos + offset) + UnityEngine.Random.insideUnitSphere * 3f;

            spawnedBots.Add(Instantiate(BotPrefabs[idx], pos, quaternion.identity, transform) as GameObject);
        }
    }

    private void KillAllBots()
    {
        // TODO: Kill all bots
        for (int i = 0; i < spawnedBots.Count; i++)
        {
            if (spawnedBots[i] != null)
            {
                spawnedBots[i].SetActive(false);
                GameObject.Destroy(spawnedBots[i]);
            }
        }
        spawnedBots.Clear();
    }

    private void GameOver(bool success)
    {
        Debug.Log("GameOver with status " + success);
        if (success)
        {
            GoToVictoryScene();
        }
        else
        {
            GoToGameOverScene();
        }
    }

    private void GoToVictoryScene()
    {
        transform.Find("ChangeToVictory").GetComponent<ChangeScene>().LoadScene();
    }

    private void GoToGameOverScene()
    {
        transform.Find("ChangeToGameOver").GetComponent<ChangeScene>().LoadScene();
    }
}
