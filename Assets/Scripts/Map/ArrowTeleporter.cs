using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ArrowTeleporter : MonoBehaviour
{
    public MapLevel.MoveDirection Direction;

    // Start is called before the first frame update
    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.tag == "Player")
        {
            GetComponentInParent<MapLevel>().PlayerEnteredArrow(Direction);
        }
    }
}
