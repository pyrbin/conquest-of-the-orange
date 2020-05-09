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

    public float minAcceleration = 5f;
    public float minSlowingDistance = 1f;

    public float accDistanceFactor = 1f;
    public float slowingFactor = 1f;

    [HideInInspector]
    public Rigidbody2D rigid;

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

        // Add acceleration
        float acceleration = minAcceleration + accDistanceFactor*math.pow(distanceToEndPoint.magnitude, 1.25f)/3;
        currentMovementVector *= acceleration;

        // Add steering
        currentMovementVector = currentMovementVector - rigid.velocity;

        if (distanceToEndPoint.magnitude < minSlowingDistance + rigid.velocity.magnitude*0.2f*slowingFactor)
        {
            rigid.velocity *= 0.99f;
        } else
        {
            rigid.AddForce(currentMovementVector);
        }
    }

    public float VelocityMagnitude()
    {
        return rigid.velocity.magnitude;
    }
}
