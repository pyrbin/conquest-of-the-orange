using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CoverageIndicator CoverageIndicator;
    public Transform Grid;

    private LevelManager levelManager;

    public void SetCurrentLevel(int id)
    {
        foreach (var child in Grid.GetComponentsInChildren<LevelGridItem>())
        {
            child.SetActive(child.Id == id);
        }
    }

    public void SetLevelCompleted(int id)
    {
        foreach (var child in Grid.GetComponentsInChildren<LevelGridItem>())
        {
            if (child.Id == id)
            {
                child.SetCompleteColor(levelManager.Player.FruitEntity.Color);
                child.SetComplete();
                return;
            }
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        levelManager = Game.Find().LevelManager;
    }

    // Update is called once per frame
    private void Update()
    {
        CoverageIndicator.SetFillColor(levelManager.Player.FruitEntity.Color);
        CoverageIndicator.SetPercentage(levelManager.PlayerWinPerc);
    }
}
