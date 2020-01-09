using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [Header ("Parameters")]
    [SerializeField] Vector3 direction;

    float speed = 0f;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }
}
