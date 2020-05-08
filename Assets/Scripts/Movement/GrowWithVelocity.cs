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

    // Start is called before the first frame update
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float growtFactor = MinScale + rigid.velocity.magnitude / GrowthFactor;

        if (growtFactor > MaxScale)
            growtFactor = MaxScale;

        Target.transform.localScale = new Vector3(growtFactor, growtFactor, growtFactor);
    }
}
