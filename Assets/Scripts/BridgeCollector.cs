using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCollector : MonoBehaviour
{
    #region Fields
    //Declaration of one little piece of bridge and it's copy for instantiation
    [SerializeField] GameObject bridgeShard;
    private GameObject bridgeShardCopy;

    //Declaration of lists, that contain picked up bridge pieces and their local coordinates in bag (behind player)
    private List<GameObject> bridgeShardList = new List<GameObject>();
    private List<Vector3> bridgeShardNewPosition = new List<Vector3>();
    private const int piecesOfBridgeInOneBlock = 5;

    //Declaration of variables that contains size of a backPack and it's position
    private const int firstPieceLocationOnYAxis = 2;
    private int backPackHeight = 0;
    private Vector3 playerBackpackPosition;

    //Declaration of variable, that checks if it's only one collision of trigger and player
    private bool isColliding;
    #endregion

    #region Properties

    public GameObject BridgeShardCopy
    {
        get { return bridgeShardCopy; }
        set { bridgeShardCopy = value; }
    }
    public Vector3 PlayerBackpackPosition
    {
        get { return playerBackpackPosition; }
        set { playerBackpackPosition = value; }
    }
    public List<GameObject> BridgeShardList
    {
        get { return bridgeShardList; }
        set { bridgeShardList = value; }
    }
    public List<Vector3> BridgeShardNewPosition
    {
        get { return bridgeShardNewPosition; }
        set { bridgeShardNewPosition = value; }
    }
    public int BackPackHeight
    {
        get { return backPackHeight; }
        set { backPackHeight = value; }
    }
    public bool IsColliding
    {
        get { return isColliding; }
        set { isColliding = value; }
    }

    #endregion

    #region Methods
    void OnTriggerEnter(Collider other)
    {
        bool playerEnteredTheTrigger = other.gameObject.tag == "BridgeShards";

        if (playerEnteredTheTrigger)
        {
            if (IsColliding)
                return;
            else
            {
                IsColliding = true;
                PickupABlockOfBridgePieces(other);
            }
        }
    }

    //Picks up a block of bridge pieces and moves it to backpack
    void PickupABlockOfBridgePieces(Collider other)
    {
        Destroy(other.gameObject);
        for (int i = 0; i != piecesOfBridgeInOneBlock; i++)
        {
            BridgeShardNewPosition.Add(new Vector3(PlayerBackpackPosition.x, PlayerBackpackPosition.y + BackPackHeight + firstPieceLocationOnYAxis, PlayerBackpackPosition.z));
            BridgeShardCopy = Instantiate(bridgeShard, BridgeShardNewPosition[BridgeShardNewPosition.Count - 1], Quaternion.Euler(0, Random.Range(0, 360), 0));
            BridgeShardList.Add(BridgeShardCopy);
            BridgeShardList[BridgeShardList.Count - 1].transform.parent = transform.GetChild(3).transform;
            BackPackHeight++;
        }
    }

    void Update()
    {
        IsColliding = false;
        PlayerBackpackPosition = gameObject.transform.GetChild(3).transform.localPosition;
        for (int i = 0; i != BridgeShardList.Count; i++)
            BridgeShardList[i].transform.localPosition = BridgeShardNewPosition[i];
    }

    #endregion
}
