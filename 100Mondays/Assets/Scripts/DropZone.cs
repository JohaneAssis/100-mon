using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerExitHandler, IPointerEnterHandler{

    public DragAndDrop.Slot typeOfItem = DragAndDrop.Slot.HAND;

	public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + "was testing the drop to" + gameObject.name);

        DragAndDrop d = eventData.pointerDrag.GetComponent<DragAndDrop>();

        //for restricting cards going from one place to another
        if (d != null)
        {
            if (typeOfItem == d.typeOfItem || typeOfItem == DragAndDrop.Slot.PLAY || typeOfItem == DragAndDrop.Slot.DISCARD)
            {
                d.parentToReturnTo = this.transform;
            }
        }
    }

    //for OnPointerEnter and/or OnPointExit you might add the animations to trigger or 
    //play a sound or apply the stats of a card or another thing
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("OnPointEnter");
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
