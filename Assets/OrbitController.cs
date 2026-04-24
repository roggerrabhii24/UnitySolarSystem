using UnityEngine;
using TMPro;

public class OrbitController : MonoBehaviour
{
    public TMP_Dropdown planetDropdown;
    public Transform sun;

    public Transform[] planets;
    public float[] speeds;

    private Vector3[] originalPositions;
    private Quaternion[] originalRotations;

    private int previousSelected = 0;

    void Start()
    {
        originalPositions = new Vector3[planets.Length];
        originalRotations = new Quaternion[planets.Length];

        for (int i = 0; i < planets.Length; i++)
        {
            originalPositions[i] = planets[i].position;
            originalRotations[i] = planets[i].rotation;
        }

        planetDropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    void Update()
    {
        int selected = planetDropdown.value;

        if (selected == 0) return;

        if (selected == 1)
        {
            for (int i = 0; i < planets.Length; i++)
                RotatePlanet(i);
        }
        else
        {
            int index = selected - 2;
            RotatePlanet(index);
        }
    }

    void OnDropdownChanged(int newSelected)
    {
        ResetPreviousSelection(previousSelected);

        if (newSelected == 0)
        {
            ResetAllPlanets();
        }

        previousSelected = newSelected;
    }

    void RotatePlanet(int index)
    {
        if (index < 0 || index >= planets.Length) return;

        planets[index].RotateAround(
            sun.position,
            Vector3.up,
            speeds[index] * Time.deltaTime
        );
    }

    void ResetPreviousSelection(int selected)
    {
        if (selected == 0) return;

        if (selected == 1)
        {
            ResetAllPlanets();
        }
        else
        {
            int index = selected - 2;
            ResetPlanet(index);
        }
    }

    void ResetPlanet(int index)
    {
        if (index < 0 || index >= planets.Length) return;

        planets[index].position = originalPositions[index];
        planets[index].rotation = originalRotations[index];
    }

    void ResetAllPlanets()
    {
        for (int i = 0; i < planets.Length; i++)
        {
            ResetPlanet(i);
        }
    }
}