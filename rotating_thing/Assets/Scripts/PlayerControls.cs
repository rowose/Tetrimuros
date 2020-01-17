using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField] GameObject obj = null;
    [SerializeField] GameObject projection = null;
    [SerializeField] List<GameObject> singleBlocks = null;
    [Header ("Parameters")]
    [SerializeField] float rotSpeed = 500f;
    [SerializeField] float tryDelay = 0.2f;
    [SerializeField] Vector3 explosionOrigin = Vector3.zero;

    bool rotate = false;
    Vector3 _rotDirection;
    float _rotValue = 0f; 
    float timer = 0f;

    void Update()
    {
        if (obj)
        {
            if (rotate)
                RotateObj();
            else if (!rotate)
            {
                if (Input.GetKeyDown(KeyCode.A))
                    SetProjection(transform.forward, 1);
                if (Input.GetKeyDown(KeyCode.D))
                    SetProjection(transform.forward, -1);
                if (Input.GetKeyDown(KeyCode.W))
                    SetProjection(Vector3.left, -1);
                if (Input.GetKeyDown(KeyCode.S))
                    SetProjection(Vector3.left, 1);
            }
        }
    }

    private void RotateObj()
    {
		if (rotate && (Vector3.Distance(obj.transform.eulerAngles, projection.transform.eulerAngles) > 15))
        {
			obj.transform.Rotate(_rotDirection, _rotValue * rotSpeed * Time.deltaTime, Space.World);
            timer += Time.deltaTime;
            if (timer >= tryDelay)
            {
                obj.transform.eulerAngles = projection.transform.eulerAngles;
                rotate = false;
                timer = 0f;
            }
        }
        else
		{
            timer = 0f;
			rotate = false;
			if (obj.transform.eulerAngles != projection.transform.eulerAngles)
			{
				obj.transform.eulerAngles = new Vector3(Mathf.Clamp(obj.transform.eulerAngles.x, projection.transform.eulerAngles.x, projection.transform.eulerAngles.x),
										    Mathf.Clamp(obj.transform.eulerAngles.y, projection.transform.eulerAngles.y, projection.transform.eulerAngles.y),
										        Mathf.Clamp(obj.transform.eulerAngles.z, projection.transform.eulerAngles.z, projection.transform.eulerAngles.z));
			}
		}
    }

    private void SetProjection(Vector3 direction, float angleValue)
    {
		projection.transform.Rotate(direction, 90 * angleValue, Space.World);
		_rotDirection = direction;
		_rotValue = angleValue;
		rotate = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.transform.tag == "Wall")
            Death();
    }

    IEnumerator SlowMoDeath()
    {
        Destroy(obj);
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.01f);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }

    private void Death()
    {
        foreach(GameObject cube in singleBlocks)
        {
            cube.SetActive(true);
            cube.GetComponent<Rigidbody>().useGravity = true;
            cube.GetComponent<Rigidbody>().AddExplosionForce(200, transform.position + explosionOrigin, 50, 1);
            cube.transform.SetParent(null);
        }
        Camera.main.GetComponent<CameraShake>().SetShakeParameters(0.5f, 1f, 1);
        StartCoroutine("SlowMoDeath");
    }
}