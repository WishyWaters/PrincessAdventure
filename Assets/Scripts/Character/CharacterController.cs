using UnityEngine;
using System.Collections;

namespace PrincessAdventure
{

    public class CharacterController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CustomizableCharacters.CustomizableCharacter _customizableCharacter;
        [SerializeField] private Animator _animator;

        [Header("Settings")]
        [SerializeField] private float _acceleration; //13
        [SerializeField] private float _moveSpeed; //7

        private readonly Vector2[] _directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        private CharacterState _currentState;
        private float _currentAcceleration;
        private Vector2 _lastActiveAxis;
        private Vector2 _currentMovement;
        private int _animatorDirection;
        private Vector2 _previousDirection;
        private GameObject _currentDirectionGameObject;

        //handle movement
        //run or walk animation
        //bomb
        private void Start()
        {
            EnableController();
        }

        public void HandleInputs(PrincessInputActions inputs)
        {
            HandleLocomotion(inputs);
            Move(_currentMovement);
        }

        public void EnableController()
        {
            ResetRigs();

            ChangeState(CharacterState.Neutral);

            _previousDirection = Vector2.zero;
            HandleDirection(new PrincessInputActions());
        }

        private void ResetRigs()
        {
            _customizableCharacter.UpRig.SetActive(false);
            _customizableCharacter.SideRig.SetActive(false);
            _customizableCharacter.DownRig.SetActive(false);
        }

        private void Move(Vector2 amount)
        {
            transform.position += (Vector3)(amount * Time.deltaTime);
        }

        private void HandleLocomotion(PrincessInputActions inputs)
        {
            var accelerationTarget = inputs.InputRunning ? 1f : .5f;
            var maxAcceleration = _acceleration;

            if (inputs.MoveAxis != Vector2.zero
                && (_currentState == CharacterState.Neutral || _currentState == CharacterState.HoldingBomb || _currentState == CharacterState.Moving))
            {
                if (!inputs.InputRunning)
                    maxAcceleration /= 2;

                HandleDirection(inputs);
                _currentAcceleration =
                    Mathf.MoveTowards(_currentAcceleration, accelerationTarget, maxAcceleration * Time.deltaTime);
                _lastActiveAxis = inputs.MoveAxis;
            }
            else
            {
                maxAcceleration /= 2;
                _currentAcceleration = Mathf.MoveTowards(_currentAcceleration, 0, maxAcceleration * Time.deltaTime);
            }

            var targetSpeed = _moveSpeed * _currentAcceleration;
            _currentMovement = _lastActiveAxis * targetSpeed;

            var animatorSpeed = Mathf.InverseLerp(0, _moveSpeed, targetSpeed);
            _animator.SetFloat("Speed", animatorSpeed);
            _animator.SetFloat("Direction", _animatorDirection);
        }

        private void HandleDirection(PrincessInputActions inputs)
        {
            var direction = GetClosestDirection(inputs.MoveAxis);
            if (direction == _previousDirection)
                return;

            _currentDirectionGameObject?.SetActive(false);

            if (direction == Vector2.right)
            {
                _currentDirectionGameObject = _customizableCharacter.SideRig;
                var scale = _customizableCharacter.SideRig.transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                _currentDirectionGameObject.transform.localScale = scale;
                _animatorDirection = 1;
            }
            else if (direction == Vector2.left)
            {
                _currentDirectionGameObject = _customizableCharacter.SideRig;
                var scale = _customizableCharacter.SideRig.transform.localScale;
                scale.x = Mathf.Abs(scale.x) * -1;
                _currentDirectionGameObject.transform.localScale = scale;
                _animatorDirection = 1;
            }
            else if (direction == Vector2.up)
            {
                _currentDirectionGameObject = _customizableCharacter.UpRig;
                _animatorDirection = 0;
            }
            else if (direction == Vector2.down)
            {
                _currentDirectionGameObject = _customizableCharacter.DownRig;
                _animatorDirection = 2;
            }

            _currentDirectionGameObject?.SetActive(true);
            _previousDirection = direction;
        }

        private Vector2 GetClosestDirection(Vector2 from)
        {
            var maxDot = -Mathf.Infinity;
            var ret = Vector3.zero;

            for (int i = 0; i < _directions.Length; i++)
            {
                var t = Vector3.Dot(from, _directions[i]);
                if (t > maxDot)
                {
                    ret = _directions[i];
                    maxDot = t;
                }
            }

            return ret;
        }

        private void ChangeState(CharacterState state)
        {
            _currentState = state;
        }
    }

}
