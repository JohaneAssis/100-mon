using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //equation is wpt(ran1)+wh(ran2)+jpt(ran3)+5ct(ran4)-wc(ran5)=$
    private CardProperties face;
    public float worker = 2f;
    public float prod = 1f;
    public float tech = 1f;
    public float hap = 2f;
    public float proj = 0f;
    public float cost = 2f;
    public float world;
    public float tempProfit = 0f;
    public float mainProfit = 0f;
    float ran1, ran2, ran3, ran4, ran5;
    int element;

    void Awake()
    {
        ran1 = Random.Range(0.8f, 1.5f);
        ran2 = Random.Range(0.8f, 1.5f);
        ran3 = Random.Range(0.8f, 1.5f);
        ran4 = Random.Range(0.8f, 1.5f);
        ran5 = Random.Range(0.8f, 1.5f);
        face = GetComponent<CardProperties>();
    }

    //still need to specify that these values are received from the card placed in the play area
    //also need to destroy the card after the card is placed in the play area
    public void getBlueValues()
    {
        switch (face.cardIndex)
        {
            case 0:
                {
                    worker += 1f;
                    tech += 1f;
                    getCombinedValues();
                    break;
                }
            case 1:
                {
                    worker += 1f;
                    prod += 1f;
                    getCombinedValues();
                    break;
                }
            case 2:
                {
                    worker += 1f;
                    hap += 1f;
                    getCombinedValues();
                    break;
                }
            case 3:
                {
                    tech += 1f;
                    prod += 1f;
                    getCombinedValues();
                    break;
                }
            case 4:
                {
                    tech += 1f;
                    hap += 1f;
                    getCombinedValues();
                    break;
                }
            case 5:
                {
                    hap += 1f;
                    prod += 1f;
                    getCombinedValues();
                    break;
                }
            case 6:
                {
                    worker += 2f;
                    cost += 1f;
                    getCombinedValues();
                    break;
                }
            case 7:
                {
                    cost += 1f;
                    prod += 2f;
                    getCombinedValues();
                    break;
                }
            case 8:
                {
                    prod += 1f;
                    tech += 2f;
                    getCombinedValues();
                    break;
                }
            case 9:
                {
                    prod += 1f;
                    hap += 2f;
                    getCombinedValues();
                    break;
                }
            case 10:
                {
                    worker += 2f;
                    tech += 2f;
                    getCombinedValues();
                    break;
                }
            case 11:
                {
                    worker += 2f;
                    hap += 2f;
                    getCombinedValues();
                    break;
                }
            case 12:
                {
                    worker += 3f;
                    cost += 2f;
                    getCombinedValues();
                    break;
                }
            case 13:
                {
                    cost += 2f;
                    tech += 3f;
                    getCombinedValues();
                    break;
                }
            case 14:
                {
                    hap += 3f;
                    cost += 2f;
                    getCombinedValues();
                    break;
                }
            case 15:
                {
                    worker += 2f;
                    prod += 2f;
                    getCombinedValues();
                    break;
                }
            case 16:
                {
                    cost -= 2f;
                    getCombinedValues();
                    break;
                }
            case 17:
                {
                    cost -= 3f;
                    getCombinedValues();
                    break;
                }
            case 18:
                {
                    hap -= 1f;
                    tech += 2f;
                    getCombinedValues();
                    break;
                }
            case 19:
                {
                    worker += 2f;
                    hap -= 1f;
                    getCombinedValues();
                    break;
                }
            case 20:
                {
                    hap -= 1f;
                    cost += 2f;
                    getCombinedValues();
                    break;
                }
            case 21:
                {
                    cost -= 1f;
                    getCombinedValues();
                    break;
                }
        }
    }

    public void getGreyValues()
    {
        switch (face.cardIndex)
        {
            case 0:
                {
                    worker -= 1f;
                    cost += 1f;
                    getCombinedValues();
                    break;
                }
            case 1:
                {
                    worker += 1f;
                    cost += 2f;
                    getCombinedValues();
                    break;
                }
            case 2:
                {
                    cost += 3f;
                    worker += 2f;
                    getCombinedValues();
                    break;
                }
            case 3:
                {
                    cost += 1f;
                    worker -= 2f;
                    getCombinedValues();
                    break;
                }
            case 4:
                {
                    hap -= 2f;
                    worker += 1f;
                    getCombinedValues();
                    break;
                }
            case 5:
                {
                    worker -= 2f;
                    getCombinedValues();
                    break;
                }
            case 6:
                {
                    prod -= 1f;
                    cost += 1f;
                    getCombinedValues();
                    break;
                }
            case 7:
                {
                    cost += 2f;
                    prod += 1f;
                    getCombinedValues();
                    break;
                }
            case 8:
                {
                    prod += 1f;
                    cost += 3f;
                    getCombinedValues();
                    break;
                }
            case 9:
                {
                    hap -= 2f;
                    cost += 1f;
                    getCombinedValues();
                    break;
                }
            case 10:
                {
                    cost += 1f;
                    getCombinedValues();
                    break;
                }
            case 11:
                {
                    prod -= 2f;
                    getCombinedValues();
                    break;
                }
            case 12:
                {
                    tech -= 1f;
                    cost += 1f;
                    getCombinedValues();
                    break;
                }
            case 13:
                {
                    cost += 2f;
                    tech += 1f;
                    getCombinedValues();
                    break;
                }
            case 14:
                {
                    cost += 3f;
                    tech += 1f;
                    getCombinedValues();
                    break;
                }
            case 15:
                {
                    cost += 1f;
                    tech -= 2f;
                    getCombinedValues();
                    break;
                }
            case 16:
                {
                    cost += 2f;
                    getCombinedValues();
                    break;
                }
            case 17:
                {
                    hap -= 2f;
                    getCombinedValues();
                    break;
                }
            case 18:
                {
                    hap -= 1f;
                    cost += 1f;
                    getCombinedValues();
                    break;
                }
            case 19:
                {
                    cost += 2f;
                    hap += 1f;
                    getCombinedValues();
                    break;
                }
            case 20:
                {
                    hap += 1f;
                    cost += 3f;
                    getCombinedValues();
                    break;
                }
            case 21:
                {
                    hap -= 2f;
                    tech += 1f;
                    getCombinedValues();
                    break;
                }
            case 22:
                {
                    cost += 3f;
                    getCombinedValues();
                    break;
                }
            case 23:
                {
                    tech -= 2f;
                    getCombinedValues();
                    break;
                }
        }
    }

    public void getRedValues()
    {
        switch (face.cardIndex)
        {
            case 0:
                {
                    worker += 1f;
                    getCombinedValues();
                    break;
                }
            case 1:
                {
                    worker += 2f;
                    cost += 2f;
                    getCombinedValues();
                    break;
                }
            case 2:
                {
                    cost += 1f;
                    getCombinedValues();
                    break;
                }
            case 3:
                {
                    tech += 1f;
                    worker -= 2f;
                    getCombinedValues();
                    break;
                }
            case 4:
                {
                    hap += 1f;
                    worker -= 2f;
                    getCombinedValues();
                    break;
                }
            case 5:
                {
                    cost -= 2f;
                    prod -= 1f;
                    getCombinedValues();
                    break;
                }
            case 6:
                {
                    worker += 1f;
                    cost += 1f;
                    getCombinedValues();
                    break;
                }
            case 7:
                {
                    cost += 2f;
                    prod += 1f;
                    getCombinedValues();
                    break;
                }
            case 8:
                {
                    worker -= 1f;
                    cost -= 1f;
                    getCombinedValues();
                    break;
                }
            case 9:
                {
                    tech -= 1f;
                    cost -= 1f;
                    getCombinedValues();
                    break;
                }
            case 10:
                {
                    hap -= 1f;
                    cost -= 1f;
                    getCombinedValues();
                    break;
                }
            case 11:
                {
                    worker -= 2f;
                    cost -= 2f;
                    getCombinedValues();
                    break;
                }
            case 12:
                {
                    tech += 1f;
                    cost += 1f;
                    getCombinedValues();
                    break;
                }
            case 13:
                {
                    cost += 2f;
                    tech += 1f;
                    getCombinedValues();
                    break;
                }
            case 14:
                {
                    hap -= 1f;
                    tech += 1f;
                    getCombinedValues();
                    break;
                }
            case 15:
                {
                    worker += 1f;
                    tech -= 1f;
                    getCombinedValues();
                    break;
                }
            case 16:
                {
                    cost -= 2f;
                    getCombinedValues();
                    break;
                }
            case 17:
                {
                    prod += 1f;
                    tech -= 2f;
                    getCombinedValues();
                    break;
                }
            case 18:
                {
                    hap += 1f;
                    cost += 1f;
                    getCombinedValues();
                    break;
                }
            case 19:
                {
                    prod += 1f;
                    hap -= 1f;
                    getCombinedValues();
                    break;
                }
            case 20:
                {
                    prod -= 1f;
                    worker += 1f;
                    getCombinedValues();
                    break;
                }
            case 21:
                {
                    hap -= 2f;
                    prod += 1f;
                    getCombinedValues();
                    break;
                }
            case 22:
                {
                    hap += 1f;
                    cost += 2f;
                    getCombinedValues();
                    break;
                }
            case 23:
                {
                    hap -= 1f;
                    tech += 2f;
                    getCombinedValues();
                    break;
                }
        }
    }

    public void getProjValues()
    {
        switch (face.cardIndex)
        {
            case 0:
                {
                    cost += 1f;
                    proj += 2f;
                    getCombinedValues();
                    break;
                }
            case 1:
                {
                    hap += 1f;
                    proj += 1f;
                    getCombinedValues();
                    break;
                }
            case 2:
                {
                    proj += 1f;
                    tech += 1f;
                    getCombinedValues();
                    break;
                }
            case 3:
                {
                    proj += 2f;
                    prod -= 1f;
                    getCombinedValues();
                    break;
                }
            case 4:
                {
                    hap -= 1f;
                    proj += 1f;
                    getCombinedValues();
                    break;
                }
            case 5:
                {
                    proj += 1f;
                    worker -= 1f;
                    getCombinedValues();
                    break;
                }
            case 6:
                {
                    proj += 1f;
                    getCombinedValues();
                    break;
                }
            case 7:
                {
                    proj += 2f;
                    getCombinedValues();
                    break;
                }
            case 8:
                {
                    proj += 3f;
                    cost += 1f;
                    getCombinedValues();
                    break;
                }
        }
    }

    public void getWorldValues()
    {
        switch (face.cardIndex)
        {
            case 0:
                {
                    tech += 4f;
                    cost += 6f;
                    getCombinedValues();
                    break;
                }
            case 1:
                {
                    cost += 6f;
                    getCombinedValues();
                    break;
                }
            case 2:
                {
                    prod -= 4f;
                    getCombinedValues();
                    break;
                }
            case 3:
                {
                    worker += 4f;
                    cost += 8f;
                    getCombinedValues();
                    break;
                }
            case 4:
                {
                    cost += 4f;
                    getCombinedValues();
                    break;
                }
            case 5:
                {
                    worker -= 4f;
                    getCombinedValues();
                    break;
                }
            case 6:
                {
                    prod += 4f;
                    cost += 10f;
                    getCombinedValues();
                    break;
                }
            case 7:
                {
                    cost += 2f;
                    getCombinedValues();
                    break;
                }
            case 8:
                {
                    tech -= 4f;
                    getCombinedValues();
                    break;
                }
            case 9:
                {
                    hap += 4f;
                    cost += 8f;
                    getCombinedValues();
                    break;
                }
            case 10:
                {
                    proj += 1f;
                    cost += 8f;
                    getCombinedValues();
                    break;
                }
            case 11:
                {
                    hap -= 4f;
                    getCombinedValues();
                    break;
                }
        }
    }

    public void getCombinedValues()
    {
        tempProfit = (worker * prod * tech * ran1) 
            + (worker * hap * ran2) 
            + (proj * prod * tech * ran3) 
            - (5f * cost * tech * ran4) 
            - (worker * cost * ran5);

	}
}
