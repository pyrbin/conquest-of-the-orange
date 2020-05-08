using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveToCursor : MonoBehaviour
{

    Vector3 currentMovementVector;

    Vector3 clickScreenPosition;
    Vector3 clickWorldPosition;

    Vector3 currentPointToMoveTo;
    Transform cachedTransform;

    public float speed = 0.0f;

    public float lastSpeed = 0.0f;
    public float acceleration = 3f;
    public float maxSpeed = 10.0f;
    public int slowingDistance = 1;

    public bool slowed = false;

    Rigidbody2D rigid;


    // Use this for initialization  
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        cachedTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
    
        // Retrive the click of the mouse in the game world
        clickScreenPosition = Input.mousePosition;

        clickWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(clickScreenPosition.x, clickScreenPosition.y, 0));
        currentPointToMoveTo = clickWorldPosition;

        // Reset the z position of the clicking point to 0
        currentPointToMoveTo.z = 0;

        // Calculate the current vector between the player position and the click
        Vector3 currentPlayerPosition = cachedTransform.position;

        // Find the angle (in radians) between the two positions (player position and click position)
        float angle = Mathf.Atan2(clickWorldPosition.y - currentPlayerPosition.y, clickWorldPosition.x - currentPlayerPosition.x);

        // Find the distance between the two points
        float distance = Vector3.Distance(currentPlayerPosition, clickWorldPosition);

        // Calculate the components of the new movemevent vector
        float xComponent = Mathf.Cos(angle) * distance;
        float yComponent = Mathf.Sin(angle) * distance;

        // Create the new movement vector
        Vector3 newMovementVector = new Vector3(xComponent, yComponent, 0);
        newMovementVector.Normalize();

        currentMovementVector = newMovementVector;

        Vector2 distanceToEndPoint = transform.position - currentPointToMoveTo;

        if (distanceToEndPoint.magnitude < 0.05)
        {
            speed = 0.0f;
        }
        else if (distanceToEndPoint.magnitude < slowingDistance)
        {
            speed *= 0.98f;
        }
        else
        {
            speed += currentMovementVector.magnitude * acceleration * Time.deltaTime;
        }



        // If the velocity is above the allowed limit, normalize it and keep it at a constant max speed when moving (instead of uniformly accelerating)
        Vector2 accelerationVector = currentMovementVector * speed;


        // If the velocity is above the allowed limit, normalize it and keep it at a constant max speed when moving (instead of uniformly accelerating)
        if (accelerationVector.magnitude >= (maxSpeed))
        {
            accelerationVector.Normalize();
            accelerationVector *= maxSpeed;
            speed = maxSpeed;
        }

        // Apply velocity to gameobject position
        rigid.velocity = accelerationVector;
    }
}
