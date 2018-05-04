using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationFadeOut : MonoBehaviour
{
    Image image;

    void Start ()
    {
        image.GetComponent<Image>();
	}
	
	IEnumerable FadeOut()
    {
        for (float f = 1.0f; f >= -0.05f; f -= 0.05f)
        {
            Color c = image.material.color;
            c.a = f;
            image.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartFading()
    {
        StartCoroutine("FadeOut");
    }
}
