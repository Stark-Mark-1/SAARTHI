using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveARManager1 : MonoBehaviour
{
    private LiveAR1 placeIndicator;
    public GameObject objectToPlace;
    private GameObject newPlacedObj;
    private GameObject BeforePlacedObj;

    void Start()
    {
        placeIndicator = FindObjectOfType<LiveAR1>();
    }
    public void ClickToPlace()
    {
        if (objectToPlace == null)
        {
            return;
        }
        if (BeforePlacedObj != null)
        {
            newPlacedObj = BeforePlacedObj;
            Instantiate(newPlacedObj, BeforePlacedObj.transform.position, BeforePlacedObj.transform.rotation);
            Destroy(BeforePlacedObj);
        }
        else
        {
            Instantiate(objectToPlace, placeIndicator.transform.position, Quaternion.identity);
        }
    }
}
