using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;

    [SerializeField] ParticleSystem winParticleSystem;
    [SerializeField] ParticleSystem loseParticleSystem;

    AudioSource audioSource;
   
    PlayerState currentState;

    const float levelLoadDelay = 1f;
    bool isTransitioning;
    public bool playerOnGround;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentState = PlayerState.alive;
        isTransitioning = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isTransitioning)
        {
            switch (collision.gameObject.tag)
            {
                case "Launch Pad":
                    playerOnGround = true;
                    break;
                case "Button":
                    StartCollisionSequence();
                    break;
                default:
                    currentState = PlayerState.dead;
                    StartCollisionSequence();
                    break;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Launch Pad")
            playerOnGround = false;
    }

    void StartCollisionSequence()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();

        if (currentState == PlayerState.alive)
        {
            isTransitioning = true;
            audioSource.PlayOneShot(winSound);
            winParticleSystem.Play();
            Invoke("LoadNextLevel", levelLoadDelay);
        }
        else
        {
            isTransitioning = true;
            audioSource.PlayOneShot(loseSound);
            loseParticleSystem.Play();
            Invoke("ReloadLevel", 1f);
        }
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(currentSceneIndex);
    }
}
