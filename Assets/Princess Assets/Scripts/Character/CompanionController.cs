using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace PrincessAdventure
{
    public class CompanionController : MonoBehaviour
    {

        [Header("References")]
        [SerializeField] SkeletonAnimation _monsterAnimator;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private InteractController _interactCtrl;
        [SerializeField] private Transform _spriteTransform;

        [Header("Settings")]
        [SerializeField] private int _companionId;
        [SerializeField] private CompanionPower _powerType;
        [SerializeField] private float _acceleration; //9
        [SerializeField] private float _moveSpeed; //6

        private readonly Vector2[] _directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        private CompanionState _currentState;
        private Vector2 _previousDirection;
        private string _animationPrefix = "Front_";
        //private GameObject _currentDirectionGameObject;
        private Coroutine _currentCoroutine;

        private PrincessInputActions _nextInputs;
        private PrincessInputActions _currentInputs;
        private bool _ignoreInputs;

        private float _currentAcceleration;
        private Vector2 _lastActiveAxis;
        private Vector2 _currentMovement;

        // Start is called before the first frame update
        void Start()
        {
            EnableController();
        }

        void FixedUpdate()
        {
            _currentInputs = _nextInputs;
            _nextInputs = new PrincessInputActions();

            if (!_ignoreInputs)
                HandleInputs(_currentInputs);
        }

        public int GetCompanionId()
        {
            return _companionId;
        }

        public void UpdateNextInputs(PrincessInputActions inputs)
        {
            _nextInputs.MoveAxis = inputs.MoveAxis;

            if (inputs.InputDropBomb)
                _nextInputs.InputDropBomb = true;
            if (inputs.InputFade)
                _nextInputs.InputFade = true;
            if (inputs.InputHoldBomb)
                _nextInputs.InputHoldBomb = true;
            if (inputs.InputInteract)
                _nextInputs.InputInteract = true;
            if (inputs.InputMagicCast)
                _nextInputs.InputMagicCast = true;
            if (inputs.InputRunning)
                _nextInputs.InputRunning = true;
            if (inputs.InputSummonComplete)
                _nextInputs.InputSummonComplete = true;
            if (inputs.InputSummoning)
                _nextInputs.InputSummoning = true;
            if (inputs.InputThrowFireball)
                _nextInputs.InputThrowFireball = true;


        }

        public void SetIngoreInputs(bool ignore)
        {
            _ignoreInputs = ignore;
        }

        public void SetColor(Color color)
        {
            _monsterAnimator.skeleton.SetColor(color);
        }

        public void Teleport(Vector2 directionToFace, Vector2 position)
        {
            _rigidbody.velocity = Vector3.zero;
            this.transform.position = position;

            _nextInputs.MoveAxis = directionToFace;
        }

        private void EnableController()
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            ChangeState(CompanionState.Neutral);

            _nextInputs = new PrincessInputActions();
            _nextInputs.MoveAxis = new Vector2(0, -.1f);
            _currentInputs = new PrincessInputActions();
            _ignoreInputs = false;
            HandleDirection(_nextInputs);
            UpdateAnimation("Idle", true);
        }


        private void HandleInputs(PrincessInputActions inputs)
        {
            //Start New Action
            if (_currentState == CompanionState.Neutral || _currentState == CompanionState.Moving)
            {
                ////TODO: check inputs for new action

                if (inputs.InputInteract)
                    HandleInteraction();
                else if (inputs.InputMagicCast)
                    HandleUnsummon();
                else if (inputs.InputThrowFireball)
                    HandleUnsummon();
                //else if (inputs.InputFade)
                //    HandleSpecial(inputs);
            }

            HandleLocomotion(inputs);
        }


        private void Move(Vector2 amount)
        {
            Vector2 nextPosition = _rigidbody.position;
            nextPosition += amount * Time.deltaTime;

            _rigidbody.MovePosition(nextPosition);

        }

        private void HandleLocomotion(PrincessInputActions inputs)
        {
            var accelerationTarget = inputs.InputRunning ? 1f : .5f;
            var maxAcceleration = _acceleration;

            if (inputs.MoveAxis != Vector2.zero
                && (_currentState == CompanionState.Neutral || _currentState == CompanionState.Moving))
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

            if(targetSpeed > 0)
            {
                _currentMovement = _lastActiveAxis * targetSpeed;
                Move(_currentMovement);
                UpdateAnimation("Walk", true);

            } else
            {
                UpdateAnimation("Idle", true);
            }
        }


        private void HandleDirection(PrincessInputActions inputs)
        {
            var direction = GetClosestDirection(inputs.MoveAxis);
            if (direction == _previousDirection)
                return;

            
            _interactCtrl.UpdateCompanionOffset(direction);


            if (direction == Vector2.right)
            {
                float scaleX = Mathf.Abs(_spriteTransform.localScale.x);

                _monsterAnimator.skeleton.SetSkin("Side");
                _monsterAnimator.skeleton.SetSlotsToSetupPose();
                _spriteTransform.localScale = new Vector3(scaleX, _spriteTransform.localScale.y, _spriteTransform.localScale.z);
                _animationPrefix = "Side_";

            }
            else if (direction == Vector2.left)
            {
                float scaleX = Mathf.Abs(_spriteTransform.localScale.x) * -1;
                _monsterAnimator.skeleton.SetSkin("Side");
                _monsterAnimator.skeleton.SetSlotsToSetupPose();
                _spriteTransform.localScale = new Vector3(scaleX, _spriteTransform.localScale.y, _spriteTransform.localScale.z);
                _animationPrefix = "Side_";

            }
            else if (direction == Vector2.up)
            {
                _monsterAnimator.skeleton.SetSkin("Back");
                _monsterAnimator.skeleton.SetSlotsToSetupPose();
                _animationPrefix = "Back_";

            }
            else if (direction == Vector2.down)
            {
                _monsterAnimator.skeleton.SetSkin("Front");
                _monsterAnimator.skeleton.SetSlotsToSetupPose();
                _animationPrefix = "Front_";

            }

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

        private void ChangeState(CompanionState state)
        {
            _currentState = state;
        }

        private void UpdateAnimation(string animationName, bool loop)
        {
            animationName = _animationPrefix + animationName;

            if (_monsterAnimator.AnimationName == animationName)
                return;

            _monsterAnimator.AnimationState.SetAnimation(0, animationName, loop);
        }

        private void HandleInteraction()
        {
            _interactCtrl.AttemptInteraction();

        }

        private void HandleUnsummon()
        {
            //Set fire rabbit to timed explosion

            GameManager.GameInstance.ActivatePrincess(true);
            
        }
    }
}