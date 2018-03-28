using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
//element 0 - 23 grey, element 24 - 45 blue, element 46 - 69 red
//element 70 - 81 world, element 82 - 90 proj

//didn't end up using this struct but kept it in just in case for future possible implementation,
//commented out some parts for the struct implementation as well,
//and kept it because I already put the 91 card with stats in the inspector set up
public struct CardFace
{
    public Sprite sprite;
    public float worker;
    public float prod;
    public float tech;
    public float hap;
    public float proj;
    public float cost;
}

public class Score : MonoBehaviour
{
    //private CardProperties face;
    public float worker = 2f;
    public float prod = 1f;
    public float tech = 1f;
    public float hap = 2f;
    public float proj = 0f;
    public float cost = 2f;
    public float tempProfit = 0f;
    public float mainProfit = 0f;
    float ran1, ran2, ran3, ran4, ran5;
    public float winNum;
    public Text workerText, prodText, techText, hapText, projText, costText, tempText, mainText;
    int currentIndex;

    public DragAndDrop.Slot typeOfItem = DragAndDrop.Slot.NONE;
    public Transform currentChild;

    public List<CardFace> listOfFaces;

    //CardFace defaultFace = new CardFace();

    public int weekNum = 0;
    public Text weekText;
    public int determineWorldNum, endGameNum;
    int worldBlockNum = 9;
    public static int greyCountFromDnD;
    public GameObject blueBlocker, redBlocker, projBlocker, worldBlocker , greyBlocker;
    public GameObject discardZone, handZone, worldTellPlayer, winScreen, loseScreen;
    public InputField playerInput, numOfWeeks;
    public Text endScreenOfWeeksW, endScreenOfProfitW, endScreenOfTargetProfitW;
    public Text endScreenOfWeeksL, endScreenOfProfitL, endScreenOfTargetProfitL;
    bool findTrueOrFalse;

    void Awake()
    {
        workerText.text = worker.ToString();
        prodText.text = prod.ToString();
        techText.text = tech.ToString();
        hapText.text = hap.ToString();
        projText.text = proj.ToString();
        costText.text = cost.ToString();
        tempText.text = tempProfit.ToString();
        mainText.text = mainProfit.ToString();
        ran1 = Random.Range(0.8f, 1.5f);
        ran2 = Random.Range(0.8f, 1.5f);
        ran3 = Random.Range(0.8f, 1.5f);
        ran4 = Random.Range(0.8f, 1.5f);
        ran5 = Random.Range(0.8f, 1.5f);
        //face = GetComponent<CardProperties>();

        worker = 2f;
        prod = 1f;
        tech = 1f;
        hap = 2f;
        proj = 0f;
        cost = 2f;

        weekText.text = weekNum.ToString();

        /*
        workerText.text = defaultFace.worker.ToString();
        prodText.text = defaultFace.prod.ToString();
        techText.text = defaultFace.tech.ToString();
        hapText.text = defaultFace.tech.ToString();
        projText.text = defaultFace.proj.ToString();
        costText.text = defaultFace.cost.ToString();
        tempText.text = tempProfit.ToString();
        mainText.text = mainProfit.ToString();
        ran1 = Random.Range(0.8f, 1.5f);
        ran2 = Random.Range(0.8f, 1.5f);
        ran3 = Random.Range(0.8f, 1.5f);
        ran4 = Random.Range(0.8f, 1.5f);
        ran5 = Random.Range(0.8f, 1.5f);
        face = GetComponent<CardProperties>();

        defaultFace.worker = 2f;
        defaultFace.prod = 1f;
        defaultFace.tech = 1f;
        defaultFace.hap = 2f;
        defaultFace.proj = 0f;
        defaultFace.cost = 2f;
        */
    }

    public void DetermineWeekBlockers()
    {
        weekNum += 1;
        weekText.text = weekNum.ToString();
        determineWorldNum = weekNum % worldBlockNum;
        /*
            worldBlocker.SetActive(determineWorldNum == 1 || determineWorldNum == 2 || determineWorldNum == 3 ||
            determineWorldNum == 4 || determineWorldNum == 5 || determineWorldNum == 6 || determineWorldNum == 7 ||
            determineWorldNum == 8 || determineWorldNum == 9 || determineWorldNum == 10);
        */
        greyBlocker.SetActive(greyCountFromDnD >= 3);       
    }

    //move the contents of update to activate somewhere else
    void Update()
    {
        if (Input.GetKeyDown(InputManager.IM.PlayCode))
        {
            currentChild = this.gameObject.transform.GetChild(0);
            if (currentChild.gameObject.tag == "greyCard")
            {
                GetGreyValues();
                DeleteUnitAndSetGreyBlocker();
            }
            else if (currentChild.gameObject.tag == "blueCard")
            {
                GetBlueValues();
                DeleteUnitAndSetGreyBlocker();
            }
            else if (currentChild.gameObject.tag == "redCard")
            {
                GetRedValues();
                DeleteUnitAndSetGreyBlocker();
            }
            else if (currentChild.gameObject.tag == "projCard")
            {
                GetProjValues();
                DeleteUnitAndSetGreyBlocker();
            }
            else if (currentChild.gameObject.tag == "worldCard")
            {
                GetWorldValues();
                DeleteUnitAndSetGreyBlocker();
            }
            DetermineWin();
        }
    }

    public void DeleteUnitAndSetGreyBlocker()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject, 1);
        }
        DetermineWeekBlockers();
        if (greyBlocker.activeSelf)
        {
            Score.greyCountFromDnD = 0;
            greyBlocker.SetActive(false);
        }

        if (worldBlocker.activeSelf == false)
        {
            IfWorldActive();
        }
        else if (worldBlocker.activeSelf == true)
        {
            RestoreHandAndDiscard();
        }
    }

    public void GetCombinedValues()
    {
        //Debug.Log("GetCombinedValues() working");
        tempProfit = worker * prod * tech * ran1
            + worker * hap * ran2
            + 3f * proj * prod * tech * ran3
            - 2f * cost * tech * ran4
            - worker * cost * ran5;
        //might need to change the location of where mainProfit is calculated so it doesn't keep incrementing
        mainProfit += tempProfit;
        workerText.text = worker.ToString();
        prodText.text = prod.ToString();
        techText.text = tech.ToString();
        hapText.text = hap.ToString();
        projText.text = proj.ToString();
        costText.text = cost.ToString();
        tempText.text = tempProfit.ToString("F");
        mainText.text = mainProfit.ToString("F");

        weekText.text = weekNum.ToString();

        /*
        Debug.Log("GetCombinedValues() working");
        tempProfit = defaultFace.worker * defaultFace.prod * defaultFace.tech * ran1
            + defaultFace.worker * defaultFace.hap * ran2
            + defaultFace.proj * defaultFace.prod * defaultFace.tech * ran3
            - 5f * defaultFace.cost * defaultFace.tech * ran4
            - defaultFace.worker * defaultFace.cost * ran5;
        //might need to change the location of where mainProfit is calculated so it doesn't keep incrementing
        //mainProfit += tempProfit;
        workerText.text = defaultFace.worker.ToString();
        prodText.text = defaultFace.prod.ToString();
        techText.text = defaultFace.tech.ToString();
        hapText.text = defaultFace.hap.ToString();
        projText.text = defaultFace.proj.ToString();
        costText.text = defaultFace.cost.ToString();
        tempText.text = tempProfit.ToString();
        mainText.text = mainProfit.ToString();
        */
    }

    public void ResetScore()
    {
        ran1 = Random.Range(0.8f, 1.5f);
        ran2 = Random.Range(0.8f, 1.5f);
        ran3 = Random.Range(0.8f, 1.5f);
        ran4 = Random.Range(0.8f, 1.5f);
        ran5 = Random.Range(0.8f, 1.5f);
        worker = 2f;
        prod = 1f;
        tech = 1f;
        hap = 2f;
        proj = 0f;
        cost = 2f;
        tempProfit = 0f;
        mainProfit = 0f;
        GetCombinedValues();
        weekNum = 0;
        /*
        defaultFace.worker = 2f;
        defaultFace.prod = 1f;
        defaultFace.tech = 1f;
        defaultFace.hap = 2f;
        defaultFace.proj = 0f;
        defaultFace.cost = 2f;
        */
    }

    public void GetCurrentIndex()
    {
        currentIndex = currentChild.GetComponent<CardProperties>().cardIndex;
        //Debug.Log("the card is in the card index, " + currentIndex);
    }

    public void IfWorldActive()
    {
        while (worldBlocker.activeSelf == false)
        {            
            greyBlocker.SetActive(true);
            handZone.SetActive(false);
            discardZone.SetActive(false);
            worldTellPlayer.SetActive(true);
            Debug.Log("Score's IfWorldActive working");
        }
    }

    public void RestoreHandAndDiscard()
    {
        handZone.SetActive(true);
        discardZone.SetActive(true);
        worldTellPlayer.SetActive(false);
    }

    /*
    public void DetermineActivation()
    {
        blueBlocker.SetActive(DropZone.discardNum <= 1);
        redBlocker.SetActive(DropZone.discardNum <= 2);
        projBlocker.SetActive(DropZone.discardNum <= 4);
        greyBlocker.SetActive(Score.greyCountFromDnD >= 3 && worldBlocker.activeSelf == true);
    }
    */

    public void GetNum()
    {       
        if (numOfWeeks != null)
        {
            endGameNum = int.Parse(numOfWeeks.text);
        }
        else
        {
            endGameNum = 101;
        }

        if (playerInput != null)
        {
            winNum = float.Parse(playerInput.text);
        }
        else
        {
            winNum = 0.0f;
        }
        Debug.Log("NumOfWeeks is " + numOfWeeks + ". The winNum is " + winNum + ".");
    }

    public void DetermineWin()
    {
        GetNum();
        if(weekNum == endGameNum && winNum < mainProfit)
        {
            winScreen.SetActive(true);
            loseScreen.SetActive(false);
            Debug.Log("DetermineWin case 1");
            endScreenOfWeeksW.text = endGameNum.ToString();
            endScreenOfProfitW.text = mainProfit.ToString("F");
            endScreenOfTargetProfitW.text = winNum.ToString();
        }
        else if (weekNum == endGameNum && winNum > mainProfit)
        {
            winScreen.SetActive(false);
            loseScreen.SetActive(true);
            Debug.Log("DetermineWin case 2");
            endScreenOfWeeksL.text = endGameNum.ToString();
            endScreenOfProfitL.text = mainProfit.ToString("F");
            endScreenOfTargetProfitL.text = winNum.ToString();
        }
        else
        {
            winScreen.SetActive(false);
            loseScreen.SetActive(false);
            Debug.Log("DetermineWin case 3");
        }
        Debug.Log("DetermineWin working");
    }

    public void GetGreyValues()
    {
        //element 0 - 23 grey for struct
        Debug.Log("getting grey card values");
        if (typeOfItem == DragAndDrop.Slot.PLAY && GameObject.FindWithTag("greyCard") == true)
        {
            //for the child of the current object that the script is attached to 
            //get the current cardIndex from the CardProperties script
            GetCurrentIndex();
            switch (currentIndex)
            {
                case 0:
                    {
                        Debug.Log("card 1");
                        worker += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 1:
                    {
                        Debug.Log("card 2");
                        worker += 2f;
                        cost += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 2:
                    {
                        Debug.Log("card 3");
                        cost += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 3:
                    {
                        Debug.Log("card 4");
                        tech += 1f;
                        worker -= 2f;
                        GetCombinedValues();
                        break;
                    }
                case 4:
                    {
                        Debug.Log("card 5");
                        hap += 1f;
                        worker -= 2f;
                        GetCombinedValues();
                        break;
                    }
                case 5:
                    {
                        Debug.Log("card 6");
                        cost -= 2f;
                        prod -= 1f;
                        GetCombinedValues();
                        break;
                    }
                case 6:
                    {
                        Debug.Log("card 7");
                        worker += 1f;
                        cost += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 7:
                    {
                        Debug.Log("card 8");
                        cost += 2f;
                        prod += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 8:
                    {
                        Debug.Log("card 9");
                        worker -= 1f;
                        cost -= 1f;
                        GetCombinedValues();
                        break;
                    }
                case 9:
                    {
                        Debug.Log("card 10");
                        tech -= 1f;
                        cost -= 1f;
                        GetCombinedValues();
                        break;
                    }
                case 10:
                    {
                        Debug.Log("card 11");
                        hap -= 1f;
                        cost -= 1f;
                        GetCombinedValues();
                        break;
                    }
                case 11:
                    {
                        Debug.Log("card 12");
                        worker -= 2f;
                        cost -= 2f;
                        GetCombinedValues();
                        break;
                    }
                case 12:
                    {
                        Debug.Log("card 13");
                        tech += 1f;
                        cost += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 13:
                    {
                        Debug.Log("card 14");
                        cost += 2f;
                        tech += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 14:
                    {
                        Debug.Log("card 15");
                        hap -= 1f;
                        tech += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 15:
                    {
                        Debug.Log("card 16");
                        worker += 1f;
                        tech -= 1f;
                        GetCombinedValues();
                        break;
                    }
                case 16:
                    {
                        Debug.Log("card 17");
                        cost -= 2f;
                        GetCombinedValues();
                        break;
                    }
                case 17:
                    {
                        Debug.Log("card 18");
                        prod += 1f;
                        tech -= 2f;
                        GetCombinedValues();
                        break;
                    }
                case 18:
                    {
                        Debug.Log("card 19");
                        hap += 1f;
                        cost += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 19:
                    {
                        Debug.Log("card 20");
                        prod += 1f;
                        hap -= 1f;
                        GetCombinedValues();
                        break;
                    }
                case 20:
                    {
                        Debug.Log("card 21");
                        prod -= 1f;
                        worker += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 21:
                    {
                        Debug.Log("card 22");
                        hap -= 2f;
                        prod += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 22:
                    {
                        Debug.Log("card 23");
                        hap += 1f;
                        cost += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 23:
                    {
                        Debug.Log("card 24");
                        hap -= 1f;
                        tech += 2f;
                        GetCombinedValues();
                        break;
                    }
            }
        }
    }

    
    public void GetBlueValues()
    {
        //element 24 - 45 blue for struct
        Debug.Log("getting blue card values");
        if (typeOfItem == DragAndDrop.Slot.PLAY && GameObject.FindWithTag("blueCard") == true)
        {
            GetCurrentIndex();
            switch (currentIndex)
            {
                case 0:
                    {
                        worker += 1f;
                        tech += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 1:
                    {
                        worker += 1f;
                        prod += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 2:
                    {
                        worker += 1f;
                        hap += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 3:
                    {
                        tech += 1f;
                        prod += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 4:
                    {
                        tech += 1f;
                        hap += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 5:
                    {
                        hap += 1f;
                        prod += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 6:
                    {
                        worker += 2f;
                        cost += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 7:
                    {
                        cost += 1f;
                        prod += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 8:
                    {
                        prod += 1f;
                        tech += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 9:
                    {
                        prod += 1f;
                        hap += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 10:
                    {
                        worker += 2f;
                        tech += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 11:
                    {
                        worker += 2f;
                        hap += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 12:
                    {
                        worker += 3f;
                        cost += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 13:
                    {
                        cost += 2f;
                        tech += 3f;
                        GetCombinedValues();
                        break;
                    }
                case 14:
                    {
                        hap += 3f;
                        cost += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 15:
                    {
                        worker += 2f;
                        prod += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 16:
                    {
                        cost -= 2f;
                        GetCombinedValues();
                        break;
                    }
                case 17:
                    {
                        cost -= 3f;
                        GetCombinedValues();
                        break;
                    }
                case 18:
                    {
                        hap -= 1f;
                        tech += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 19:
                    {
                        worker += 2f;
                        hap -= 1f;
                        GetCombinedValues();
                        break;
                    }
                case 20:
                    {
                        hap -= 1f;
                        cost += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 21:
                    {
                        cost -= 1f;
                        GetCombinedValues();
                        break;
                    }
            }
        }
    }

    public void GetRedValues()
    {
        //element 46 - 69 red for struct
        Debug.Log("getting red card values");
        if (typeOfItem == DragAndDrop.Slot.PLAY && GameObject.FindWithTag("redCard") == true)
        {
            GetCurrentIndex();
            switch (currentIndex)
            {
                case 0:
                    {
                        worker -= 1f;
                        cost += 1f;
                        GetCombinedValues();

                        break;
                    }
                case 1:
                    {
                        worker += 1f;
                        cost += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 2:
                    {
                        cost += 3f;
                        worker += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 3:
                    {
                        cost += 1f;
                        worker -= 2f;
                        GetCombinedValues();
                        break;
                    }
                case 4:
                    {
                        hap -= 2f;
                        worker += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 5:
                    {
                        worker -= 2f;
                        GetCombinedValues();
                        break;
                    }
                case 6:
                    {
                        prod -= 1f;
                        cost += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 7:
                    {
                        cost += 2f;
                        prod += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 8:
                    {
                        prod += 1f;
                        cost += 3f;
                        GetCombinedValues();
                        break;
                    }
                case 9:
                    {
                        hap -= 2f;
                        cost += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 10:
                    {
                        cost += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 11:
                    {
                        prod -= 2f;
                        GetCombinedValues();
                        break;
                    }
                case 12:
                    {
                        tech -= 1f;
                        cost += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 13:
                    {
                        cost += 2f;
                        tech += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 14:
                    {
                        cost += 3f;
                        tech += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 15:
                    {
                        cost += 1f;
                        tech -= 2f;
                        GetCombinedValues();
                        break;
                    }
                case 16:
                    {
                        cost += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 17:
                    {
                        hap -= 2f;
                        GetCombinedValues();
                        break;
                    }
                case 18:
                    {
                        hap -= 1f;
                        cost += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 19:
                    {
                        cost += 2f;
                        hap += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 20:
                    {
                        hap += 1f;
                        cost += 3f;
                        GetCombinedValues();
                        break;
                    }
                case 21:
                    {
                        hap -= 2f;
                        tech += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 22:
                    {
                        cost += 3f;
                        GetCombinedValues();
                        break;
                    }
                case 23:
                    {
                        tech -= 2f;
                        GetCombinedValues();
                        break;
                    }
            }
        }
    }

    public void GetProjValues()
    {
        //element 82 - 90 proj for struct
        Debug.Log("getting proj card values");
        if (typeOfItem == DragAndDrop.Slot.PLAY && GameObject.FindWithTag("projCard") == true)
        {
            GetCurrentIndex();
            switch (currentIndex)
            {
                case 0:
                    {
                        cost += 1f;
                        proj += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 1:
                    {
                        hap += 1f;
                        proj += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 2:
                    {
                        proj += 1f;
                        tech += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 3:
                    {
                        proj += 2f;
                        prod -= 1f;
                        GetCombinedValues();
                        break;
                    }
                case 4:
                    {
                        hap -= 1f;
                        proj += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 5:
                    {
                        proj += 1f;
                        worker -= 1f;
                        GetCombinedValues();
                        break;
                    }
                case 6:
                    {
                        proj += 1f;
                        GetCombinedValues();
                        break;
                    }
                case 7:
                    {
                        proj += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 8:
                    {
                        proj += 3f;
                        cost += 1f;
                        GetCombinedValues();
                        break;
                    }
            }
        }
    }

    public void GetWorldValues()
    {
        //element 70 - 81 world for struct
        Debug.Log("getting world card values");
        if (typeOfItem == DragAndDrop.Slot.PLAY && GameObject.FindWithTag("worldCard") == true)
        {
            GetCurrentIndex();
            switch (currentIndex)
            {
                case 0:
                    {
                        tech += 4f;
                        cost += 6f;
                        GetCombinedValues();
                        break;
                    }
                case 1:
                    {
                        cost += 6f;
                        GetCombinedValues();
                        break;
                    }
                case 2:
                    {
                        prod -= 4f;
                        GetCombinedValues();
                        break;
                    }
                case 3:
                    {
                        worker += 4f;
                        cost += 8f;
                        GetCombinedValues();
                        break;
                    }
                case 4:
                    {
                        cost += 4f;
                        GetCombinedValues();
                        break;
                    }
                case 5:
                    {
                        worker -= 4f;
                        GetCombinedValues();
                        break;
                    }
                case 6:
                    {
                        prod += 4f;
                        cost += 10f;
                        GetCombinedValues();
                        break;
                    }
                case 7:
                    {
                        cost += 2f;
                        GetCombinedValues();
                        break;
                    }
                case 8:
                    {
                        tech -= 4f;
                        GetCombinedValues();
                        break;
                    }
                case 9:
                    {
                        hap += 4f;
                        cost += 8f;
                        GetCombinedValues();
                        break;
                    }
                case 10:
                    {
                        proj += 1f;
                        cost += 8f;
                        GetCombinedValues();
                        break;
                    }
                case 11:
                    {
                        hap -= 4f;
                        GetCombinedValues();
                        break;
                    }
            }
        }
    }
}

