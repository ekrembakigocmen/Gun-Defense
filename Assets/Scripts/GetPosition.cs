using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPosition : MonoBehaviour
{
    public SelectionFilter selectionFilter;
    public void ShopButtonPosition()
    {

        selectionFilter.transform.position = transform.position;
        selectionFilter.transform.GetComponent<Image>().enabled = true;

    }
    
   
   
}
