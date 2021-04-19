using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSpawner : MonoBehaviour
{
    private int initAmount = 2;
    private float initStartZ = 8.311115f;
    private float plotSize = 60f;
    private float xPosLeft = -26.96186f;
    private float xPosRight = 5.03814f;
    private float zLastPos = 0f;

    public List<GameObject> plots;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < initAmount; i++)
        {
            GameObject plotLeft = plots[Random.Range(0, plots.Count)];
            GameObject plotRight = plots[Random.Range(0, plots.Count)];

            float zPos = zLastPos + plotSize;

            Instantiate(plotLeft, new Vector3(xPosLeft, 0.025f, zPos), plotLeft.transform.rotation);
            Instantiate(plotRight, new Vector3(xPosRight, 0.025f, zPos), new Quaternion(0, 90, 0, 0));

            zLastPos += plotSize;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
