using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class PredeterminedController : MonoBehaviour
{
    public FruitEntity FruitEntity { get; private set; }
    public float GoalDistance = 1f;
    public List<Vector2> Points;

    [HideInInspector]
    public Vector3 TargetPos { get; private set; }

    private int index = 0;
    private float height = 4.6f;
    private float width = 8.5f;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Points == null || Points.Count == 0)
        {
            Points.Add(new Vector2(-8, -4));
            Points.Add(new Vector2(8, -4));
            Points.Add(new Vector2(8, 4));
            Points.Add(new Vector2(-8, 4));
            Points.Add(new Vector2(0, 0));
        }

        foreach (Vector2 point in Points)
        {
            if (math.abs(point.x) > width)
                print("Absolute value of x must be less than " + width);
            if (math.abs(point.y) > width)
                print("Absolute value of y must be less than " + height);
        }

        index = Random.Range(0, Points.Count);
        TargetPos = getNewPosition();
        FruitEntity = GetComponentInChildren<FruitEntity>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (FruitEntity == null)
        {
            FruitEntity = GetComponentInChildren<FruitEntity>();

            return;
        }

        if ((FruitEntity.transform.position - TargetPos).magnitude < GoalDistance)
            TargetPos = getNewPosition();

        if (FruitEntity.Squishes.Eatable(Game.Find().LevelManager.Player.FruitEntity))
        {
            FruitEntity.Movement.MoveTo(Game.Find().LevelManager.Player.FruitEntity.transform.position);
        }

        FruitEntity.Movement.MoveTo(TargetPos);
    }

    private Vector2 getNewPosition()
    {
        LevelInfo levelInfo = Game.Find().LevelManager.GetCurrentLevel();
        index++;
        Vector2 relativePoint = Points[index % Points.Count];
        Vector2 point = new Vector2(relativePoint.x + levelInfo.Pos.x, relativePoint.y + levelInfo.Pos.y);
        return point;
    }
}
