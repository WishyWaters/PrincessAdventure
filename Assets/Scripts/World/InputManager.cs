using UnityEngine;
using System.Collections;

namespace PrincessAdventure
{

    public class InputManager : MonoBehaviour
    {
        private ControllerTypes _inputName = ControllerTypes.Other;
        private bool _isPaused = false;
        private CharacterController _charCtrl;

        private float _interactDownStart;
        private float _magicDownStart;
        private float _bombDownStart;
        private float _holdThreshold = .2f;

        private void Awake()
        {
            _inputName = getControllerType();
            _charCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

        }

        //Centralized inputs, communicate tasks out to other components
        //Defaulting to ps5 controller for now
        //Use fixed update because we move via Rigidbody & Physics
        void Update()
        {

            //settings
            if (Input.GetButtonDown("Settings"))
                Debug.Log("Settings");

            //inventory & status
            if (Input.GetButtonDown("Status"))
                Debug.Log("Status");


            PrincessInputActions newInputs = new PrincessInputActions();

            //directional
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            newInputs.MoveAxis = new Vector2(horizontalInput, verticalInput).normalized;


            //Interact - interact OR run hold
            if (Input.GetButtonDown("Interact"))
                _interactDownStart = Time.time;
            if (Input.GetButtonUp("Interact"))
            {
                if (_interactDownStart + _holdThreshold >= Time.time)
                {
                    newInputs.InputInteract = true;
                    //Debug.Log("interact");
                }
                _interactDownStart = 0f;

            }
            if (Input.GetButton("Interact"))
            {
                if (_interactDownStart + _holdThreshold <= Time.time)
                {
                    newInputs.InputRunning = true;
                }
            }

            //Magic - Cast OR Summon
            if (Input.GetButtonDown("Magic"))
                _magicDownStart = Time.time;
            if (Input.GetButtonUp("Magic"))
            {
                if (_magicDownStart + _holdThreshold >= Time.time)
                    newInputs.InputMagicCast = true;
                else 
                    newInputs.InputSummonComplete = true;

                _magicDownStart = 0f;

            }
            if (Input.GetButton("Magic"))
            {
                if (_magicDownStart + _holdThreshold <= Time.time)
                {
                    newInputs.InputSummoning = true;
                }
            }

            if (Input.GetButtonDown("Bomb"))
                _bombDownStart = Time.time;
            if(Input.GetButtonUp("Bomb"))
            {
                if (_bombDownStart + _holdThreshold >= Time.time)
                    newInputs.InputDropBomb = true;
                else
                    newInputs.InputThrowBomb = true;
                _bombDownStart = 0f;
            }
            if(Input.GetButton("Bomb"))
            {
                if (_bombDownStart + _holdThreshold <= Time.time)
                    newInputs.InputHoldBomb = true;
            }

            if (Input.GetButtonDown("Fade"))
                newInputs.InputFade = true;



            _charCtrl.UpdateNextInputs(newInputs);

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
}
