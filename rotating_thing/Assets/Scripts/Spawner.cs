using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header ("Parameters")]
    [SerializeField] Wall[] walls;
    [SerializeField] float spawnDelay = 2f;
    [SerializeField] float baseSpeed = 10f;
    [SerializeField] float maxSpeed = 30f;
    [SerializeField] int wallCounterSpeedUp = 5;
    [SerializeField] float speedToAdd  = 2f;

    float lastWall = 0f;
    List<Wall> instantiatedWalls = new List<Wall>();
    int counter = 0;

    private void Start() {
        InvokeRepeating("SpawnWall", 0, spawnDelay);
    }

    private void Update() {
        
    }

    private void SpawnWall()
    {
        instantiatedWalls.Add(InstantiateWall());
        counter++;
        if (counter >= wallCounterSpeedUp && baseSpeed < maxSpeed)
        {
            baseSpeed += speedToAdd;
            counter = 0;
        }
        foreach(Wall wall in instantiatedWalls)
            wall.SetSpeed(baseSpeed);
    }

    private Wall InstantiateWall()
    {
        Wall wall = null;
		var i = Random.Range(0, walls.Length);

		if (i != lastWall)
		{
			wall = Instantiate(walls[i], transform.position, walls[i].transform.rotation);
            lastWall = i;
		}
        else
            return InstantiateWall();

        return wall;
    }
}
