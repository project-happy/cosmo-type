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

    [SerializeField] TMP_Text readyText;
    Player player;

/*    [SerializeField] private GameObject readyButton;*/
    private GameObject readyBtn;
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
        readyBtn = GameObject
       .FindGameObjectWithTag("ReadyButton");

        Button myButton = readyBtn.GetComponent<Button>();
        readyText = readyBtn.GetComponentInChildren<TMP_Text>();
        myButton.onClick.AddListener(OnReadyClicked);


    }


    public void SetUp(Player player)
    {
        this.player = player;
        playerNameText.text = player.NickName;
        UpdatePlayerStatus(player);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
      
        if(this.player == otherPlayer)
        {
            Destroy(gameObject);
            manager.CheckGameReady();
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }



    public void ApplyLocalChanges()
    {
     /*   readyBtn.SetActive(true);*/
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
        if (this.player != targetPlayer) return;


        Debug.Log("Players List Updated");
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
            readyText.text = "UNREADY";
        }
        else
        {
            statusText.text = "Not ready";
            statusText.color = Color.red;
            readyText.text = "READY";
        }
    }




}
