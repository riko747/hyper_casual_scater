using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CollisionHandler collisionHandler;
    Rigidbody rigidBody;
    bool playerOnGround;
    bool playerOnFinish;

    float speed;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    

    void Update()
    {
        playerOnGround = collisionHandler.playerOnGround;
        playerOnFinish = collisionHandler.playerOnFinish;
    }

    void FixedUpdate()
    {
        if (playerOnGround)
            speed = 1000;
        else if (!playerOnGround)
            speed = 600;
        rigidBody.AddRelativeForce(Vector3.forward * speed * Time.fixedDeltaTime, ForceMode.Force);
    }
}
