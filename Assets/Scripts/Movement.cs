using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player movement logic.
/// </summary>
public class Movement : MonoBehaviour
{
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;
    [SerializeField] ParticleSystem leftRotatingThrustParticle;
    [SerializeField] ParticleSystem rightRotatingThrustParticle;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] float thrustSpeed;
    [SerializeField] float rotationSpeed;

    Rigidbody rigidBody;
    AudioSource audioSource;
    bool playerOnGround;
   
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        ProcessingThrust();
        ProcessingRotate();
    }

    /// <summary>
    /// Preparing to thrust.
    /// </summary>
    void ProcessingThrust()
    {
        if (Input.GetKey(KeyCode.Space))
            Thrust();
        else
            audioSource.Stop();
    }

    /// <summary>
    /// Thrusting.
    /// </summary>
    void Thrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        leftThrustParticle.Play();
        rightThrustParticle.Play();
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(thrustSound);
    }

    /// <summary>
    /// Processing to rotate.
    /// </summary>
    void ProcessingRotate()
    {
        playerOnGround = GetComponent<CollisionHandler>().playerOnGround;
        if (!playerOnGround)
            Rotating();
    }

    /// <summary>
    /// Rotating.
    /// </summary>
    void Rotating()
    {
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
            RotatingLeft();
        else if (Input.GetKey(KeyCode.D))
            RotatingRight();
        rigidBody.freezeRotation = false;
    }

    void RotatingRight()
    {
        transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        leftRotatingThrustParticle.Play();
    }

    void RotatingLeft()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        rightRotatingThrustParticle.Play();
    }
}
