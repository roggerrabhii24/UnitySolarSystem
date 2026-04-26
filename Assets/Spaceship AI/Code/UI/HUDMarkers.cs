using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDMarkers : Singleton<HUDMarkers> {

    // Current selected target offscreen indicator
    public GameObject OffscreenIndicatorPrefab;
    // For the marker pool
    public GameObject NonselectedIndicatorPrefab;

    private GameObject _offscreenIndicator;
    private Image _currTargetOffscreenMarker;

    public Transform Target { get; set; }

    private Image _currentTargetMarker;

    private Image[] _markerPool;
    private int _markerPoolSize = 30;
    
    private float _hScreenWidth, _hScreenHeight;
    private Dictionary<int, GameObject> _markerObjectMap;

    private void Start()
    {
        _markerObjectMap = new Dictionary<int, GameObject>();
        _hScreenHeight = Screen.height / 2;
        _hScreenWidth = Screen.width / 2;
        _currentTargetMarker = GetComponentInChildren<Image>();

        _markerPool = new Image[_markerPoolSize];

        // Initialize marker pool
        for (int i = 0; i < _markerPoolSize; i++)
        {
            GameObject indicator = GameObject.Instantiate(NonselectedIndicatorPrefab, this.transform);
            _markerPool[i] = indicator.GetComponent<Image>();
            _markerPool[i].enabled = false;

            // Initialize the marker - gameobject map
            _markerObjectMap.Add(i, null);
        }

        _offscreenIndicator = GameObject.Instantiate(OffscreenIndicatorPrefab, transform);
        _currTargetOffscreenMarker = _offscreenIndicator.GetComponent<Image>();
        _offscreenIndicator.SetActive(false);
    }
    
    void Update () {
        // Handle current target on and off-screen markers
		if(Target != null && Target.gameObject.activeInHierarchy)
        {
            DisplayMarker(Target);            
        }
        else
        {
            _currentTargetMarker.enabled = false;
            _offscreenIndicator.SetActive(false);
        }

        GameObject[] objectsInRange = GameObject.FindGameObjectsWithTag("Ship");

        // Pass all objects
        for (int i = 0; i < objectsInRange.Length; i++)
        {
            GameObject obj = objectsInRange[i];
            // Check if obj is already attached to a marker
            bool alreadyUsed = false;
            foreach(var markerObj in _markerObjectMap.Values)
                if(obj == markerObj)
                {
                    alreadyUsed = true;
                    break;
                }

            if (alreadyUsed)
                continue;

            if (IsObjectOnScreen(obj.transform) && obj.transform != Target)
            {
                // Find first available HUD marker
                for (int j = 0; j < _markerPoolSize; j++)
                {
                    if (_markerObjectMap[j] == null)
                    {
                        // Assign marker to onscreen object
                        _markerPool[j].enabled = true;
                        _markerObjectMap[j] = obj;

                        _markerPool[j].GetComponent<NonSelectedHUDMarker>().MarkerTarget = obj;
                        _markerPool[j].rectTransform.localPosition = GetScreenPosOfObject(obj.transform);
                        break;
                    }
                }
            }
        }

        // Pass all markers, turn off unused ones
        for (int j = 0; j < _markerPoolSize; j++)
        {
            if (_markerObjectMap[j] != null)
            {
                GameObject obj = _markerObjectMap[j];
                if (!IsObjectOnScreen(obj.transform) || obj.transform == Target)
                {
                    // Turn off marker
                    _markerPool[j].enabled = false;
                    _markerObjectMap[j] = null;
                }
                else
                {
                    // Update marker position
                    _markerPool[j].rectTransform.localPosition = GetScreenPosOfObject(obj.transform);
                }
            }
            else
            {
                // Turn off marker
                _markerPool[j].enabled = false;
            }
        }


    }

    private void DisplayMarker(Transform target)
    {
        float x = Camera.main.WorldToScreenPoint(target.position).x - _hScreenWidth;
        float y = Camera.main.WorldToScreenPoint(target.position).y - _hScreenHeight;
        float z = Camera.main.WorldToScreenPoint(target.position).z;

        // Check if Target is off-screen            
        if (x < -_hScreenWidth || x > _hScreenWidth || y < -_hScreenHeight || y > _hScreenHeight)
        {
            // Target is off screen
            _currentTargetMarker.enabled = false;

            if (!_offscreenIndicator.activeInHierarchy)
            {
                _offscreenIndicator.SetActive(true);
            }
            else
            {
                if(z>0)
                    _currTargetOffscreenMarker.rectTransform.localPosition = new Vector3(
                        Mathf.Clamp(x, -_hScreenWidth, _hScreenWidth),
                        Mathf.Clamp(y, -_hScreenHeight, _hScreenHeight), 0f);
                else
                    _currTargetOffscreenMarker.rectTransform.localPosition = new Vector3(
                        Mathf.Clamp(x, _hScreenWidth, -_hScreenWidth),
                        Mathf.Clamp(y, _hScreenHeight, -_hScreenHeight), 0f);
            }

        }
        else
        {
            if (z > 0)
            {
                // Target is on screen
                _offscreenIndicator.SetActive(false);

                _currentTargetMarker.enabled = true;
                _currentTargetMarker.rectTransform.localPosition = new Vector3(x, y, 0f);
            }
        }
    }

    private bool IsObjectOnScreen(Transform obj)
    {
        float x = Camera.main.WorldToScreenPoint(obj.position).x;
        float y = Camera.main.WorldToScreenPoint(obj.position).y;
        float z = Camera.main.WorldToScreenPoint(obj.position).z;

        // Check if Target is off-screen            
        if (x < 0 || x > Screen.width || y < 0 || y > Screen.height)
        {
            return false;
        }
        else if (z > 0) // Target is in front of the camera
        {
            return true;
        }
        else // Target is behind the camera
        {
            return false;
        }

    }

    private Vector3 GetScreenPosOfObject(Transform target)
    {
        float x = Camera.main.WorldToScreenPoint(target.position).x - _hScreenWidth;
        float y = Camera.main.WorldToScreenPoint(target.position).y - _hScreenHeight;

        return new Vector3(
            Mathf.Clamp(x, -_hScreenWidth, _hScreenWidth),
            Mathf.Clamp(y, -_hScreenHeight, _hScreenHeight), 0f);
    }

    public void ClearTarget()
    {
        Target = null;
        _currentTargetMarker.enabled = false;
    }
}
