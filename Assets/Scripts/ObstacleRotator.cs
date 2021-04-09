using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script is responsible for rotating obstacles logic
/// </summary>
public class ObstacleRotator : MonoBehaviour
{
    #region Fields;
    private float rotationOnXAxis = 0f;
    private float rotationOnYAxis = -100f;
    private float rotationOnZAxis = 0f;
    #endregion

    #region Properties
    public float RotationOnXAxis
    {
        get { return rotationOnXAxis; }
        set { rotationOnXAxis = value; }
    }
    public float RotationOnYAxis
    {
        get { return rotationOnYAxis; }
        set { rotationOnYAxis = value; }
    }
    public float RotationOnZAxis
    {
        get { return rotationOnZAxis; }
        set { rotationOnZAxis = value; }
    }
    #endregion

    #region Methods
    void Update()
    {
        transform.Rotate(rotationOnXAxis, rotationOnYAxis * Time.deltaTime, rotationOnZAxis);
    }
    #endregion
}
