using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneBoundaryManager : MonoBehaviour
{
    ARPlaneManager planeManager;

    void Start()
    {
        // Get the ARPlaneManager component
        planeManager = GetComponent<ARPlaneManager>();

        // Subscribe to the planeAdded event to handle newly detected planes
        planeManager.planesChanged += OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
    {
        // Iterate through all added/updated planes
        foreach (var plane in eventArgs.added)
        {
            // Limit the size of the detected plane based on your criteria
            SetPlaneSize(plane);
        }

        foreach (var plane in eventArgs.updated)
        {
            // Limit the size of the updated plane based on your criteria
            SetPlaneSize(plane);
        }
    }

    void SetPlaneSize(ARPlane plane)
    {
        // Example: Limit the size of the plane to a certain threshold
        float maxSize = 5f;
        float currentSize = Mathf.Max(plane.size.x, plane.size.y);

        if (currentSize > maxSize)
        {
            Vector3 newScale = plane.transform.localScale;
            newScale.x *= maxSize / currentSize;
            newScale.y *= maxSize / currentSize;

            plane.transform.localScale = newScale;
        }
    }
}
