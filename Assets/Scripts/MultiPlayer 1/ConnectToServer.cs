using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

        Connect();
    }


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
