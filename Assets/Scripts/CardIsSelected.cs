using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardIsSelected : MonoBehaviour
{
    public GameObject border;

    public void BorderOn()
    {
        border.SetActive(true);
    }
    public void BorderOff()
    {
        border.SetActive(false);
    }
    
    public void CardSelect()
    {

    }
}
