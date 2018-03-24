using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager IM;

    public KeyCode FlipCode { get; set; }
    public KeyCode PlayCode { get; set; }

    public void Awake()
    {
        //to stay throughout all scenes
        if (IM == null)
        {
            DontDestroyOnLoad(gameObject);
            IM = this;
        }
        else if(IM != this)
        {
            Destroy(gameObject);
        }
        //gets key from last played game and also assigns default values
        FlipCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("flipKey", "F"));
        PlayCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("playKey", "G"));

    }

    void Start ()
    {
		
	}

	void Update ()
    {
		
	}
}
