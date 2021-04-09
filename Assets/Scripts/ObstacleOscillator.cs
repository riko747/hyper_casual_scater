using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script is responsible for smooth movement of an obstacles
/// </summary>
public class ObstacleOscillator : MonoBehaviour
{
    #region Fields
    [Range(0, 1)] float movementFactor;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period;

    const float tau = Mathf.PI * 2;

    private Vector3 startingPosition;
    #endregion

    #region Properties
    public Vector3 StartingPosition
    {
        get { return startingPosition; }
        set { startingPosition = value; }
    }
    #endregion

    #region Methods
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; //one full cycle
        float rawSinWave = Mathf.Sin(cycles * tau); //value between -1 and 1

        movementFactor = (rawSinWave + 1f) / 2f; //value between 0 and 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = StartingPosition + offset;
    }
    #endregion
}
