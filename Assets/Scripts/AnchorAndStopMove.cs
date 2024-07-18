using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AnchorAndStopMove : MonoBehaviour
{
    private Vector3 initialPosition;
    private XROrigin xrOrigin;
    private ARPlaneManager arPlaneManager;

    void Start()
    {
        // Get reference to XROrigin
        xrOrigin = FindObjectOfType<XROrigin>();

        // Store the initial position relative to XROrigin
        if (xrOrigin != null)
        {
            initialPosition = xrOrigin.transform.InverseTransformPoint(transform.position);
        }
        else
        {
            Debug.LogError("XROrigin not found. Make sure your AR settings are configured correctly.");
        }

        // Get reference to ARPlaneManager
        arPlaneManager = FindObjectOfType<ARPlaneManager>();

        if (arPlaneManager == null)
        {
            Debug.LogError("ARPlaneManager not found. Make sure AR settings are configured correctly.");
        }
    }

    void Update()
    {
        // Update the position of the GameObject relative to XROrigin
        if (xrOrigin != null)
        {
            transform.position = xrOrigin.transform.TransformPoint(initialPosition);
        }
    }

    void DisablePlaneTracking()
    {
        // Disable ARPlaneManager to stop plane detection
        if (arPlaneManager != null)
        {
            arPlaneManager.enabled = false;
        }
    }

    void EnablePlaneTracking()
    {
        // Enable ARPlaneManager to resume plane detection
        if (arPlaneManager != null)
        {
            arPlaneManager.enabled = true;
        }
    }
}
