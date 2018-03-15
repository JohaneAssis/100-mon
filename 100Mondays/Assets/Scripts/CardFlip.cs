using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.UI;

//[ExecuteInEditMode]
public class CardFlip : MonoBehaviour
{
    
    //basically flipping the card to a random face 
    SpriteRenderer spriteRenderer;
    CardProperties prop;
    public Sprite endImage;
    //Image image;
=======

public class CardFlip : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CardProperties prop;
>>>>>>> e95d130ac169ec9216e2d2c44eaffb1b9195a525

    public AnimationCurve scaleCurve;
    public float dur = 0.5f;

    public void FlipCard(Sprite startImage, Sprite endImage, int cardIndex)
    {
        StopCoroutine(Flip(startImage, endImage, cardIndex));
        StartCoroutine(Flip(startImage, endImage, cardIndex));
    }

    IEnumerator Flip(Sprite startImage, Sprite endImage, int cardIndex)
    {
        float time = 0f;
        startImage = spriteRenderer.sprite;

        while (time <= 1f)
        {
            float scale = scaleCurve.Evaluate(time);
            time = time + Time.deltaTime / dur;

            Vector3 localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;
<<<<<<< HEAD
            
            if (time >= 0.1f)
            {
                endImage = spriteRenderer.sprite;
=======

            if (time >= 0.5f)
            {
                spriteRenderer.sprite = endImage;
>>>>>>> e95d130ac169ec9216e2d2c44eaffb1b9195a525
            }

            yield return new WaitForFixedUpdate();
        }

        if (cardIndex == -1)
        {
            prop.ChangeFace(false);
        }
        else
        {
            prop.cardIndex = cardIndex;
            prop.ChangeFace(true);
        }
    }

<<<<<<< HEAD
    /*
    public void FlipFace()
    {
        Debug.Log("hey");
        image.sprite = endImage;
    }
    */
    void Awake()
    {
        //image = GetComponent<Image>();
=======
    void Awake()
    {
>>>>>>> e95d130ac169ec9216e2d2c44eaffb1b9195a525
        spriteRenderer = GetComponent<SpriteRenderer>();
        prop = GetComponent<CardProperties>();
    }
}