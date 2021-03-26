using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField]Vector3 movementVector;
    [Range(0,1)]float movementFactor;
    [SerializeField] float period;

    const float tau = Mathf.PI * 2;

    void Start()
    {
        startingPosition = transform.position;    
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;//one full cycle
        float rawSinWave = Mathf.Sin(cycles * tau); //value between -1 and 1

        movementFactor = (rawSinWave + 1f) / 2f; //value between 0 and 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
