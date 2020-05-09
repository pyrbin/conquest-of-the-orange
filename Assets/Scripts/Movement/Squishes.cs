using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FruitEntity))]
public class Squishes : MonoBehaviour
{
    public float Power = 5f;
    public float Threshold = 5f;

    private FruitMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<FruitEntity>().Movement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print(movement.VelocityMagnitude() + "colision");
        FruitMovement otherMovement = collision.gameObject.GetComponent<FruitEntity>().Movement;


        if (movement.VelocityMagnitude() - otherMovement.VelocityMagnitude() > Threshold)
            Destroy(this.gameObject);
    }
}
