using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaarthiController : MonoBehaviour
{
    public GameObject Textbox;
   // public GameObject options;
    // public GameObject startTour;
    private GameObject model;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        model = GameObject.FindGameObjectWithTag("Body");
        if(model != null && model.activeSelf)
        {
            TB_remover(Textbox);
        }
        if (Textbox.activeSelf)
        {
          //  options.SetActive(false);
        }
        else
        {
            //options.SetActive(true);
        }
    }

    public void TB_remover( GameObject obj)
    {
        if(obj != null)
        {
            obj.SetActive(false);
        }
    }
    
}
