using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject PauseMenuPanel;
    [SerializeField] private GameObject Loading;
    public static bool GameIsPaused = false;


    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }


    }

    public void Pause()
    {

        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }


}
