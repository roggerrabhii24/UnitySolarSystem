using UnityEngine;

public class PlanetExperienceManager : MonoBehaviour
{
    [Header("Space Camera")]
    public Camera spaceCamera;

    [Header("Planet Cameras")]
    public Camera earthCamera;
    public Camera marsCamera;
    public Camera venusCamera;
    // (you can add more later)

    void Start()
    {
        SwitchToSpace(); // start in space
    }

    void DisableAllPlanetCameras()
    {
        if (earthCamera) earthCamera.enabled = false;
        if (marsCamera) marsCamera.enabled = false;
        if (venusCamera) venusCamera.enabled = false;
    }

    public void SwitchToSpace()
    {
        spaceCamera.enabled = true;
        DisableAllPlanetCameras();
    }

    public void SwitchToEarth()
    {
        spaceCamera.enabled = false;
        DisableAllPlanetCameras();
        earthCamera.enabled = true;
    }

    public void SwitchToMars()
    {
        spaceCamera.enabled = false;
        DisableAllPlanetCameras();
        marsCamera.enabled = true;
    }

    public void SwitchToVenus()
    {
        spaceCamera.enabled = false;
        DisableAllPlanetCameras();
        venusCamera.enabled = true;
    }
}