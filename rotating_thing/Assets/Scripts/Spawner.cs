using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header ("Parameters")]
    [SerializeField] Wall[] walls;
    [SerializeField] float spawnDelay = 2f;
    [SerializeField] float baseSpeed = 2f;

    float lastWall = 0f;

    private void Start() {
        InvokeRepeating("SpawnWall", 0, spawnDelay);
    }

    private void Update() {
        
    }

    private void SpawnWall()
    {
        InstantiateWall().SetSpeed(baseSpeed);
    }

    private Wall InstantiateWall()
    {
        Wall wall = null;
		var i = Random.Range(0, walls.Length);

		if (i != lastWall)
		{
			wall = Instantiate(walls[i], transform.position, transform.rotation);
			lastWall = i;
		}
        else
            return InstantiateWall();

        return wall;
    }
}
