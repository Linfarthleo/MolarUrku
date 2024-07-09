using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointerManager : MonoBehaviour
{
    public static CameraPointerManager Instance;

    [SerializeField] private GameObject pointer;
    [SerializeField] private float maxDistancePointer = 4.5f;
    [Range(0, 1)]
    [SerializeField] private float disPointerObject = 0.95f;

    private const float _maxDistance = 100;
    private GameObject _gazedAtObject = null;
    private bool _isPointerOverInteractable = false;

    private readonly string interactableTag = "Interactable";
    private float scaleSize = 0.025f;

    [HideInInspector]
    public Vector3 hitPoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GazeManager.Instance.OnGazeSelection += GazeSelection;
    }

    private void GazeSelection()
    {
        if (_isPointerOverInteractable)
        {
            _gazedAtObject?.SendMessage("OnPointerClickXR");
        }
    }

    private void Update()
    {
        // Update the pointer position and rotation to follow the camera
        pointer.transform.position = transform.position + transform.forward * maxDistancePointer;
        pointer.transform.rotation = transform.rotation;

        // Raycast to detect objects
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            hitPoint = hit.point;
            if (_gazedAtObject != hit.transform.gameObject)
            {
                UpdatePointerState(hit.transform.gameObject);
            }

            if (hit.transform.CompareTag(interactableTag))
            {
                PointerOnGaze(hit.point);
            }
            else
            {
                PointerOutGaze();
            }
        }
        else
        {
            UpdatePointerState(null);
        }

        // Check for screen touches
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            if (_isPointerOverInteractable)
            {
                _gazedAtObject?.SendMessage("OnPointerClickXR");
            }
        }
    }

    private void UpdatePointerState(GameObject newGazedObject)
{
    if (_gazedAtObject != null && _gazedAtObject.activeInHierarchy)
    {
        _gazedAtObject.SendMessage("OnPointerExitXR");
        _isPointerOverInteractable = false;
    }

    _gazedAtObject = newGazedObject;

    if (_gazedAtObject != null && _gazedAtObject.activeInHierarchy)
    {
        _gazedAtObject.SendMessage("OnPointerEnterXR");
        _isPointerOverInteractable = true;
        GazeManager.Instance.StartGazeSelection();
    }
    else
    {
        GazeManager.Instance.CancelGazeSelection();
    }
}


    private void PointerOnGaze(Vector3 hitPoint)
{
    float distance = Vector3.Distance(transform.position, hitPoint);
    float scaleFactor = Mathf.Min(scaleSize * distance, 1.0f);
    pointer.transform.localScale = Vector3.one * scaleFactor;
    pointer.transform.position = CalculatePointerPosition(transform.position, hitPoint, disPointerObject);
}



    private void PointerOutGaze()
    {
        pointer.transform.localScale = Vector3.one * 0.1f;
        pointer.transform.parent.transform.localPosition = new Vector3(0, 0, maxDistancePointer);
        pointer.transform.parent.parent.transform.rotation = transform.rotation;
        GazeManager.Instance.CancelGazeSelection();
        _isPointerOverInteractable = false;
    }

    private Vector3 CalculatePointerPosition(Vector3 p0, Vector3 p1, float t)
{
    return Vector3.Lerp(p0, p1, t);  // Usa Lerp para una interpolación más segura y limpia
}

}