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
    int currentSceneIndex;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentState = PlayerState.alive;
        isTransitioning = false;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isTransitioning)
            CheckColission(collision);
    }

    /// <summary>
    /// Checking what the rocket collided with
    /// </summary>
    /// <param name="collision"></param>
    void CheckColission(Collision collision)
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

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Launch Pad")
            playerOnGround = false;
    }

    /// <summary>
    /// Collision logic support. Movement disables and checks player state.
    /// </summary>
    void StartCollisionSequence()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();

        if (currentState == PlayerState.alive)
            PrepareToNextLevel();
        else
            PrepareToRestartLevel();
    }

    /// <summary>
    /// Preparing to restarting current level. Playing lose sound and lose particles. Invoking method that restarts current level.
    /// </summary>
    void PrepareToRestartLevel()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(loseSound);
        loseParticleSystem.Play();
        Invoke("RestartLevel", 1f);
    }

    /// <summary>
    /// Preparing to loading next level. Playing win sound and win particles. Invoking method that loads next level.
    /// </summary>
    void PrepareToNextLevel()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(winSound);
        winParticleSystem.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    /// <summary>
    /// If next level exists - load next level. If not - load first level;
    /// </summary>
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }

    /// <summary>
    /// Restarting current level
    /// </summary>
    void RestartLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
}
