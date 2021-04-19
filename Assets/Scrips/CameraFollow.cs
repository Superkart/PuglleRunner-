using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerTransform;
    private Vector3 CameraOffset;

    [Range(0.01f, 1f)]
    public float SmoothnessFactor = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        CameraOffset = transform.position - PlayerTransform.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 NewPosition = PlayerTransform.position + CameraOffset;
        transform.position = Vector3.Slerp(transform.position, NewPosition, SmoothnessFactor);


    }
}
