using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugCard : MonoBehaviour
{
    CardFlip flip;
    CardProperties cardProp;
    int cardIndex = 0;

    public GameObject card;

    void Awake()
    {
        cardProp = card.GetComponent<CardProperties>();
        flip = card.GetComponent<CardFlip>();
    }

    void OnGUI()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(cardIndex >= cardProp.faces.Length)
            {
                cardIndex = 0;
                flip.FlipCard(cardProp.faces[cardProp.faces.Length - 1], cardProp.cardBack, -1);
            }
            else
            {
                if(cardIndex > 0)
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
