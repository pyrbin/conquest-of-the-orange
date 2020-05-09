using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FruitEntity))]
public class Squishes : MonoBehaviour
{
    public float Threshold = 0.1f;
    private FruitEntity fruitEntity;

    // Start is called before the first frame update
    void Start()
    {
        fruitEntity = GetComponent<FruitEntity>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "FruitEntity")
            return;

        FruitEntity otherFruitEntity = collision.gameObject.GetComponent<FruitEntity>();
        Squishes otherSquishy = otherFruitEntity.gameObject.GetComponent<Squishes>();


        if (fruitEntity.Grower.Growth - otherFruitEntity.Grower.Growth > otherSquishy.Threshold)
        {
            otherFruitEntity.OnSquish();
        }


        if (otherFruitEntity.Grower.Growth - fruitEntity.Grower.Growth > Threshold)
        {
            fruitEntity.OnSquish();
        }


    }
}
