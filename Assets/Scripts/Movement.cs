using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float thrust = 500f;
    [SerializeField] float rotate = 20f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    void ProcessRotation()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            RightStartThrusting();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            LeftStartThrusting();
        }
        else
        {
            SideThrustingStop();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    void SideThrustingStop()
    {
        rightBooster.Stop();
        leftBooster.Stop();
    }

    void LeftStartThrusting()
    {
        ApplyRotation(-rotate);
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
    }

    void RightStartThrusting()
    {
        ApplyRotation(rotate);
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(-Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
