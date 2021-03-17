using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Launch Pad":
                Debug.Log("You're on " + collision.gameObject.tag + " right now");
                break;
            case "Landing Pad":
                Debug.Log("You're on " + collision.gameObject.tag + " right now");
                break;
            default:
                SceneManager.LoadScene("scene0");
                break;
        }
    }
}
