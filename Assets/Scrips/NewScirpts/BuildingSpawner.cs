using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    public ObjectPool pool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject GetRandomBuilding()
    {
        //Hardcoding the building rage to 5
       // int randomNo = Random.Range(0, 4);
        GameObject gameObject1 = pool.GetItemFromPool("Buildings");
        return gameObject1;

    }
}
