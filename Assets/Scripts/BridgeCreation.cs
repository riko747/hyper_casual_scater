using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCreation : MonoBehaviour
{
    [SerializeField] GameObject player;

    void Update()
    {

        bool playerTouchesScreen = Input.touchCount > 0;

        if (playerTouchesScreen)
        {
            
        }
    }

}
