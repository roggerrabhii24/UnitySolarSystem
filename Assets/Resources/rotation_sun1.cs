using UnityEngine;
using System.Collections;

public class rotation_sun : MonoBehaviour
{
    private float rot_speed;

    void Start()
    {
        rot_speed = Random.Range(6f, 10f);
    }

    void Update()
    {
        // transform.Rotate is fine as-is; no API changes needed here
        this.transform.Rotate(Vector3.up, Time.deltaTime * rot_speed);
    }
}
