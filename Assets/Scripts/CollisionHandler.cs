using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    LevelManagement levelManagementScript;
    GameObject finishPad;

    public bool playerOnGround;
    public bool playerOnFinish;
    public bool playerAlive;
    public bool playerOnBridge;

    [SerializeField] ParticleSystem[] winParticles;

    void Start()
    {
        playerOnFinish = false;
        levelManagementScript = Camera.main.GetComponent<LevelManagement>();
        finishPad = GameObject.FindWithTag("Finish");
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
            levelManagementScript.Invoke("LoadNextLevel", 1f);
            foreach (ParticleSystem winParticle in winParticles)
                winParticle.Play();
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
            levelManagementScript.Invoke("RestartLevel", 1f);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        playerOnGround = false;
    }

    
}
