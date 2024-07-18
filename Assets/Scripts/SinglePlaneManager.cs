using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SinglePlaneManager :MonoBehaviour
{
    ARPlaneManager planeManager;

    void Start()
    {
        // Get the ARPlaneManager component
        planeManager = GetComponent<ARPlaneManager>();
    }

    void Update()
    {
        // Ensure only one plane is active
        ActivateSinglePlane();
    }

    void ActivateSinglePlane()
    {
        // Iterate through all tracked planes
        foreach (ARPlane plane in planeManager.trackables)
        {
            // Activate the current plane and deactivate others
            plane.gameObject.SetActive(plane == GetClosestPlane());
        }
    }

    ARPlane GetClosestPlane()
    {
        // Find the closest plane based on some criteria (e.g., distance)
        ARPlane closestPlane = null;
        float closestDistance = float.MaxValue;

        foreach (ARPlane plane in planeManager.trackables)
        {
            float distance = Vector3.Distance(transform.position, plane.transform.position);

            if (distance < closestDistance)
            {
                closestPlane = plane;
                closestDistance = distance;
            }
        }

        return closestPlane;
    }
}

