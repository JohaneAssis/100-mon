using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DebugCard : MonoBehaviour
{
    public static DebugCard Instance;
    bool checkZone = false;
    DragAndDrop.Slot zone;

    //current place that activates the flip
    CardFlip flip;
    CardProperties cardProp;
    int cardIndex = 0;
    int count = 0;
    PointerEventData eventData;

    public GameObject card;

    private void Start()
    {
        DebugCard.Instance = this;
    }

    void Awake()
    {
        SetCard(card);
    }

    public void SetCard(GameObject c)
    {
        card = c;
        cardProp = card.GetComponent<CardProperties>();
        flip = card.GetComponent<CardFlip>();
        count = 0;

    }
    void OnGUI()
    {
        if (Input.GetKeyDown(InputManager.IM.FlipCode))
        {
            DragAndDrop checkSlot = card.GetComponent<DragAndDrop>();
            if (checkSlot.typeOfItem == DragAndDrop.Slot.HAND)
            {
                checkZone = true;
            }
            count++;

            if (checkZone == true)
            {
                if (count <= 1)
                {
                    if (cardIndex >= cardProp.faces.Length)
                    {
                        cardIndex = 0;
                        flip.FlipCard(cardProp.faces[cardProp.faces.Length - 1], cardProp.cardBack, -1);
                    }
                    else
                    {
                        if (cardIndex > 0)
                        {
                            flip.FlipCard(cardProp.faces[cardIndex - 1], cardProp.faces[cardIndex], cardIndex);
                        }
                        else
                        {
                            flip.FlipCard(cardProp.cardBack, cardProp.faces[cardIndex], cardIndex);
                        }
                        cardIndex++;
                    }
                }
            }
        }
    }
}