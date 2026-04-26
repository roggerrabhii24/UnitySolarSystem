using UnityEngine;
using TMPro;

public class CameraDropdownController : MonoBehaviour
{
    public TMP_Dropdown dropdown;

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

    private Camera[] cameras;

    void Start()
    {
        cameras = new Camera[]
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

        dropdown.onValueChanged.AddListener(SwitchCamera);

        SwitchCamera(0);
    }

    void SwitchCamera(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != null)
                cameras[i].gameObject.SetActive(i == index);
        }
    }
}