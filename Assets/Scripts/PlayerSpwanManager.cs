using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class PlayerSpwanManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    void Awake()
    {
        SpawnPlayer();
    }

 
    public void SpawnPlayer()
    {
        Transform spawnPoint = spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1];
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnPoint.position, Quaternion.identity, 0);
    }

    public Transform GetSpawnPoint(int playerIndex)
    {
        return spawnPoints[playerIndex];
    }


}
