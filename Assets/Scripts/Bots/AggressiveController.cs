using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveController : MonoBehaviour
{
    public FruitEntity FruitEntity { get; private set; }

    [HideInInspector]
    public Vector3 TargetPos { get; private set; }

    private Rigidbody2D player;

    // Start is called before the first frame update
    private void Start()
    {
        FruitEntity = GetComponentInChildren<FruitEntity>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (FruitEntity == null || player == null) return;

        float xOffset = Random.Range(-1.5f, 1.5f);
        float yOffset = Random.Range(-1.5f, 1.5f);
        Vector2 pos = new Vector2(player.position.x + xOffset, player.position.y + yOffset);
        FruitEntity.Movement.MoveTo(pos + player.velocity * UnityEngine.Random.insideUnitSphere * 2f);
    }
}
