using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FruitEntity))]
public class Squishes : MonoBehaviour
{
    public float Threshold = 0.1f;
    private FruitEntity fruitEntity;

    public bool Eatable(FruitEntity other)
    {
        if (other == null || fruitEntity == null) return false;
        if (other.transform.parent.tag == transform.parent.tag) return false;
        return (fruitEntity.Grower.Growth - other.Grower.Growth > Threshold);
    }

    private void Awake()
    {
        fruitEntity = GetComponent<FruitEntity>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "FruitEntity")
        {
            AudioManager.Find().PlayOneShotSound(AudioManager.SoundType.BOUNCE);
            return;
        }
           

        FruitEntity otherFruitEntity = collision.gameObject.GetComponent<FruitEntity>();

        if (Eatable(otherFruitEntity))
        {
            otherFruitEntity.OnSquish();
        }
        else if (otherFruitEntity.Squishes.Eatable(fruitEntity))
        {
            fruitEntity.OnSquish();
        }
    }
}
