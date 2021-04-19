using UnityEngine;

public class NewCharacterController : MonoBehaviour
{
    public float speed;
    public float JumpForce;
    private Rigidbody rb;
    //private bool jump = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movehorizontal = Input.GetAxis("Horizontal");
        float movevertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movehorizontal, 0f, movevertical);
        rb.AddForce(movement * speed);
        if (Input.GetKeyDown(KeyCode.Space))
        {

            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }




    }
}