using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo;
    public Transform placeholderParent;

    public GameObject placeholder;
    public GameObject copyCard;
    GameObject copyCardPrefab;

<<<<<<< HEAD
    public enum Slot { NONE, HAND, GREY, RED, BLUE, PROJ, WORLD, DISCARD, PLAY };
    public Slot typeOfItem = Slot.HAND;
    //public List<RaycastResult> raycastResults;
    public GameObject cardPrefab;
    public int discardVal;
    private DropZone dropZone;

    public void Start()
    {
        dropZone = transform.parent.GetComponent<DropZone>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        discardVal = dropZone.discardNum;
        DebugCard.Instance.SetCard(gameObject);
        //Debug.Log("OnBeginDrag");
        //if type of the card is grey, blue, red, proj, world then create a copy of it
        if (typeOfItem == Slot.GREY
            || typeOfItem == Slot.BLUE //&& discardVal >= 2 
            || typeOfItem == Slot.RED //&& discardVal >= 3
            || typeOfItem == Slot.PROJ //&& discardVal >= 5
            || typeOfItem == Slot.WORLD)
            {
                GameObject cardObj = Instantiate(cardPrefab);
                cardObj.transform.SetParent(transform.parent);
                cardObj.transform.position = transform.position;
                cardObj.transform.localScale = Vector3.one;
                typeOfItem = Slot.HAND;
            }
=======
    public enum Slot { HAND, GREY, RED, BLUE, PROJ, WORLD, DISCARD, PLAY };
    public Slot typeOfItem = Slot.HAND;
    //public List<RaycastResult> raycastResults;

    public GameObject card;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
>>>>>>> e95d130ac169ec9216e2d2c44eaffb1b9195a525

        Debug.Log("created placeholder");
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        Debug.Log("set parent of placeholder");

        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;
        Debug.Log("set height and width varibales of placeholder");

        this.transform.localScale = this.transform.localScale * 1.3f;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        Debug.Log("setting the sibiling of placeholder");

        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);
<<<<<<< HEAD
        Debug.Log("setting the parent that the card returns to for the card beinging moved");
        GetComponent<CanvasGroup>().blocksRaycasts = false;  
        
=======
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        Instantiate(card);
>>>>>>> e95d130ac169ec9216e2d2c44eaffb1b9195a525
        // might loop through each of the zones while beinging to Drag to make a glow 
        //that will show where the card are valid to play
        //DropZone[] zones = GameObject.FindObjectsOfType<DropZone>();
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
        //Debug.Log("Moved the placeholder");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Debug.Log("set the parent of the card to where the card is placed");

        this.transform.localScale = this.transform.localScale / 1.3f;
        
        Destroy(placeholder);
        Debug.Log("Destroyed the placeholder");
 
        if (typeOfItem == Slot.GREY
            || typeOfItem == Slot.BLUE
            || typeOfItem == Slot.RED
            || typeOfItem == Slot.PROJ
            || typeOfItem == Slot.WORLD)
            {
                Debug.Log(parentToReturnTo.name);
                DragAndDrop.Slot s = parentToReturnTo.GetComponent<DropZone>().typeOfItem;
                Debug.Log(s);
                typeOfItem = s;
            }

        cardPrefab = GameObject.FindWithTag("greyCard");

<<<<<<< HEAD
        if (typeOfItem == Slot.DISCARD)
        {
            Destroy(cardPrefab);
            Debug.Log("discarded card");
        }

        //might be for helping with making the cards disappear when its being played in the play area, 
=======
        card = GameObject.FindWithTag("greyCard");
        if (typeOfItem == DragAndDrop.Slot.DISCARD)
        {
            Destroy(card);
        }

        //for helping with making the cards disappear when being played in the play area, 
>>>>>>> e95d130ac169ec9216e2d2c44eaffb1b9195a525
        //targeting the drop box and changing stats
        //EventSystem.current.RaycastAll(eventData, raycastResults);
    }
}
