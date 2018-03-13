using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardProperties : MonoBehaviour
{
    Image testImage;
    public Sprite[] faces;
    public Sprite cardBack;
    public int cardIndex;

    public void ChangeFace(bool showFace)
    {
        if (showFace)
        {
            cardIndex = Random.Range(0, this.faces.Length - 1);
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
