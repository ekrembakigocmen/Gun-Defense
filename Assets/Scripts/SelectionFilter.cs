using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectionFilter : MonoBehaviour
{
    public GameObject filter;
    

    public void HideFilter()
    {
       
        filter.transform.GetComponent<Image>().enabled = false;
    }

   
}
