using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidBody;

    [SerializeField] float thrustSpeed;
    [SerializeField] float rotationSpeed;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Thrust();
        Rotate();
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
            rigidBody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
    }

    void Rotate()
    {
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        rigidBody.freezeRotation = false;
    }
}
