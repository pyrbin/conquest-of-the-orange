using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class RandomController : MonoBehaviour
{
    public FruitEntity FruitEntity { get; private set; }

    [HideInInspector]
    public Vector3 TargetPos { get; private set; }

    private LevelManager levelManager;

    // Start is called before the first frame update
    private void Awake()
    {
        TargetPos = getNewPosition();
        FruitEntity = GetComponentInChildren<FruitEntity>();
    }

    // Update is called once per frame
    private void Update()
    {
        if ((FruitEntity.transform.position - TargetPos).magnitude < 5)
            TargetPos = getNewPosition();
        FruitEntity.Movement.MoveTo(TargetPos);
    }

    private Vector2 getNewPosition() {
        float x = Random.Range(levelXBounds().x, levelXBounds().y);
        float y = Random.Range(levelYBounds().x, levelYBounds().y);
        return new Vector2(x, y);
    }

    private float2 levelXBounds()
    {
        LevelInfo levelInfo = Game.Find().LevelManager.GetCurrentLevel();
        float width = 8.5f;
        return new Vector2((levelInfo.Pos.x - width), (levelInfo.Pos.x + width));
    }

    private float2 levelYBounds()
    {
        LevelInfo levelInfo = Game.Find().LevelManager.GetCurrentLevel();
        float height = 4.6f;
        return new Vector2((levelInfo.Pos.y - height), (levelInfo.Pos.y + height));
    }

}
