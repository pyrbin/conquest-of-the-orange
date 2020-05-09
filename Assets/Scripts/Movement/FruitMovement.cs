using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[RequireComponent(typeof(Rigidbody2D))]
public class FruitMovement : MonoBehaviour
{
    private Vector2 currentMovementVector;

    // private Vector3 clickScreenPosition;
    private Vector3 clickWorldPosition;

    private Vector3 currentPointToMoveTo;
    private Transform cachedTransform;

    public float speed = 0.0f;

    public float lastSpeed = 0.0f;
    public float acceleration = 0.01f;
    public float maxSpeed = 10.0f;
    public int slowingDistance = 1;

    public bool slowed = false;

    private Rigidbody2D rigid;

    // Use this for initialization
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        cachedTransform = transform;
    }

    // Update is called once per frame
    public void MoveTo(Vector3 moveTo)
    {
        clickWorldPosition = moveTo;
        currentPointToMoveTo = clickWorldPosition;

        // Reset the z position of the clicking point to 0
        currentPointToMoveTo.z = 0;

        // Calculate the current vector between the player position and the click
        Vector2 currentPlayerPosition = cachedTransform.position;

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

        if (distanceToEndPoint.magnitude < slowingDistance)
        {
            rigid.velocity *= 0.98f;
        } else
        {
            rigid.velocity += currentMovementVector * acceleration * Time.deltaTime;
        }



    }

    public void Bounce(int force)
    {
        
    }
}
