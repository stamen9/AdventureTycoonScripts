using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HideOnLeaveHover : MonoBehaviour
    , IPointerExitHandler
    //, IPointerEnterHandler
{

    /*public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.SetActive(true);
    }*/

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit button");
        this.gameObject.SetActive(false);
    }

}
