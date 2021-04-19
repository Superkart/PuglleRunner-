using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10f;
    public SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed / 2;
        float verticalMovement = Input.GetAxis("Vertical") * movementSpeed;
        transform.Translate(new Vector3(horizontalMovement, 0, verticalMovement) * Time.deltaTime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEnter();
    }
}
