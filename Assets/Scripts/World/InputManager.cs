using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private ControllerTypes _inputName = ControllerTypes.Other;

    private void Awake()
    {
        _inputName = getControllerType();
        //Debug.Log(_inputName);
    }

    //Centralized inputs, communicate tasks out to other components
    //Defaulting to ps5 controller for now
    void Update()
    {
        //settings
        if (Input.GetButtonDown("Settings"))
            Debug.Log("Settings");

        //inventory & status
        if (Input.GetButtonDown("Status"))
            Debug.Log("Status");

        //directional
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0f || verticalInput != 0f)
            Debug.Log(horizontalInput + ", " + verticalInput);

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

    private ControllerTypes getControllerType()
    {
        //string[] joystickNames = Input.GetJoystickNames();
        //foreach (string name in joystickNames)
        //{
        //    Debug.Log(name);
        //}

        string joystickName = Input.GetJoystickNames()[0].ToLower();

        if (joystickName.Contains("xbox"))
        {
            return ControllerTypes.Xbox;
        }
        else if (joystickName.Contains("playstation") || joystickName.Contains("sony"))
        {
            return ControllerTypes.Ps;
        }
        else
        {
            return ControllerTypes.Other;
        }
    }
}
