using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;
    LevelManagement levelManagementScript;

    public bool playerOnGround;
    public bool playerOnFinish;
    public bool playerAlive;
    public bool playerOnBridge;
    bool playerIsTranscending;

    [SerializeField] ParticleSystem[] winParticles;

    void Start()
    {
        levelManagementScript = Camera.main.GetComponent<LevelManagement>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            playerOnGround = true;
        if (collision.gameObject.tag == "BridgeShards")
            playerOnBridge = true;
            
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            playerOnFinish = true;
            audioSource.Stop();
            audioSource.PlayOneShot(winSound);
            foreach (ParticleSystem winParticle in winParticles)
                winParticle.Play();
            levelManagementScript.Invoke("LoadNextLevel", 1f);
            
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            playerIsTranscending = true;
            audioSource.Stop();
            audioSource.PlayOneShot(loseSound);
            Invoke("PrepareToRestart", 1f);
        }
    }

    void PrepareToRestart()
    {
        Destroy(gameObject);
        levelManagementScript.Invoke("RestartLevel", 1f);
    }

    void OnCollisionExit(Collision collision)
    {
        playerOnGround = false;
    }

    
}
