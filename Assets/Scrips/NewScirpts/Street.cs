using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Street : MonoBehaviour
{
    public BuildingSpawner Spawner;
    public GameObject Building1;
    public GameObject Building2;
    // Start is called before the first frame update


    public void SpawnBuilding(GameObject tower, bool left = false)
    {
      /*  var building = Spawner.GetRandomBuilding();
        building.SetActive(true);
        building.transform.SetParent(tower.transform);
        if(left)
           building.transform.Rotate(0f, 180f, 0f);
        building.transform.localPosition = Vector3.zero;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
