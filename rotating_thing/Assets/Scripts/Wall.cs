using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField] MeshRenderer renderer = null;
    [Header ("Parameters")]
    [SerializeField] Vector3 direction;
    [SerializeField] float fadeSpeed = 5f;
    [SerializeField] float fadeStartPos = -5f;

    float speed = 0f;

    Material mat = null;
    Color color;

    void Start()
    {
        mat = renderer.material;
        color = mat.color;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if (transform.position.z < fadeStartPos && color.a > 0)
        {
            color.a = color.a - fadeSpeed * Time.deltaTime;
            mat.color = color;
            renderer.material = mat;
        }
        if (color.a <= 0)
            Destroy(gameObject);
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }
}
