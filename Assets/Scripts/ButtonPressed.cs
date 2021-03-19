using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    float movementSpeed = 0.05f;
    bool buttonIsPressed = false;

    void FixedUpdate()
    {
        if (buttonIsPressed)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(25,0.1f,0), movementSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        buttonIsPressed = true;
    }
}
