using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrusterPower = 100f;
    [SerializeField] float thrustRotation = 100f;

    Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessRotation();
        ProcessForwardThrust();
    }

    void ProcessForwardThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrusterPower * Time.deltaTime);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotationThrust(thrustRotation);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotationThrust(-thrustRotation);
        }
    }

    void ApplyRotationThrust(float thurst)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * thurst * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
