﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Vector3 WorldMousePos { get; private set; }

    public FruitEntity FruitEntity { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        FruitEntity = GetComponentInChildren<FruitEntity>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (FruitEntity.Movement.rigid.velocity.magnitude > 1f)
            AudioManager.Find().PlaySound(AudioManager.SoundType.ROLL);
        if (FruitEntity.Movement.rigid.velocity.magnitude < 1f)
            AudioManager.Find().StopSound(AudioManager.SoundType.ROLL);

        UpdateMousePos();

        FruitEntity.Movement.MoveTo(WorldMousePos);
    }

    private void UpdateMousePos()
    {
        WorldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }
}
