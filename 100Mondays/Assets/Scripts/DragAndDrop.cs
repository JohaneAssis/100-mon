using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Transform parentToReturnTo = null;
    public Transform placeholderParent = null;

    public GameObject placeholder = null;

    public enum Slot { HAND, GRAY, RED, BLUE, PROJ, WORLD, DISCARD, PLAY };
    public Slot typeOfItem = Slot.HAND;
    public List<RaycastResult> raycastResults;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //change size of card to larger size
        Debug.Log("OnBeginDrag");

        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        this.transform.localScale = this.transform.localScale * 1.3f;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        // might loop through each of the zones while beinging to Drag to make a glow 
        //that will show where the card are valid to play
        DropZone[] zones = GameObject.FindObjectsOfType<DropZone>();
	}

    public void OnDrag(PointerEventData eventData)
    {
        int newSiblingIndex = parentToReturnTo.childCount;

        //Debug.Log("OnDrag");
        this.transform.position = eventData.position;

        for(int i = 0; i < placeholderParent.childCount; i++ )
        {
            if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                if (placeholderParent.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }
                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //change size of card to be back to default
        Debug.Log("OnEndDrag");
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        this.transform.localScale = this.transform.localScale / 1.3f;

        Destroy(placeholder);

        //for helping with making the cards disappear when being played in the play area, 
        //targeting the drop box and changing stats
        EventSystem.current.RaycastAll(eventData, raycastResults);
    }

}
