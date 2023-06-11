using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text playerNameText;
    [SerializeField] TMP_Text statusText;
    Player player;

    [SerializeField] private GameObject readyButton;
    private LobbyManagement manager;

    Image playerIcon;
    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    /*    public Color highlightColor;*/


    private void Start()
    {
      manager = GameObject
    .FindGameObjectWithTag("LobbyManagement")
    .GetComponent<LobbyManagement>();

        playerProperties["isReady"] = false;
        playerIcon = gameObject.GetComponent<Image>();
    }


    public void SetUp(Player player)
    {
        this.player = player;
        playerNameText.text = player.NickName;
  
      
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(this.player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }



    public void ApplyLocalChanges()
    {
        readyButton.SetActive(true);
    }

    public void OnReadyClicked()
    {
       
        bool isReady = (bool)playerProperties["isReady"];
        if (isReady)
        {
            playerProperties["isReady"] = false;

        }
        else
        {
            playerProperties["isReady"] = true;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }


    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (!(this.player == targetPlayer)) return;

       
            UpdatePlayerStatus(targetPlayer);

        manager.CheckGameReady();

    }

    public void UpdatePlayerStatus(Player player)

    {
        
        if (!player.CustomProperties.ContainsKey("isReady")) return;

        bool isReady = (bool)player.CustomProperties["isReady"];
        if (isReady)
        {
            statusText.text = "Ready";
            statusText.color = Color.green;
        }
        else
        {
            statusText.text = "Not ready";
            statusText.color = Color.red;
        }
    }




}
