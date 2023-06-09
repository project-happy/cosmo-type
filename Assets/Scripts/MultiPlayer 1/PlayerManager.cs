using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine) // if the photon view owend by the local player
        {
            CreateController();
        }
    }

    //create a player at random position
    void CreateController()
    {
        Vector2 randomPostion = new Vector2(Random.Range(-7, 9), -4.75f);
        PhotonNetwork.Instantiate(
            Path.Combine("PhotonPrefabs", "PlayerController"),
            randomPostion,
            Quaternion.identity
        );
    }
}
