using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class PredeterminedController : MonoBehaviour
{
    public FruitEntity FruitEntity { get; private set; }
    public List<Vector2> points;

    [HideInInspector]
    public Vector3 TargetPos { get; private set; }

    private int index = 0;
    private float height = 4.6f;
    private float width = 8.5f;

    // Start is called before the first frame update
    private void Awake()
    {
;

        if (points == null || points.Count == 0)
        {
            print("lmao");
            points.Add(new Vector2(-8, -4));
            points.Add(new Vector2(8, -4));
            points.Add(new Vector2(8, 4));
            points.Add(new Vector2(-8, 4));
            points.Add(new Vector2(0, 0));
        }
   
        foreach (Vector2 point in points)
        {
            if (math.abs(point.x) > width)
                print("Absolute value of x must be less than " + width);
            if (math.abs(point.y) > width)
                print("Absolute value of y must be less than " + height);
        }

        TargetPos = getNewPosition();
        FruitEntity = GetComponentInChildren<FruitEntity>();

    }

    // Update is called once per frame
    private void Update()
    {
        if ((FruitEntity.transform.position - TargetPos).magnitude < 2)
            TargetPos = getNewPosition();
        FruitEntity.Movement.MoveTo(TargetPos);
    }

    private Vector2 getNewPosition()
    {
        index++;
        return points[index % points.Count];
    }

    private float2 levelXBounds()
    {
        LevelInfo levelInfo = Game.Find().LevelManager.GetCurrentLevel();

        return new Vector2((levelInfo.Pos.x - width), (levelInfo.Pos.x + width));
    }

    private float2 levelYBounds()
    {
        LevelInfo levelInfo = Game.Find().LevelManager.GetCurrentLevel();

        return new Vector2((levelInfo.Pos.y - height), (levelInfo.Pos.y + height));
    }

}
