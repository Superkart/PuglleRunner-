using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public int NoOfObstacles = 2;

    [SerializeField]
    public List<Transform> ObstacleTransforms;

    // Start is called before the first frame update
    void Start()
    {
        // get two random items from list
        if (ObstacleTransforms == null)
            return;

     var shuffledList = ObstacleTransforms.OrderBy(x => Random.value).ToList();

        var item1 = shuffledList[0];
        var item2 = shuffledList[1];

        item1.gameObject.SetActive(true);
        item2.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
