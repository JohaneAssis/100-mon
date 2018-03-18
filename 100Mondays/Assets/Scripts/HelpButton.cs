using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 28), "Game Help"))
        {
            Debug.Log("help button pressed");
        }
    }
}
