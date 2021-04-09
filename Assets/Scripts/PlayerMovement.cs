using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script is responsible for player movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    #region Fields
    [SerializeField] CollisionHandler collisionHandler;
    
    private Rigidbody rigidBody;

    private float speedOnGround = 1000;
    private float speedOnBridge = 2000;
    private float speedOnFly = 600;

    private bool playerOnGround;
    private bool playerOnBridge;

    float speed;
    #endregion

    #region Properties
    public Rigidbody Rigidbody
    {
        get { return rigidBody; }
        set { rigidBody = value; }
    }
    public float SpeedOnGround
    {
        get { return speedOnGround; }
        set { speedOnGround = value; }
    }
    public float SpeedOnBridge
    {
        get { return speedOnBridge; }
        set { speedOnBridge = value; }
    }
    public float SpeedOnFly
    {
        get { return speedOnFly; }
        set { speedOnFly = value; }
    }
    public bool PlayerOnGround
    {
        get { return playerOnGround; }
        set { playerOnGround = value; }
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
        Rigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        PlayerOnGround = collisionHandler.PlayerOnGround;
        PlayerOnBridge = collisionHandler.PlayerOnBridge;
    }

    void FixedUpdate()
    {
        SetSpeed();
        Rigidbody.AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Force);
    }

    void SetSpeed()
    {
        if (PlayerOnGround)
            speed = SpeedOnGround;
        else if (PlayerOnBridge)
            speed = SpeedOnBridge;
        else if (!PlayerOnGround && !playerOnBridge)
            speed = SpeedOnFly;
    }
    #endregion
}
