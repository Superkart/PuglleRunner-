using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftStreet : Street
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnBuilding(Building1, true);
        SpawnBuilding(Building2, true);
    }
}
