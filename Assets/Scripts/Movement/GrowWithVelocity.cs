using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GrowWithVelocity : MonoBehaviour
{
    private Rigidbody2D rigid;
    public GameObject Target;

    public float MinScale = 1;
    public float MaxScale = 3;
    public float GrowthFactor = 5;


    public float degrowthCooldown = 1;


    [HideInInspector]
    public float Growth = 1f;

    // Start is called before the first frame update
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Growth = MinScale + rigid.velocity.magnitude / GrowthFactor;

        if (Growth > MaxScale)
            Growth = MaxScale;

        if (Growth < MinScale)
            Growth = MinScale;

        Target.transform.localScale = new Vector3(Growth, Growth, Growth);
    }
}
