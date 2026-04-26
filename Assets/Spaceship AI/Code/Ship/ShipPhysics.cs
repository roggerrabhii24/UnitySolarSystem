using UnityEngine;

/// <summary>
/// Applies linear and angular forces to a ship.
/// This is based on the ship physics from https://github.com/brihernandez/UnityCommon/blob/master/Assets/ShipPhysics/ShipPhysics.cs
/// </summary>
public class ShipPhysics : MonoBehaviour
{
    [Tooltip("X: Lateral thrust\nY: Vertical thrust\nZ: Longitudinal Thrust")]
    public Vector3 LinearForce = new Vector3(50.0f, 50.0f, 200.0f);

    [Tooltip("X: Pitch\nY: Yaw\nZ: Roll")]
    public Vector3 AngularForce = new Vector3(75.0f, 75.0f, 25.0f);

    [Range(0.0f, 1.0f)]
    [Tooltip("Multiplier for longitudinal thrust when reverse thrust is requested.")]
    private float _reverseMultiplier = 1.0f;

    [Tooltip("Multiplier for all forces. Can be used to keep force numbers smaller and more readable.")]
    private float _forceMultiplier = 100.0f;

    public Rigidbody Rigidbody { get; private set; }

    private Vector3 _appliedLinearForce = Vector3.zero;
    private Vector3 _appliedAngularForce = Vector3.zero;

    private Vector3 _maxAngularForce;
    private float _rBodyDrag;

    // Keep a reference to the ship this is attached to just in case.
    private Ship _ship;

    // Use this for initialization
    void Awake()
    {
        _ship = GetComponent<Ship>();
        Rigidbody = GetComponent<Rigidbody>();

        _rBodyDrag = Rigidbody.linearDamping;
        _maxAngularForce = AngularForce * _forceMultiplier;
    }

    void FixedUpdate()
    {
        if (Rigidbody != null)
        {
            // Ship is moved by linear (throttle and strafe) and angular (yaw, pitch, roll) forces
            Rigidbody.AddRelativeForce(
                _appliedLinearForce, 
                ForceMode.Force);
            Rigidbody.AddRelativeTorque(
                ClampVector3(_appliedAngularForce, -1 * _maxAngularForce, _maxAngularForce),
                ForceMode.Force);
        }
    }

    private void Update()
    {
        // Read throttle and torque values from ship's AI Controller
        Vector3 linearInput = new Vector3(0, 0, _ship.AIController.throttle);
        _appliedLinearForce = MultiplyByComponent(linearInput, LinearForce) * _forceMultiplier;
        _appliedAngularForce = _ship.AIController.angularTorque;
        _appliedAngularForce.z = 0;
    }

    #region helper methods
    /// <summary>
    /// Returns a Vector3 where each component of Vector A is multiplied by the equivalent component of Vector B.
    /// </summary>
    private Vector3 MultiplyByComponent(Vector3 a, Vector3 b)
    {
        Vector3 ret;

        ret.x = a.x * b.x;
        ret.y = a.y * b.y;
        ret.z = a.z * b.z;

        return ret;
    }

    /// <summary>
    /// Clamps vector components to a value between the minimum and maximum values given in min and max vectors.
    /// </summary>
    /// <param name="vector">Vector to be clamped</param>
    /// <param name="min">Minimum vector components allowed</param>
    /// <param name="max">Maximum vector components allowed</param>
    /// <returns></returns>
    private Vector3 ClampVector3(Vector3 vector, Vector3 min, Vector3 max)
    {
        return new Vector3(
            Mathf.Clamp(vector.x, min.x, max.x),
            Mathf.Clamp(vector.y, min.y, max.y),
            Mathf.Clamp(vector.z, min.z, max.z)
            );
    }
    #endregion helper methods
}