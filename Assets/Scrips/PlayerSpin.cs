using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpin : MonoBehaviour
{
    float Speed = 1.2f;
    public void Rotate(float horizontalMovement)
    {
        float fSpeed = horizontalMovement * Speed;
        transform.Rotate(fSpeed, 0, 0, Space.World);
    }
}
