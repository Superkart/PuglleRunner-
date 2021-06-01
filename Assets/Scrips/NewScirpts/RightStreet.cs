using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightStreet : Street
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnBuilding(Building1);
        SpawnBuilding(Building2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
