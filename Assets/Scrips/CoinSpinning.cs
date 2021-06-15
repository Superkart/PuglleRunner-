using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpinning : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f, 2, 0f, Space.World);  
    }
}
