using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardProperties : MonoBehaviour
{
<<<<<<< HEAD
    //to get the face of the card
=======
>>>>>>> e95d130ac169ec9216e2d2c44eaffb1b9195a525
    Image testImage;
    public Sprite[] faces;
    public Sprite cardBack;
    public int cardIndex;
<<<<<<< HEAD
    //public DragAndDrop.Slot typeOfItem = DragAndDrop.Slot.HAND;
=======
>>>>>>> e95d130ac169ec9216e2d2c44eaffb1b9195a525

    public void ChangeFace(bool showFace)
    {
        if (showFace)
        {
            cardIndex = Random.Range(0, this.faces.Length - 1);
<<<<<<< HEAD
            //changing the sprite of the face of the card
=======
>>>>>>> e95d130ac169ec9216e2d2c44eaffb1b9195a525
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
