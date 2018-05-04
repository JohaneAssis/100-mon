using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationFade : MonoBehaviour
{
    Image image;

	void Start ()
    {
        image = GetComponent<Image>();
        Color c = image.material.color;
        c.a = 0f;
        image.material.color = c;
	}
	
	IEnumerator FadeIn()
    {
		for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color c = image.material.color;
            c.a = f;
            image.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
	}

    public void StartFading()
    {
        StartCoroutine("FadeIn");
    }
}
