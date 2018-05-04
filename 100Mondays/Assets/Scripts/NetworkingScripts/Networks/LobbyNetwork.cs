using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour
{

	void Start ()
    {
        print("connecting to server");
        PhotonNetwork.ConnectUsingSettings("0.0.0");
	}
	
    void OnConnectedToMaster()
    {
        print("connected to master");
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    void OnJoinedLobby()
    {
        print("joinned Lobby");
        if (!PhotonNetwork.inRoom)
        {
            MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();
        }
    }
}
