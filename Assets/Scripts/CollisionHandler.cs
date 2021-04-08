using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject bridgeShard;
    GameObject bridgeShardCopy;

    Vector3 playerBackpackPosition;

    public List<GameObject> bridgeShardList = new List<GameObject>();
    public List<Vector3> bridgeShardNewPosition = new List<Vector3>();
    LevelManagement levelManagementScript;

    public bool playerOnGround;
    public bool playerOnFinish;
    public bool playerAlive;

    int backPackHeight = 0;

    public static int bridgeBlocks = 0;

    [SerializeField] ParticleSystem[] winParticles;

    void Start()
    {
        playerOnFinish = false;
        levelManagementScript = Camera.main.GetComponent<LevelManagement>();
        playerBackpackPosition = gameObject.transform.GetChild(3).transform.localPosition;
    }

    void OnCollisionStay(Collision collision)
    {
        playerOnGround = true;
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BridgeShards")
        {
            bridgeBlocks += 1;
            Destroy(other.gameObject);
            for (int i = 0; i != 5 ; i++)
            {
                bridgeShardCopy = Instantiate(bridgeShard, playerBackpackPosition, Quaternion.Euler(0, Random.Range(0,360), 0));
                bridgeShardCopy.transform.parent = gameObject.transform.GetChild(3).transform;
                bridgeShardList.Add(bridgeShardCopy);
                bridgeShardNewPosition.Add(new Vector3(playerBackpackPosition.x, playerBackpackPosition.y + backPackHeight + 1, playerBackpackPosition.z));
                backPackHeight++;
            }
        }
    }

    void Update()
    {
        for (int i = 0; i != bridgeShardNewPosition.Count; i++)
        {
            bridgeShardList[i].transform.localPosition = bridgeShardNewPosition[i];
        }
    }


    void OnCollisionExit(Collision collision)
    {
        playerOnGround = false;
    }

    
}
