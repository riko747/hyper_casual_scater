using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotator : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, -100 * Time.deltaTime, 0);
    }
}
