using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10f;
    public SpawnManager spawnManager;
   
    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed / 2;
        float verticalMovement = Input.GetAxis("Vertical");
        //float horizontalClampedMovement = Mathf.Clamp(horizontalMovement, -5.6f, 1.6f);
        float vericalClampedMovment = Mathf.Clamp01(verticalMovement) * movementSpeed;

        transform.Translate(new Vector3(horizontalMovement, 0, vericalClampedMovment) * Time.deltaTime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEnter();
    }
}
