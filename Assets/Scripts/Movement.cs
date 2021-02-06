using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrusterPower = 100f;
    [SerializeField] float thrustRotation = 100f;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] ParticleSystem thrustLeft;
    [SerializeField] ParticleSystem thrustRight;
    [SerializeField] ParticleSystem thrustMain;

    Rigidbody rb;
    AudioSource audioSource; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }

    }

    void StopThrusting()
    {
        thrustMain.Stop();
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrusterPower * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSound);
        }
        if (!thrustMain.isPlaying)
        {
            thrustMain.Play();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            LeftRotatingThrust();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RightRotatingThrust();
        }
        else
        {
            thrustRight.Stop();
            thrustLeft.Stop();
        }
    }

    void RightRotatingThrust()
    {
        ApplyRotationThrust(-thrustRotation);
        if (!thrustRight.isPlaying)
        {
            thrustRight.Play();
            thrustLeft.Stop();
        }
    }

    void LeftRotatingThrust()
    {
        ApplyRotationThrust(thrustRotation);
        if (!thrustLeft.isPlaying)
        {
            thrustLeft.Play();
            thrustRight.Stop();
        }
    }

    void ApplyRotationThrust(float thurst)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * thurst * Time.deltaTime);
        rb.freezeRotation = false;       
    }
}
