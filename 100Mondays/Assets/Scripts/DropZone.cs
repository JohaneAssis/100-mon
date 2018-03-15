using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerExitHandler, IPointerEnterHandler
{
    public DragAndDrop.Slot typeOfItem = DragAndDrop.Slot.NONE;
    public int discardNum = 0;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was testing the drop to " + gameObject.name);

        DragAndDrop d = eventData.pointerDrag.GetComponent<DragAndDrop>();

        //for restricting cards going from one place to another and for the discard pile
        if (d != null)
        {
            if (typeOfItem == d.typeOfItem || typeOfItem == DragAndDrop.Slot.PLAY || typeOfItem == DragAndDrop.Slot.DISCARD)
            {
                d.parentToReturnTo = this.transform;
                if (typeOfItem == DragAndDrop.Slot.DISCARD)
                {                 
                    discardNum++;
                    foreach (Transform child in transform)
                    {
                        Destroy(child.gameObject);
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    //for OnPointerEnter and/or OnPointExit might be a place to add the animations to trigger or 
    //play a sound or apply the stats of a card or another thing
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointEnter");
        if (eventData.pointerDrag == null)
        {
            return;
        }

        DragAndDrop d = eventData.pointerDrag.GetComponent<DragAndDrop>();

        if (d != null)
        {
            if (typeOfItem == d.typeOfItem)
            {
                d.placeholderParent = this.transform;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // Debug.Log("OnPointerExit");
        if (eventData.pointerDrag == null)
        {
            return;
        }

        DragAndDrop d = eventData.pointerDrag.GetComponent<DragAndDrop>();

        if (d != null && d.placeholderParent == this.transform)
        {
            if (typeOfItem == d.typeOfItem)
            {
                d.placeholderParent = d.parentToReturnTo;
            }
        }
    }
}
