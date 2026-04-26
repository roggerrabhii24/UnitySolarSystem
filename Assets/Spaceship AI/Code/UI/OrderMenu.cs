using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Allows the user to issue an order to the ship selected on the user interface.
/// The selected ship is stored in the HUDMarkers script.
/// </summary>
public class OrderMenu : MonoBehaviour
{
    [Tooltip("Buttons which give orders to currently selected ship")]
    public Button[] OrderButtons;

    // Just some consts for easy access
    private const int MOVE_TO = 0, PATROL = 1, IDLE = 2, MOVE_CENTER = 3, FOLLOW = 4;

    // Waypoints in scene
    private Transform[] _waypoints;

    private void Awake()
    {
        // Get all waypoints in scene
        var wps = GameObject.FindGameObjectsWithTag("Waypoint");
        // Get Transform references instead of GameObject references
        _waypoints = new Transform[wps.Length];
        for(int i=0; i<wps.Length; i++){
            _waypoints[i] = wps[i].transform;
        }

    }

    /// <summary>
    /// Below are three examples on how to issue orders to a ship. 
    /// </summary>
    void Start()
    {
        if (OrderButtons.Length < 3)
            Debug.LogError("Order buttons not assigned to OrderMenu object");

        OrderButtons[MOVE_TO].onClick.AddListener(() =>
        {
            if (HUDMarkers.Instance.Target != null)
            {
                var RandomWaypoint = _waypoints[Random.Range(0, _waypoints.Length - 1)];
                HUDMarkers.Instance.Target.GetComponent<ShipAI>().MoveTo(RandomWaypoint);
            }
            else
            {
                ShowError();
            }
        });
        OrderButtons[PATROL].onClick.AddListener(() =>
        {
            if (HUDMarkers.Instance.Target != null)
            {
                HUDMarkers.Instance.Target.GetComponent<ShipAI>().PatrolPath(_waypoints);
            }
            else
            {
                ShowError();
            }
        });
        OrderButtons[IDLE].onClick.AddListener(() =>
        {
            if (HUDMarkers.Instance.Target != null)
            {
                HUDMarkers.Instance.Target.GetComponent<ShipAI>().Idle();
            }
            else
            {
                ShowError();
            }
        });
        OrderButtons[MOVE_CENTER].onClick.AddListener(() =>
        {
            if (HUDMarkers.Instance.Target != null)
            {
                HUDMarkers.Instance.Target.GetComponent<ShipAI>().MoveTo(Vector3.one);
            }
            else
            {
                ShowError();
            }
        });
        OrderButtons[FOLLOW].onClick.AddListener(() =>
        {
            if (HUDMarkers.Instance.Target != null)
            {
                List<Ship> ships = new List<Ship>(FindObjectsOfType<Ship>());
                ships.Remove(HUDMarkers.Instance.Target.GetComponent<Ship>());

                Ship otherShip; // Find another ship to follow
                do
                {
                    otherShip = ships[Random.Range(0, ships.Count - 1)];
                } while (otherShip.transform == HUDMarkers.Instance.Target);

                HUDMarkers.Instance.Target.GetComponent<ShipAI>().Follow(otherShip.transform);
            }
            else
            {
                ShowError();
            }
        });
    }

    private static void ShowError()
    {
        ConsoleOutput.Instance.PostMessage("Error: No ship is targeted!", Color.red);
    }

}
