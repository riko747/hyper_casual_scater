using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCollector : MonoBehaviour
{
    [SerializeField] GameObject bridgeShard;
    GameObject bridgeShardCopy;

    Vector3 playerBackpackPosition;
    public List<GameObject> bridgeShardList = new List<GameObject>();
    public List<Vector3> bridgeShardNewPosition = new List<Vector3>();

    public int backPackHeight = 0;
    bool isColliding;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BridgeShards")
        {
            if (isColliding) 
                return;
            isColliding = true;
            Destroy(other.gameObject);
            for (int i = 0; i != 5; i++)
            {
                bridgeShardNewPosition.Add(new Vector3(playerBackpackPosition.x, playerBackpackPosition.y + backPackHeight + 2, playerBackpackPosition.z));
                bridgeShardCopy = Instantiate(bridgeShard, bridgeShardNewPosition[bridgeShardNewPosition.Count - 1], Quaternion.Euler(0, Random.Range(0, 360), 0));
                bridgeShardList.Add(bridgeShardCopy);
                bridgeShardList[bridgeShardList.Count - 1].transform.parent = transform.GetChild(3).transform;
                backPackHeight++;
            }
        }
    }

    void Update()
    {
        isColliding = false;
        playerBackpackPosition = gameObject.transform.GetChild(3).transform.localPosition;
        for (int i = 0; i != bridgeShardList.Count; i++)
        {
            bridgeShardList[i].transform.localPosition = bridgeShardNewPosition[i];
        }
    }
}
