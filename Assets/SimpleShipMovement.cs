using UnityEngine;

public class SimpleShipMovement : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float turnSpeed = 80f;
    public float verticalSpeed = 80f;

    void Update()
    {
        float move = Input.GetAxis("Vertical");      // W/S
        float turn = Input.GetAxis("Horizontal");    // A/D

        // Q / E for up and down
        float vertical = 0f;
        if (Input.GetKey(KeyCode.Q)) vertical = 1f;     // up
        if (Input.GetKey(KeyCode.E)) vertical = -1f;    // down

        // Forward movement (your ship uses right axis)
        transform.position += transform.right * move * moveSpeed * Time.deltaTime;

        // Vertical movement (Y axis)
        transform.position += Vector3.up * vertical * verticalSpeed * Time.deltaTime;

        // Rotation
        transform.Rotate(Vector3.up * turn * turnSpeed * Time.deltaTime);
    }
}