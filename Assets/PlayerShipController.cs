using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    private ShipAI ai;

    void Start()
    {
        ai = GetComponent<ShipAI>();
    }

    void Update()
    {
        if (ai == null) return;

        float move = Input.GetAxis("Vertical");     // W/S
        float turn = Input.GetAxis("Horizontal");   // A/D

        // Forward/backward movement
        ai.throttle = Mathf.Clamp(move, -1f, 1f);

        // Rotation (Yaw)
        ai.angularTorque = new Vector3(0, turn * 100f, 0);
    }
}