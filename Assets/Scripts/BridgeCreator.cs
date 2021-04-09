using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCreator : MonoBehaviour
{
    BridgeCollector bridgeCollector;

    List<GameObject> bridgeShardList;

    bool playerOnGround;

    GameObject bridgeShardCopy;

    float playerPositionOnY;

    int backpackHeight;
    void Start()
    {
        bridgeCollector = GameObject.FindWithTag("Player").GetComponent<BridgeCollector>();
    }

    void Update()
    {
        bridgeShardList = bridgeCollector.bridgeShardList;
        backpackHeight = bridgeCollector.backPackHeight;
        playerOnGround = GameObject.FindWithTag("Player").GetComponent<CollisionHandler>().playerOnGround;
        playerPositionOnY = transform.position.y;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (bridgeShardList.Count - 1 >= 0 && !playerOnGround)
                {
                    bridgeShardCopy = Instantiate(bridgeShardList[bridgeShardList.Count - 1], new Vector3(transform.position.x, playerPositionOnY + (touch.position.y - Screen.width / 2) / 1500, transform.position.z + 0.6f), Quaternion.Euler(transform.localRotation.x - (touch.position.y - Screen.width / 2) / 1500, transform.localRotation.y, transform.localRotation.z));
                    bridgeShardCopy.transform.localScale = new Vector3(0.5f, 0.05f, 0.5f);
                    bridgeShardCopy.GetComponent<Collider>().isTrigger = false;
                    Destroy(bridgeShardList[bridgeShardList.Count - 1]);
                    bridgeShardList.RemoveAt(bridgeShardList.Count - 1);
                    backpackHeight -= 1;
                }
                else
                    return;
            }
        } 
    }
}
