using UnityEngine;
using System.Collections;

namespace PrincessAdventure
{

    public class InputManager : MonoBehaviour
    {
        private ControllerTypes _inputName = ControllerTypes.Other;

        private float _interactDownStart;
        private float _magicDownStart;
        private float _bombDownStart;
        private float _holdThreshold = .4f;
        private float _interactCooldown = .2f;

        private void Awake()
        {
            _inputName = getControllerType();
        }

        //Centralized inputs, communicate tasks out to other components
        //Defaulting to ps5 controller for now
        //Use fixed update because we move via Rigidbody & Physics
        void Update()
        {
            CapturePauseInput();

            if (GameManager.GameInstance.GetCurrentGameState() == GameState.Playing)
                CaptureGameplayInput();

        }

        private void CapturePauseInput()
        {
            bool techPause = false;
            bool gamePause = false;

            //settings
            if (Input.GetButtonDown("Settings"))
                techPause = true;

            //inventory & status
            if (Input.GetButtonDown("Status"))
                gamePause = true;

            GameManager.GameInstance.RoutePauseInput(techPause, gamePause);

        }

        private void CaptureGameplayInput()
        {
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
                if (_interactDownStart > 0 && _interactDownStart + _holdThreshold >= Time.time)
                {
                    if(CanInteract())
                    {
                        UpdateInteractCooldown();
                        newInputs.InputInteract = true;

                    }
                    
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
                {
                    UpdateInteractCooldown();
                    newInputs.InputMagicCast = true;
                }
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
            if (Input.GetButtonUp("Bomb"))
            {
                if (_bombDownStart + _holdThreshold >= Time.time)
                    newInputs.InputThrowFireball = true;
                else
                    newInputs.InputDropBomb = true;
                _bombDownStart = 0f;
            }
            if (Input.GetButton("Bomb"))
            {
                if (_bombDownStart + _holdThreshold <= Time.time)
                    newInputs.InputHoldBomb = true;
            }

            if (Input.GetButton("Fade"))
                newInputs.InputFade = true;


            GameManager.GameInstance.RouteInputs(newInputs);
        }

        private ControllerTypes getControllerType()
        {
            //string[] joystickNames = Input.GetJoystickNames();
            //foreach (string name in joystickNames)
            //{
            //    Debug.Log(name);
            //}
            if (Input.GetJoystickNames() == null || Input.GetJoystickNames().Length == 0)
                return ControllerTypes.Other;

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

        private bool CanInteract()
        {
            return _interactCooldown < Time.time;

        }

        private void UpdateInteractCooldown()
        {
            _interactCooldown = Time.time + .5f;

        }
    }
}
