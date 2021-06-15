using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private List<Transform> m_obstacleTiles;
    int listSize;
    int randomNumberObjects;
    public List<GameObject> ObstalePrefabs;
    public int ObjectCount = 20;
    List<int> m_spawnedElements;
    public List<GameObject> Lanes;
    List<GameObject> m_SpawnedObstacles;

    public void Start()
    {
        m_spawnedElements = new List<int>();
        m_obstacleTiles = new List<Transform>();
        m_SpawnedObstacles = new List<GameObject>();

        //left right and center
        foreach (GameObject go in Lanes)
        {
            var laneTiles = go.GetComponentsInChildren<Transform>();
            for (int i = 0; i < laneTiles.Length; i++)
            {
                m_obstacleTiles.Add(laneTiles[i]);
            }

        }

        listSize = m_obstacleTiles.Count;
        randomNumberObjects = Random.Range(10, 15);
        SpawnObstacles();
    }

    public void ResetAllObstacles()
    {
        foreach (var go in m_SpawnedObstacles)
        {
            GameObject.Destroy(go);
        }

        m_spawnedElements.Clear();
        SpawnObstacles();
    }

    public void SpawnObstacles()
    {
        for (int i = 0; i < randomNumberObjects; i++)
        {
            int tileNumber = Random.Range(0, listSize);
            if (!m_spawnedElements.Contains(tileNumber))
            {
                m_spawnedElements.Add(tileNumber);
                SpawnObstacleAtPosition(m_obstacleTiles[tileNumber].transform);

            }
        }
    }

    void SpawnObstacleAtPosition(Transform trans)
    {
        var obstacle = ObstalePrefabs[Random.Range(0, ObstalePrefabs.Count)];
        var obj = GameObject.Instantiate(obstacle, Vector3.zero, Quaternion.identity, trans);
        obj.transform.localPosition = Vector3.zero;
        m_SpawnedObstacles.Add(obj);
    }
}
