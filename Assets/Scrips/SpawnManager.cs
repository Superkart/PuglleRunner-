using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public RoadSpawn roadSpawner;
    public ObstacleSpawner obstacleSpawn;
    // Start is called before the first frame update
    void Start()
    {
        roadSpawner = GetComponent<RoadSpawn>();
        obstacleSpawn = GetComponent<ObstacleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTriggerEnter()
    {
        roadSpawner.MoveRoad();
    }
}
