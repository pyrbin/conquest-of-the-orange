using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelCamera : MonoBehaviour
{
    public Transform Target;

    public float Smooth = 5.0f;

    // Shake vars Transform of the camera to shake. Grabs the gameObject's transform if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;

    public float decreaseFactor = 1.0f;

    [HideInInspector]
    public bool isShaking = false;

    private Vector3 originalPos;
    private float originalShakeDuration; //<--add this

    private void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponentInChildren<Camera>().transform;
        }
    }

    private void OnEnable()
    {
        originalPos = camTransform.localPosition;
        originalShakeDuration = shakeDuration; //<--add this
    }

    private void Update()
    {
        if (Target)
        {
            transform.position = Vector3.Lerp(
                transform.position, Target.position,
                Time.deltaTime * Smooth);
        }

        if (isShaking)
        {
            if (shakeDuration > 0)
            {
                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, originalPos + Random.insideUnitSphere * shakeAmount, Time.deltaTime * 3);

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = originalShakeDuration; //<--add this
                camTransform.localPosition = originalPos;
                isShaking = false;
            }
        }
    }

    public void ZoomInOut()
    {
        GetComponent<Animation>().Play();
    }

    public void ShakeCamera()
    {
        isShaking = true;
    }

    public void ShakeCamera(float dur, float amount)
    {
        isShaking = true;
        shakeDuration = dur;
        shakeAmount = amount;
    }
}
