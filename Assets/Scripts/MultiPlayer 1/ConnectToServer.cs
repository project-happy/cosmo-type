using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Connect();
    }



    //connect to the server
    private void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //callback when connect to the server
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    //callback when we succssfully joined the lobby in the server
    // load the lobby scene
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("MultiLobby");
    }


}
