using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("Distance from ship to camera position")]
    public Vector3 CameraOffset;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private bool _isTracking = false;
    private Transform _target;

    private void Start()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (_isTracking)
        {
            if (HUDMarkers.Instance.Target == null)
            {
                _isTracking = false;
                return;
            }

            // Keep constant distance to target
            Vector3 targetPosition = HUDMarkers.Instance.Target.position;
            transform.position = targetPosition + CameraOffset;
        }
    }

    public void ResetCameraPosition()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
    }

    public void ToggleTracking()
    {
        if (HUDMarkers.Instance.Target == null)
            return;

        _isTracking = !_isTracking;
        if (_isTracking)
        {
            transform.position = HUDMarkers.Instance.Target.position + CameraOffset;
            transform.rotation = Quaternion.LookRotation(HUDMarkers.Instance.Target.position - transform.position, transform.up);
        }
        else
        { 
            ResetCameraPosition();
        }
    }
}
