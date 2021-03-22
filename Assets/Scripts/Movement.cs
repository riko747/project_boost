using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Thrust();
        Rotate();
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
            leftThrustParticle.Play();
            rightThrustParticle.Play();
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(thrustSound);
        }
        else
            audioSource.Stop();
    }

    void Rotate()
    {
        playerOnGround = GetComponent<CollisionHandler>().playerOnGround;
        if (!playerOnGround)
        {
            rigidBody.freezeRotation = true;
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
                rightRotatingThrustParticle.Play();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
                leftRotatingThrustParticle.Play();
            }
            rigidBody.freezeRotation = false;
        }
    }
}
