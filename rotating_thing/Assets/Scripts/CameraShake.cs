using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    float shakeDuration = 0f;
    float shakeAmount = 0f;
    float decreaseFactor = 1f;

    Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.position = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor; 
        }
        else
        {
            shakeDuration = 0f;
            transform.position = originalPos;
        }
    }

    public void SetShakeParameters(float durationValue, float amountValue, float decreaseFactorValue)
    {
        shakeDuration = durationValue;
        shakeAmount = amountValue;
        decreaseFactor = decreaseFactorValue;
    }
}
