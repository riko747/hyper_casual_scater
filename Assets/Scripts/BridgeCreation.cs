using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCreation : MonoBehaviour
{
    /*[SerializeField]CollisionHandler collisionHandler;

    int bridgeShardListSize;
    List<GameObject> bridgeShardList;

    bool playerTouchesScreen;
    void Start()
    {
        collisionHandler = GameObject.FindWithTag("Player").GetComponent<CollisionHandler>();
    }

    void Update()
    {
        bridgeShardListSize = collisionHandler.bridgeShardList.Count;
        playerTouchesScreen = Input.touchCount > 0;
        bridgeShardList = collisionHandler.bridgeShardList;

        if (playerTouchesScreen)
        {
            bridgeShardList[bridgeShardListSize - 1].transform.position = GameObject.FindWithTag("Player").transform.position;
            if (bridgeShardListSize != 0)
                bridgeShardList.RemoveAt(bridgeShardListSize - 1);
            else
                bridgeShardList.RemoveAt(0);

        }
    }*/

}
