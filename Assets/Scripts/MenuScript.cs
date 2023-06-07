using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This Script represents the menu at the start of the game and at the end.
public class MenuScript : MonoBehaviour
{
    const int StartMenuIndex = 0;
    const int LvlOneIndex = 2;
    const int tutorialIndex = 1;

    // Moving to the next lvl/scene
    public void NextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        nextSceneIndex =
            nextSceneIndex == StartMenuIndex ? nextSceneIndex = LvlOneIndex : nextSceneIndex;
        SceneManager.LoadScene(nextSceneIndex);
    }

    //Quit the game
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene(tutorialIndex);
    }
}
