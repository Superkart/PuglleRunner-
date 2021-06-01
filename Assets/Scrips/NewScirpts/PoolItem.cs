using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolItem
{
    public GameObject ObjectTobeCreated;
    public int NoOfInstances;
    public List<GameObject> Objects;
    public bool InUse = false;
    public PoolItem()
    {
    
    }
}
