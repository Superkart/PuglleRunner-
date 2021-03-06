using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawn : MonoBehaviour
{
    public List<GameObject> roads;
    private float offset = 30f;
    // Start is called before the first frame update
    void Start()
    {
        if(roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
        
    }
    public void MoveRoad()
    {
        GameObject moveRoad = roads[0];
        roads.Remove(moveRoad);
        float newZ = roads[roads.Count - 1].transform.position.z + offset;
        moveRoad.transform.position = new Vector3(0f, 0f, newZ);
        roads.Add(moveRoad);

       var obsSpawner = moveRoad.GetComponentInChildren<ObstacleSpawner>();

        if (obsSpawner != null)
        {
            obsSpawner.ResetAllObstacles();
        }
        else {
            Debug.LogError("Obstacle Spawner is missing");
        }

    }

    
   
}
