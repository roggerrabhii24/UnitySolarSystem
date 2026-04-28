using UnityEngine;

public class PlanetSceneLoader : MonoBehaviour
{
    public SceneTransitionManager transitionManager;

    public void OnPlanetSelected(int index)
    {
        if (index == 1) // Earth
        {
            transitionManager.LoadSceneWithFade("EarthExperience");
        }

        if (index == 2) // Mars
        {
            transitionManager.LoadSceneWithFade("Mars Landscape 3D Overview");
        }
    }

    public void LoadSolarSystem()
    {
        transitionManager.LoadSceneWithFade("solarsystem");
    }
}