using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    enum livingState { alive, dead }

    livingState currentState;
    AudioSource audioSource;

    const float levelLoadDelay = 1f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentState = livingState.alive;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Launch Pad":
                break;
            case "Landing Pad":
                StartCollisionSequence();
                break;
            default:
                currentState = livingState.dead;
                StartCollisionSequence();
                break;
        }
    }

    void StartCollisionSequence()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();

        if (currentState == livingState.alive)
            Invoke("LoadNextLevel", levelLoadDelay);
        else
            Invoke("ReloadLevel", 1f);
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
