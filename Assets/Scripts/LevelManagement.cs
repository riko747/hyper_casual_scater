using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    int currentSceneIndex;
    GameObject player;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        player = GameObject.FindWithTag("Player");
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void RestartLevel()
    {
        Destroy(player);
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
