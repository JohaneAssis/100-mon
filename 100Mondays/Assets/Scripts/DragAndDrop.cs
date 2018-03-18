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

    public enum Slot { NONE, HAND, GREY, RED, BLUE, PROJ, WORLD, DISCARD, PLAY };
    public Slot typeOfItem = Slot.HAND;

    public DropZone dropZone;
    public GameObject cardPrefab;
    public Text discardCountDnD;
    public GameObject blueBlocker, redBlocker, greyBlocker, projBlocker, worldBlocker;

    public void Start()
    {
        //DropZone dropZone = transform.parent.GetComponent<DropZone>();
        SetDiscardCountTextDnD();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DebugCard.Instance.SetCard(gameObject);
        //Debug.Log("OnBeginDrag");
        
        if (typeOfItem == Slot.GREY) //can only draw at max, 3 per turn
        {
            Score.greyCountFromDnD += 1;
            Debug.Log("greyCountFromDnD "+ Score.greyCountFromDnD);
            CopyIt();            
            //Debug.Log("DragAndDrop's discardNum " + DropZone.discardNum);
        }
        else if (typeOfItem == Slot.BLUE && DropZone.discardNum >= 2)
        {
            CopyIt();
            DropZone.discardNum -= 2;
            //Debug.Log("DragAndDrop's discardNum " + DropZone.discardNum);
        }
        else if (typeOfItem == Slot.RED && DropZone.discardNum >= 3)
        {
            CopyIt();
            DropZone.discardNum -= 3;
            //Debug.Log("DragAndDrop's discardNum " + DropZone.discardNum);
        }
        else if (typeOfItem == Slot.PROJ && DropZone.discardNum >= 5)
        {
            CopyIt();
            DropZone.discardNum -= 5;
            //Debug.Log("DragAndDrop's discardNum " + DropZone.discardNum);
        }
        else if (typeOfItem == Slot.WORLD)
        {
            CopyIt();
        }

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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");

        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        this.transform.localScale = this.transform.localScale / 1.3f;
        
        Destroy(placeholder);
        SetDiscardCountTextDnD();

        //to reactive as soon as the blockers' parameters are met 
        blueBlocker.SetActive(DropZone.discardNum <= 1);
        redBlocker.SetActive(DropZone.discardNum <= 2);
        projBlocker.SetActive(DropZone.discardNum <= 4);
        greyBlocker.SetActive(Score.greyCountFromDnD >= 3);
        

        if (typeOfItem == Slot.GREY
            || typeOfItem == Slot.BLUE
            || typeOfItem == Slot.RED
            || typeOfItem == Slot.PROJ
            || typeOfItem == Slot.WORLD)
        {
            DragAndDrop.Slot s = parentToReturnTo.GetComponent<DropZone>().typeOfItem;
            typeOfItem = s;
        }

        cardPrefab = GameObject.FindWithTag("greyCard");

        if (typeOfItem == Slot.DISCARD)
        {
            Destroy(cardPrefab);
            //Debug.Log("discarded card");
        }
    }

    public void CopyIt()
    {
        GameObject cardObj = Instantiate(cardPrefab);
        cardObj.transform.SetParent(transform.parent);
        cardObj.transform.position = transform.position;
        cardObj.transform.localScale = Vector3.one;
        typeOfItem = Slot.HAND;
    }
    
    public void SetDiscardCountTextDnD()
    {
        Debug.Log("displaying discard score");
        discardCountDnD.text = DropZone.discardNum.ToString();
    }
    
}
