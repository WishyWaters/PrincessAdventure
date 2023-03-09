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

        private PrincessState _currentState;
        private float _currentAcceleration;
        private Vector2 _lastActiveAxis;
        private Vector2 _currentMovement;
        private int _animatorDirection;
        private Vector2 _previousDirection;
        private GameObject _currentDirectionGameObject;
        private Coroutine _currentCoroutine;


        //handle movement
        //run or walk animation
        //bomb
        private void Start()
        {
            EnableController();
        }

        public void HandleInputs(PrincessInputActions inputs)
        {
            //Start New Action
            if (_currentState == PrincessState.Neutral || _currentState == PrincessState.Moving)
            {
                PowerStance(false);
                //check inputs for new action
                //interact
                if (inputs.InputInteract)
                    HandleInteraction();
                else if (inputs.InputMagicCast)
                    HandleMagicCast(inputs);
                else if (inputs.InputSummoning)
                    HandleSummoning();
                else if (inputs.InputDropBomb)
                    HandleBombDrop(inputs);
                else if (inputs.InputHoldBomb)
                    HandleHoldBomb();
            }
            else if (_currentState == PrincessState.Summon)
            {
                PowerStance(true);
                if (inputs.InputSummonComplete)
                    HandleSummonCompleted(inputs);
            }
            else if(_currentState == PrincessState.HoldingBomb)
            {
                PowerStance(true);
                if (inputs.InputThrowBomb)
                    HandleThrowBomb(inputs);
            } else
            {
                PowerStance(false);
            }

            //Can always fade
            if (inputs.InputFade)
                HandleFade();

            HandleLocomotion(inputs);
        }

        public void EnableController()
        {
            ResetRigs();

            ChangeState(PrincessState.Neutral);

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
                && (_currentState == PrincessState.Neutral || _currentState == PrincessState.HoldingBomb || _currentState == PrincessState.Moving))
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

            Move(_currentMovement);
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

        private void ChangeState(PrincessState state)
        {
            _currentState = state;
        }

        private void PowerStance(bool isActive)
        {
            _animator.SetBool("Showing Weapon", isActive);
        }

        private void HandleInteraction()
        {

        }

        private void HandleMagicCast(PrincessInputActions inputs)
        {
            Debug.Log("Attempt Cast");
            ChangeState(PrincessState.MagicCast);

            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(DoMagicCast(inputs));
        }

        private void HandleSummoning()
        {
            Debug.Log("Start Summon");
            ChangeState(PrincessState.Summon);
        }

        private void HandleSummonCompleted(PrincessInputActions inputs)
        {
            Debug.Log("Attempt Summon");


            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(DoSummonCast(inputs));
        }

        private void HandleBombDrop(PrincessInputActions inputs)
        {
            Debug.Log("Drop bomb");


            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(DoBomb(inputs));

        }

        private void HandleHoldBomb()
        {
            Debug.Log("Hold bomb");
            ChangeState(PrincessState.HoldingBomb);
        }

        private void HandleThrowBomb(PrincessInputActions inputs)
        {
            Debug.Log("Throw Bomb");


            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(DoBomb(inputs));


        }

        private void HandleFade()
        {
            Debug.Log("Fade");
        }


        #region Coroutines

        private IEnumerator DoMagicCast(PrincessInputActions inputs)
        {
            
            _animator.SetTrigger("Attack 1");

            // make sure each attack is going in the direction player is pressing
            if (inputs.MoveAxis != Vector2.zero)
            {
                _lastActiveAxis = inputs.MoveAxis;
                HandleDirection(inputs);
                _currentAcceleration = 1.2f;
            }
            else
                _currentAcceleration = 0.8f;

            // wait for animator state to change to attack
            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") == false)
                yield return null;

            _animator.ResetTrigger("Attack 1");


            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1")
                       && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                // prevent too early attack input
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
                    inputs.InputMagicCast = false;

                yield return null;
            }

            
            ChangeState(PrincessState.Neutral);
        }

        private IEnumerator DoSummonCast(PrincessInputActions inputs)
        {

            _animator.SetTrigger("Stab");

            // make sure each attack is going in the direction player is pressing
            if (inputs.MoveAxis != Vector2.zero)
            {
                _lastActiveAxis = inputs.MoveAxis;
                HandleDirection(inputs);
                _currentAcceleration = 1.2f;
            }
            else
                _currentAcceleration = 0.8f;

            // wait for animator state to change to attack
            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Stab") == false)
                yield return null;

            _animator.ResetTrigger("Stab");


            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Stab")
                       && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                // prevent too early attack input
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
                    inputs.InputSummonComplete = false;

                yield return null;
            }


            ChangeState(PrincessState.Neutral);
        }

        private IEnumerator DoBomb(PrincessInputActions inputs)
        {

            _animator.SetTrigger("Spell");

            // make sure each attack is going in the direction player is pressing
            if (inputs.MoveAxis != Vector2.zero)
            {
                _lastActiveAxis = inputs.MoveAxis;
                HandleDirection(inputs);
                _currentAcceleration = 1.2f;
            }
            else
                _currentAcceleration = 0.8f;

            // wait for animator state to change to attack
            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Spell") == false)
                yield return null;

            _animator.ResetTrigger("Spell");


            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Stab")
                       && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                // prevent too early attack input
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
                    inputs.InputSummonComplete = false;

                yield return null;
            }


            ChangeState(PrincessState.Neutral);
        }


        #endregion
    }

}
