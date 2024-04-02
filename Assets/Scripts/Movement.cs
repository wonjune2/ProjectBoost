using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float thrust = 500f;
    [SerializeField] float rotate = 20f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        }
    
    }

    void ProcessRotation()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(rotate);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(-rotate);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(-Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
