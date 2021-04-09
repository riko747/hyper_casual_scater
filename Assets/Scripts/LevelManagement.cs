using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Script is responsible for game management
/// </summary>
public class LevelManagement : MonoBehaviour
{
    #region Fields
    private int currentSceneIndex;
    private GameObject player;
    #endregion

    #region Properties
    public int CurrentSceneIndex
    {
        get { return currentSceneIndex; }
        set { currentSceneIndex = value; }
    }
    public GameObject Player
    {
        get { return player; }
        set { player = value; }
    }
    #endregion

    #region Methods
    void Start()
    {
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Player = GameObject.FindWithTag("Player");
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
        Destroy(Player);
        SceneManager.LoadScene(CurrentSceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion;
}
