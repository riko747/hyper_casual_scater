using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script is responsible for creating the bridge 
/// </summary>
public class BridgeCreator : MonoBehaviour
{
    #region Fields
    //Accessing BridgeCollector class and it's property to get bridge pieces
    private BridgeCollector bridgeCollector;
    private List<GameObject> bridgeShardList;

    //Declaration of copy of a bridge piece copy for further instantiation
    private GameObject bridgeShardCopy;

    //Initialize scale of spawned bridge pieces
    private const float bridgeShardXScale = 0.5f;
    private const float bridgeShardYScale = 0.05f;
    private const float bridgeShardZScale = 0.5f;

    //Initialize bridge piece spawn point relatively to the player
    private const float accuracyCoefficientOnY = 1500f;
    private const float accuracyCoefficientOnZ = 0.6f;

    //Checking player position on Y axis
    private float playerPositionOnY;

    //Checking player state
    private bool playerOnGround;
    #endregion

    #region Properties
    public BridgeCollector BridgeCollector
    {
        get { return bridgeCollector; }
        set { bridgeCollector = value; }
    }
    public List<GameObject> BridgeShardList
    {
        get { return bridgeShardList; }
        set { bridgeShardList = value; }
    }
    public GameObject BridgeShardCopy
    {
        get { return bridgeShardCopy; }
        set { bridgeShardCopy = value; }
    }
    public float PlayerPositionOnY
    {
        get { return playerPositionOnY; }
        set { playerPositionOnY = value; }
    }
    public bool PlayerOnGround
    {
        get { return playerOnGround; }
        set { playerOnGround = value; }
    }
    #endregion Properties

    #region Methods
    void Start()
    {
        BridgeCollector = GameObject.FindWithTag("Player").GetComponent<BridgeCollector>();
    }

    void Update()
    {
        bool userTouchesScreen = Input.touchCount > 0;

        BridgeShardList = BridgeCollector.BridgeShardList;
        PlayerOnGround = GameObject.FindWithTag("Player").GetComponent<CollisionHandler>().PlayerOnGround;
        PlayerPositionOnY = transform.localPosition.y;

        if (userTouchesScreen)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                bool bridgeShardListIsNotEmpty = BridgeShardList.Count - 1 >= 0;

                if (bridgeShardListIsNotEmpty && !PlayerOnGround)
                    PrepareToSpawnBridgeShard(touch);
                else
                    return;
            }
        } 
    }

    //Declaration and initialization of variables, that responsible for bridge creation
    void PrepareToSpawnBridgeShard(Touch touch)
    {
        Vector3 newBridgeShardPosition;
        Quaternion newBridgeShardRotation;
        int lastElementInList;

        InitializeSpawnData(touch, out newBridgeShardPosition, out newBridgeShardRotation, out lastElementInList);
        SpawnBridgeShardOnMap(newBridgeShardPosition, newBridgeShardRotation, lastElementInList);
        RemoveBridgeShardFromBackpack(lastElementInList);
    }

    //Initialization of spawn point and rotation of bridge piece
    private void InitializeSpawnData(Touch touch, out Vector3 newBridgeShardPosition, out Quaternion newBridgeShardRotation, out int lastElementInList)
    {
        int halfScreenOnWidth = Screen.width / 2;
        //Calculation of position to spawn new bridge piece
        float newBridgeShardPositionX = transform.localPosition.x;
        float newBridgeShardPositionY = PlayerPositionOnY + (touch.position.y - halfScreenOnWidth) / accuracyCoefficientOnY;
        float newBridgeShardPositionZ = transform.localPosition.z + accuracyCoefficientOnZ;
        //Calculation of rotation of new spawned bridge piece
        float newBridgeShardRotationX = transform.localRotation.x - (touch.position.y - halfScreenOnWidth) / accuracyCoefficientOnY;
        float newBridgeShardRotationY = transform.localRotation.y;
        float newBridgeShardRotationZ = transform.localRotation.z;

        newBridgeShardPosition = new Vector3((float)newBridgeShardPositionX, newBridgeShardPositionY, newBridgeShardPositionZ);
        newBridgeShardRotation = Quaternion.Euler(newBridgeShardRotationX, (float)newBridgeShardRotationY, (float)newBridgeShardRotationZ);
        lastElementInList = BridgeShardList.Count - 1;
    }

    //Spawning bridge piece
    void SpawnBridgeShardOnMap(Vector3 newBridgeShardPosition, Quaternion newBridgeShardRotation, int lastElementInList)
    {
        BridgeShardCopy = Instantiate(BridgeShardList[lastElementInList], newBridgeShardPosition, newBridgeShardRotation);
        BridgeShardCopy.transform.localScale = new Vector3(bridgeShardXScale, bridgeShardYScale, bridgeShardZScale);
        BridgeShardCopy.GetComponent<Collider>().isTrigger = false;
    }

    //Clearing last element of list and deleting bridge piece from backpack
    void RemoveBridgeShardFromBackpack(int lastElementInList)
    {
        Destroy(BridgeShardList[lastElementInList]);
        BridgeShardList.RemoveAt(lastElementInList);
    }
    #endregion
}
