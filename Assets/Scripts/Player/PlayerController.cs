using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Vector3 WorldMousePos { get; private set; }

    private FruitEntity FruitEntity;

    // Start is called before the first frame update
    private void Awake()
    {
        FruitEntity = GetComponentInChildren<FruitEntity>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateMousePos();

        FruitEntity.Movement.MoveTo(WorldMousePos);
    }

    private void UpdateMousePos()
    {
        WorldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }
}
