using UnityEngine;

public class PlanetSceneLoader : MonoBehaviour
{
    public SceneTransitionManager transitionManager;

    public void OnPlanetSelected(int index)
    {
        // Dropdown order:
        // 0 = Solar System
        // 1 = Earth
        // 2 = Moon
        // 3 = Mars

        if (index == 0)
        {
            LoadSolarSystem();
        }
        else if (index == 1)
        {
            transitionManager.LoadSceneWithFade("EarthExperience");
        }
        else if (index == 2)
        {
            transitionManager.LoadSceneWithFade("MoonLunarLand");
        }
        else if (index == 3)
        {
            transitionManager.LoadSceneWithFade("Mars Landscape 3D Overview");
        }
    }

    public void LoadSolarSystem()
    {
        transitionManager.LoadSceneWithFade("solarsystem");
    }
}