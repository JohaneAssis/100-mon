using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerExitHandler, IPointerEnterHandler
{
    public DragAndDrop.Slot typeOfItem = DragAndDrop.Slot.NONE;
    public GameObject blueBlocker, redBlocker, greyBlocker, projBlocker;
    public static int discardNum = 0;
    public Score scoreObj;

    public AudioClip discardSound1;
    public AudioClip discardSound2;
    //public Text discardCount;

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerDrag.name + " was testing the drop to " + gameObject.name);

        DragAndDrop d = eventData.pointerDrag.GetComponent<DragAndDrop>();

        //for restricting cards going from one place to another and for the discard pile
        if (d != null)
        {
            if (typeOfItem == d.typeOfItem || typeOfItem == DragAndDrop.Slot.PLAY 
                || typeOfItem == DragAndDrop.Slot.DISCARD)
            {
                d.parentToReturnTo = this.transform;

                if (typeOfItem == DragAndDrop.Slot.DISCARD)
                {                    
                    DropZone.discardNum++;
                    //Debug.Log("DropZone's discardNum "+ DropZone.discardNum);
                    //SetDiscardCountText();
                    foreach (Transform child in transform)
                    {
                        Destroy(child.gameObject);
                    }
                    SoundManager.instance.RandomizeSound(discardSound1, discardSound2);
                }
                
            }
            
        }
        DetermineActivation();
    }

    public void DetermineActivation()
    {
        blueBlocker.SetActive(discardNum <= 1);
        redBlocker.SetActive(discardNum <= 2);
        projBlocker.SetActive(discardNum <= 4);
        greyBlocker.SetActive(Score.greyCountFromDnD >= 3 /*&& GetComponent<Score>().worldBlocker.activeSelf == true*/);
        
        scoreObj.worldBlocker.SetActive(scoreObj.determineNum % 9 != 0);
        int weekNumMinusOne = scoreObj.weekNum - 1;

        if (!scoreObj.worldBlocker.activeSelf)
        {
            scoreObj.allBlocker.SetActive(true);
        }
        else if (scoreObj.weekNum % 9 == 0)
        {
            scoreObj.allBlocker.SetActive(false);
        }
        
        Debug.Log(scoreObj.weekNum % 9 == 0);
        Debug.Log("scoreObj.weekNum is " + scoreObj.weekNum );
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
    
    /*
    public void SetDiscardCountText()
    {
        discardCount.text = DropZone.discardNum.ToString();
    }
    */
}
