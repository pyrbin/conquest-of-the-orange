using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerEnteredArrowEvent : UnityEvent<MapLevel.MoveDirection>
{
}

public class MapLevel : MonoBehaviour
{
    public GameObject ArrowUp;   // 1
    public GameObject ArrowRight;// 2
    public GameObject ArrowDown; // 3
    public GameObject ArrowLeft; // 4

    public enum MoveDirection : int
    {
        Up, Right, Down, Left
    }

    public static int2 GetMoveDirOffset(MoveDirection dir)
    {
        int2 offset;
        switch (dir)
        {
            case MoveDirection.Up:
                offset = new int2(0, -1);
                break;

            case MoveDirection.Right:
                offset = new int2(1, 0);
                break;

            case MoveDirection.Down:
                offset = new int2(0, 1);
                break;

            case MoveDirection.Left:
                offset = new int2(-1, 0);
                break;

            default:
                offset = new int2(0, 0);
                break;
        }
        return offset;
    }

    public PlayerEnteredArrowEvent ArrowEvent;

    public void Awake()
    {
        if (ArrowEvent == null) Init();
    }

    public void Init()
    {
        ResetArrows();
        if (ArrowEvent == null)
            ArrowEvent = new PlayerEnteredArrowEvent();
    }

    public void ResetArrows()
    {
        ArrowUp.SetActive(false);
        ArrowRight.SetActive(false);
        ArrowDown.SetActive(false);
        ArrowLeft.SetActive(false);
    }

    public void PlayerEnteredArrow(MoveDirection dir)
    {
        ArrowEvent.Invoke(dir);
    }

    public void EnableArrow(MoveDirection dir)
    {
        GetArrowByDir(dir).SetActive(true);
    }

    private GameObject GetArrowByDir(MoveDirection id)
    {
        GameObject arrow = ArrowUp;
        switch (id)
        {
            case MoveDirection.Up:
                arrow = ArrowUp;
                break;

            case MoveDirection.Right:
                arrow = ArrowRight;
                break;

            case MoveDirection.Down:
                arrow = ArrowDown;
                break;

            case MoveDirection.Left:
                arrow = ArrowLeft;
                break;
        }
        return arrow;
    }
}
