using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithVelocity : MonoBehaviour
{

    public float maxRotateSpeed = 10f;
    float currentRotateSpeed = 0f;
    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
