using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class EnemyBehaviorController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private EnemySoundController _sfxCtrl;
        [SerializeField] private EnemyAnimateController _animateCtrl;
        [SerializeField] private EnemyActionController _actionCtrl;
        [SerializeField] private TreasureExplosion _treasure;

        [Header("Core Settings")]
        [SerializeField] private LayerMask _whatIsPlayer;
        [SerializeField] private LayerMask _whatIsBlockade;

        [Header("Behavior Settings")]
        [SerializeField] private EnemyPatrolTypes _patrolType;
        [SerializeField] private float _patrolTime;
        [SerializeField] private float _idleTimeAfterPatrol;
        [SerializeField] private float _fleeTime;

        [Header("Movement Settings")]
        [SerializeField] private float _acceleration; //8+
        [SerializeField] private float _moveSpeed; //2 to 7
        [SerializeField] private Vector2 _startDirection;
        [SerializeField] private float _sightRadius;

        private float _stateEndTime;
        private EnemyStates _currentState;

        private Vector2 _currentDirection;
        private float _currentAcceleration;
        private Vector2 _lastActiveAxis;

        private float _onSightSignalCooldown;

        void FixedUpdate()
        {
            HandleBehavior();
        }

        private void HandleBehavior()
        {
            //Debug.Log(_currentState);
            switch (_currentState)
            {
                case EnemyStates.Idle:
                    if (ShouldEndCurrentState())
                        ChangeState(EnemyStates.Patrolling, _patrolTime);
                    else
                        HandleLocomotion(_currentDirection);
                    break;

                case EnemyStates.Patrolling:
                    if (ShouldEndCurrentState())
                        ChangeState(EnemyStates.Idle, _idleTimeAfterPatrol);
                    else
                    {
                        FindPlayer();
                        HandleLocomotion(_currentDirection);
                    }
                        
                    break;

                case EnemyStates.Fleeing:
                    if (ShouldEndCurrentState())
                        ChangeState(EnemyStates.Patrolling, _patrolTime);
                    else
                        HandleLocomotion(_currentDirection);
                    break;

                case EnemyStates.Hurt:
                case EnemyStates.Attack:
                    if (ShouldEndCurrentState())
                        ChangeState(EnemyStates.Patrolling, _patrolTime);
                    break;

                case EnemyStates.Disabled:
                    break;


            }
        }

        private bool ShouldEndCurrentState()
        {
            if (Time.time > _stateEndTime)
            {
                return true;

            }

            return false;
        }

        private void ChangeState(EnemyStates newState, float stateLength)
        {
            switch (newState)
            {
                case EnemyStates.Patrolling:
                    ChangeDirection();
                    break;
                case EnemyStates.Idle:
                    _sfxCtrl.PlayIdleSound();
                    break;
                case EnemyStates.Attack:
                    _animateCtrl.AnimateAttack(_currentDirection);
                    _sfxCtrl.PlayAttackSound();
                    break;
                case EnemyStates.Hurt:
                    _animateCtrl.AnimateHurt(_currentDirection);
                    _sfxCtrl.PlayOnHurtSound();
                    break;
                case EnemyStates.Disabled:
                    _rigidbody.simulated = false;
                    _animateCtrl.AnimateDeath(_currentDirection);
                    _sfxCtrl.PlayDeathSound();
                    _treasure.ThrowTreasure();
                    break;
                case EnemyStates.Fleeing:
                    break;


            }

            _stateEndTime = Time.time + stateLength;
            _currentState = newState;
        }

        private void ChangeDirection()
        {
            //if (_currentDirection == Vector2.zero)
            //    _currentDirection = _directionBeforeIdle;

            if (_currentDirection == Vector2.zero)
                _currentDirection = _startDirection;

            switch (_patrolType)
            {
                case EnemyPatrolTypes.backForth:
                    _currentDirection *= -1;
                    break;
                case EnemyPatrolTypes.sharpTurn:
                    if ((_currentDirection.x >= 0 && _currentDirection.y >= 0)
                        || (_currentDirection.x < 0 && _currentDirection.y < 0))
                        _currentDirection.y *= -1;
                    else
                        _currentDirection.x *= -1;
                    break;
                case EnemyPatrolTypes.random:
                    _currentDirection.x = Random.Range(-1f, 1f);
                    _currentDirection.y = Random.Range(-1f, 1f);
                    break;

            }

            _currentDirection = _currentDirection.normalized;
        }

        private void ChangeDirection(Vector2 newDirection)
        {
            _currentDirection = newDirection;

        }


        private void FindPlayer()
        {

            float sightRadius = _sightRadius + GameManager.GameInstance.GetPlayerNoticeModifier();

            Collider2D[] playersInSight = Physics2D.OverlapCircleAll((Vector2)this.transform.position, sightRadius, _whatIsPlayer);

            if (playersInSight.Length > 0)
            {
//                Debug.Log("Player sighted");
                //Call the EnemyActionController

                if(!_actionCtrl.AttemptAction(playersInSight, _whatIsPlayer))
                    ChasePlayer(playersInSight[0].transform.position);

                
            }
        }

        private void ChasePlayer(Vector3 playerPosition)
        {
            if (_onSightSignalCooldown <= Time.time)
            {
                _onSightSignalCooldown = Time.time + 30;
                _sfxCtrl.PlaySightSound();
            }

            Vector3 direction = (playerPosition - this.transform.position).normalized;
            //Debug.DrawLine(playerPosition, playerPosition + direction * 10, Color.red, Mathf.Infinity);
            ChangeDirection((Vector2)direction);
        }

        private void HandleLocomotion(Vector2 moveAxis)
        {
            var maxAcceleration = _acceleration;

            if (moveAxis != Vector2.zero)
            {
                _currentAcceleration =
                    Mathf.MoveTowards(_currentAcceleration, 1, maxAcceleration * Time.deltaTime);
                _lastActiveAxis = moveAxis;
            }
            else
            {
                maxAcceleration /= 2;
                _currentAcceleration = Mathf.MoveTowards(_currentAcceleration, 0, maxAcceleration * Time.deltaTime);
            }

            var targetSpeed = _moveSpeed * _currentAcceleration;

            if (targetSpeed > 0)
            {
                Vector2 movement = _lastActiveAxis * targetSpeed;
                Move(movement);
            }

            _animateCtrl.AnimateMovement(_lastActiveAxis, targetSpeed, moveAxis);

        }

        private void Move(Vector2 amount)
        {
            Vector2 nextPosition = _rigidbody.position;
            nextPosition += amount * Time.deltaTime;

            _rigidbody.MovePosition(nextPosition);

        }

        #region public methods
        public void AttemptReflect(Vector2 direction)
        {
            if (_currentState == EnemyStates.Patrolling || _currentState == EnemyStates.Idle || _currentState == EnemyStates.Fleeing)
            {
                ChangeState(EnemyStates.Fleeing, _fleeTime);
                ChangeDirection(direction);
            }
        }

        public void AttemptNewDirection(Vector2 newDirection = default(Vector2))
        {
            if(newDirection != default(Vector2))
                ChangeDirection(newDirection);
            else
                ChangeDirection();

        }

        public void AttemptFleeTarget(Vector3 targetPosition)
        {
            Vector3 direction = (this.transform.position - targetPosition).normalized;
            //Debug.DrawLine(playerPosition, playerPosition + direction * 10, Color.red, Mathf.Infinity);
            ChangeDirection((Vector2)direction);
        }

        public void AttemptStateChange(EnemyStates newState, float stateLength=2f)
        {
            ChangeState(newState, stateLength);
        }


        #endregion
    }
}
