using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance; //singlton pattern

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject); // there can only be one
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    void onSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1) // we are in the game.
        {
            PhotonNetwork.Instantiate(
                Path.Combine("PhotonPrefabs", "PlayerManager"),
                Vector3.zero,
                Quaternion.identity
            );
        }
    }
}
