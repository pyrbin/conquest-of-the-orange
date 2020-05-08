using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DemoRTSCamera : MonoBehaviour
{
    [Header("Mouse Settings")]
    [Space]
    public float speed = 2.0f;
    public float zoomSpeed = 2.0f;

    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {

        // Directional Movement
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        // Camera Zoom Function
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (camera.orthographicSize > 3)
            {
                camera.orthographicSize -= 1;
            }

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (camera.orthographicSize < 13)
            {
                camera.orthographicSize += 1;
            }
        }
    }
}


