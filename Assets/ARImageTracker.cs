using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageTracker : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject modelPrefab; // Assign your 3D model prefab in the inspector

    private GameObject spawnedModel;

    void Start()
    {
        if (trackedImageManager == null)
        {
            trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        }

        if (trackedImageManager != null)
        {
            trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }
        else
        {
            Debug.LogError("ARTrackedImageManager not found. Make sure AR Foundation package is installed.");
        }
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateModel(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateModel(trackedImage);
        }
    }

    void UpdateModel(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            if (spawnedModel == null)
            {
                spawnedModel = Instantiate(modelPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
            }
            else
            {
                spawnedModel.transform.position = trackedImage.transform.position;
                spawnedModel.transform.rotation = trackedImage.transform.rotation;
            }
        }
        else if (trackedImage.trackingState == TrackingState.Limited || trackedImage.trackingState == TrackingState.None)
        {
            if (spawnedModel != null)
            {
                // Handle model visibility or other actions when tracking is limited or lost
                // For example, you might want to deactivate or hide the model
                spawnedModel.SetActive(false);
            }
        }
    }
}