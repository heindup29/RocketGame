using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period =2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period == Mathf.Epsilon)return;

        var cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        var rawSineWave = Mathf.Sin(tau * cycles);

        movementFactor = (rawSineWave + 1) / 2;

        Vector3 offset = movementFactor * movementVector;
        transform.position = offset + startingPos;
    }
}
