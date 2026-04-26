using UnityEngine;

/// <summary>
/// Ties all the primary ship components together.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Ship : MonoBehaviour
{
    private const float WINGMAN_OFFSET = 30f;

    #region ship components

    // Artificial intelligence controls
    public ShipAI AIController
    {
        get { return _aiInput; }
    }
    private ShipAI _aiInput;

    // Ship rigidbody physics
    private ShipPhysics _physics;

    #endregion ship components

    private Ship PortWingman = null, StarboardWingman = null;

    private void Awake()
    {
        _aiInput = GetComponent<ShipAI>();
        _physics = GetComponent<ShipPhysics>();
    }

    // Gets the formation position offset when invoked by an escort ship
    public Vector3 GetWingmanPosition(Ship requestee)
    {
        if (this == requestee)
            return Vector3.zero;

        if (PortWingman == null)            // Port slot not occupied
        {
            Debug.Log("[WINGMAN]: Ship " + requestee.name + " is port wingman for " + name);
            PortWingman = requestee;
            return new Vector3(-WINGMAN_OFFSET, 0, -WINGMAN_OFFSET);
        }
        else if (StarboardWingman == null)  // Starboard slot not occupied
        {
            Debug.Log("[WINGMAN]: Ship " + requestee.name + " is starboard wingman for " + name);
            StarboardWingman = requestee;
            return new Vector3(-WINGMAN_OFFSET, 0, WINGMAN_OFFSET);
        }
        else    // Both slots occupied, ask port wingman 
        {
            return new Vector3(WINGMAN_OFFSET, 0, -WINGMAN_OFFSET) + PortWingman.GetWingmanPosition(requestee);
        }
    }

    public void RemoveWingman(Ship wingman)
    {
        if (PortWingman == wingman)
        {
            PortWingman = null;
        }
        else
        {
            StarboardWingman = null;
        }
    }

}
