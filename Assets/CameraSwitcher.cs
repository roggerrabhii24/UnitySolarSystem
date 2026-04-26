using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera shipThirdPersonCamera;
    public Camera shipPOVCamera;

    public Camera mercuryCamera;
    public Camera venusCamera;
    public Camera earthCamera;
    public Camera marsCamera;
    public Camera jupiterCamera;
    public Camera saturnCamera;
    public Camera uranusCamera;
    public Camera neptuneCamera;
    public Camera plutoCamera;

    private Camera[] allCameras;

    void Start()
    {
        allCameras = new Camera[]
        {
            shipThirdPersonCamera,
            shipPOVCamera,
            mercuryCamera,
            venusCamera,
            earthCamera,
            marsCamera,
            jupiterCamera,
            saturnCamera,
            uranusCamera,
            neptuneCamera,
            plutoCamera
        };

        EnableOnly(shipThirdPersonCamera);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EnableOnly(shipThirdPersonCamera);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EnableOnly(shipPOVCamera);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EnableOnly(mercuryCamera);
        if (Input.GetKeyDown(KeyCode.Alpha4)) EnableOnly(venusCamera);
        if (Input.GetKeyDown(KeyCode.Alpha5)) EnableOnly(earthCamera);
        if (Input.GetKeyDown(KeyCode.Alpha6)) EnableOnly(marsCamera);
        if (Input.GetKeyDown(KeyCode.Alpha7)) EnableOnly(jupiterCamera);
        if (Input.GetKeyDown(KeyCode.Alpha8)) EnableOnly(saturnCamera);
        if (Input.GetKeyDown(KeyCode.Alpha9)) EnableOnly(uranusCamera);

        if (Input.GetKeyDown(KeyCode.N)) EnableOnly(neptuneCamera);
        if (Input.GetKeyDown(KeyCode.P)) EnableOnly(plutoCamera);
    }

    void EnableOnly(Camera cam)
    {
        foreach (Camera c in allCameras)
        {
            if (c != null)
                c.gameObject.SetActive(false);
        }

        if (cam != null)
            cam.gameObject.SetActive(true);
    }
}