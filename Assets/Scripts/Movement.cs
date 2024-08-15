using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1.0f;
    [SerializeField] float rotationThrust = 1.0f;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime, ForceMode.Acceleration);
        }


    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            ApplyRotation(rotationThrust);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThrust)
    {
        rb.AddRelativeTorque(Vector3.forward * rotationThrust * Time.deltaTime, ForceMode.Acceleration);
    }
}
