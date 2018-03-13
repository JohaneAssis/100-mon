using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CardProperties prop;

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

            if (time >= 0.5f)
            {
                spriteRenderer.sprite = endImage;
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

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        prop = GetComponent<CardProperties>();
    }
}