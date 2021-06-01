using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    public List<PoolItem> PoolList;

    public List<GameObject> PooledObjects;

    // Start is called before the first frame update
    void Awake()
    {
        PooledObjects = new List<GameObject>();
        CreateItemsInsideThePool();
    }

    void CreateItemsInsideThePool()
    {
        foreach (PoolItem item in PoolList)
        {
            for (int i = 0; i < item.NoOfInstances; i++)
            {
                var inst = GameObject.Instantiate(item.ObjectTobeCreated);
                item.Objects = new List<GameObject>();
                PooledObjects.Add(inst);
                inst.SetActive(false);
            }
        }
    }

    public GameObject GetItemFromPool(string tag)
    {
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            var obj = PooledObjects[i];
            if (!obj.activeInHierarchy && obj.CompareTag(tag))
            {
                return obj;            
            }
        }

        return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
