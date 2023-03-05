using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    //Centralized inputs, communicate tasks out to other components
    void Update()
    {
        //settings

        //inventory & status

        //directional
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Debug.Log(horizontalInput + ", " + verticalInput);

        //action (tap vs hold)
        if (Input.GetButtonDown("Interact"))
            Debug.Log("interaction");

        if (Input.GetButtonDown("Magic"))
            Debug.Log("magic");

        if (Input.GetButtonDown("Fade"))
            Debug.Log("fade");

        if (Input.GetButtonDown("Bomb"))
            Debug.Log("boom");


    }
}
