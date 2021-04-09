using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The script is responsible to managing collision of player with environment
/// </summary>
public class CollisionHandler : MonoBehaviour
{
    #region Fields
    //Declaration of win/lose sounds, win particles and audiosource
    private AudioSource audioSource;
    [SerializeField] ParticleSystem[] winParticles;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;
    //Declaration of LevelManagement class to aññess it
    LevelManagement levelManagementScript;
    //Player state
    private bool playerOnGround;
    private bool playerOnFinish;
    private bool playerOnBridge;

    private const float invokeTimeBeforeLoadLevel = 1f;
    #endregion

    #region Properties
    public AudioSource AudioSource
    {
        get { return audioSource; }
        set { audioSource = value; }
    }
    public LevelManagement LevelManagementScript
    {
        get { return levelManagementScript; }
        set { levelManagementScript = value; }
    }
    public bool PlayerOnGround
    {
        get { return playerOnGround; }
        set { playerOnGround = value; }
    }
    public bool PlayerOnFinish
    {
        get { return playerOnFinish; }
        set { playerOnFinish = value; }
    }
    public bool PlayerOnBridge
    {
        get { return playerOnBridge; }
        set { playerOnBridge = value; }
    }
    #endregion

    #region Methods
    void Start()
    {
        LevelManagementScript = Camera.main.GetComponent<LevelManagement>();
        AudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            PlayerOnGround = true;
        if (collision.gameObject.tag == "BridgeShards")
            PlayerOnBridge = true;
            
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
            PrepareToLoadNextLevel();
        if (collision.gameObject.tag == "Obstacle")
            PrepareToRestartLevel();
    }
    void OnCollisionExit(Collision collision)
    {
        PlayerOnGround = false;
        PlayerOnBridge = false;
    }
    //Preparation to restart current level
    void PrepareToRestartLevel()
    {
        AudioSource.Stop();
        AudioSource.PlayOneShot(loseSound);
        Invoke("InvokeToRestartLevel", 1f);
    }
    //Preparation to load next level
    void PrepareToLoadNextLevel()
    {
        PlayerOnFinish = true;
        AudioSource.Stop();
        AudioSource.PlayOneShot(winSound);
        foreach (ParticleSystem winParticle in winParticles)
            winParticle.Play();
        LevelManagementScript.Invoke("LoadNextLevel", invokeTimeBeforeLoadLevel);
    }
    //Invoking method that restarts current level
    void InvokeToRestartLevel()
    {
        Destroy(gameObject);
        LevelManagementScript.Invoke("RestartLevel", 1f);
    }
    #endregion
}
