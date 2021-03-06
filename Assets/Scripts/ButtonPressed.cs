using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    float movementSpeed = 1f;
    float buttonPressedYPosition = 0.4f;
    bool buttonIsPressed = false;

    Vector3 startingPosition;
    Vector3 targetPosition;
    void Start()
    {
        startingPosition = transform.position;
        targetPosition = new Vector3(startingPosition.x, startingPosition.y - buttonPressedYPosition, startingPosition.z);
    }

    void FixedUpdate()
    {
        if (buttonIsPressed)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        buttonIsPressed = true;
    }
}
