using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 3f;
    public SpawnManager spawnManager;
    public PlayerSpin PSpinRef;
    
    // Update is called once per frame
    void Update()
    {

        //float verticalMovement = Input.GetAxis("Vertical");
        float verticalMovement = 1f;
        float horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed / 2;
        float vericalClampedMovment = Mathf.Clamp01(verticalMovement) * movementSpeed;

        var currPos = transform.localPosition;

        if (currPos.x < -5.6 )
        {
            if(horizontalMovement < 0f)
                horizontalMovement = 0f;
        }
        if (currPos.x > 1.6f)
        {
            if (horizontalMovement > 0f)
                horizontalMovement = 0f;
        }

         transform.Translate(new Vector3(horizontalMovement, 0, vericalClampedMovment) * Time.deltaTime);

        if (PSpinRef != null && !GameOver.gameOver)
            PSpinRef.Rotate(vericalClampedMovment);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("GameOver");
            GameOver.gameOver = true;
        }

        if (other.CompareTag("SpawnTrigger"))
        {
            spawnManager.SpawnTriggerEnter();
        }
    }
}
