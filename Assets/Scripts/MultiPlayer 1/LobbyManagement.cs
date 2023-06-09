using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

//this class represents the lobby manager on the game.

public class LobbyManagement : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField createInput;

    [SerializeField]
    private TMP_InputField joinInput;

    //when create btn clicked, create room
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createInput.text);
    }

    //when join btn clicked, join room
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    //when player join load the scene
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MultiDemo");
    }
}
