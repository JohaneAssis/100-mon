using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardProperties : MonoBehaviour
{
    //to get the face of the card
    Image testImage;
    public Sprite[] faces;
    public Sprite cardBack;
    public int cardIndex;
    //public DragAndDrop.Slot typeOfItem = DragAndDrop.Slot.HAND;

    public void ChangeFace(bool showFace)
    {
        if (showFace)
        {
            cardIndex = Random.Range(0, this.faces.Length - 1);
            //changing the sprite of the face of the card
            testImage.sprite = faces[cardIndex];
        }
        else
        {
            testImage.sprite = cardBack;
        }
    }

    void Awake()
    {
        testImage = GetComponent<Image>();
    }
}
