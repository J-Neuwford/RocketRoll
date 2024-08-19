using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 10.0f;

    void Start()
    {
        startingPosition = transform.position;    
    }

    void Update()
    {
        if (period == Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2.0f;
        float rawSinWave = Mathf.Sin(cycles * tau); ;

        // sin wave value mapped from range -1,1 to 0,1
        movementFactor = (rawSinWave + 1f) / 2;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
