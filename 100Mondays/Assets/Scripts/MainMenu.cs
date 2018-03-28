using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    Event keyEvent;
    KeyCode newKey;
    Transform optionMenu;
    Text buttonText;

    bool waitingForKey;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LeaveGameBackToMainMenus()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Debug.Log("game quit");
        Application.Quit();
    }

    public void Start()
    {
        optionMenu = transform.Find("Option Menu");
        optionMenu.gameObject.SetActive(false);
        waitingForKey = false;

        for (int i = 0; i < 5; i++)
        {
            if (optionMenu.GetChild(i).name == "flipKey")
            {
                optionMenu.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.FlipCode.ToString();
            }
            else if (optionMenu.GetChild(i).name == "playKey")
            {
                optionMenu.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.PlayCode.ToString();
            }
        }
    }

    public void Update()
    {
        
    }

    public void OnGUI()
    {
        keyEvent = Event.current;

        if(keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    public void SendText(Text text)
    {
        buttonText = text;
    }

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
        {
            yield return null;
        }
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;
        yield return WaitForKey();
        switch (keyName)
        {
            case "flipKey":
                {
                    InputManager.IM.FlipCode = newKey;
                    buttonText.text = InputManager.IM.FlipCode.ToString();
                    PlayerPrefs.SetString("flipKey", InputManager.IM.FlipCode.ToString());
                    break;
                }
            case "playKey":
                {
                    InputManager.IM.PlayCode = newKey;
                    buttonText.text = InputManager.IM.PlayCode.ToString();
                    PlayerPrefs.SetString("playKey", InputManager.IM.PlayCode.ToString());
                    break;
                }
        }
        yield return null;
    }
}
