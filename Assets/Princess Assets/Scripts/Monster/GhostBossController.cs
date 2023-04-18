//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace PrincessAdventure
//{
//    public class GhostBossController : MonoBehaviour
//    {

//        [Header("References")]
//        [SerializeField] private Rigidbody2D _rigidbody;
//        [SerializeField] private EnemyRigTypes _rigType;
//        [SerializeField] private SpineAnimateController _spineController;
//        [SerializeField] private EnemySoundController _sfxCtrl;
//        [SerializeField] private GameObject _deathEffect;
//        [SerializeField] private GameObject _monsterToSpawnPrefab;

//        [Header("Core Settings")]
//        [SerializeField] private int _health;
//        [SerializeField] private bool _dealHeartDamage;
//        [SerializeField] private int _coinDamage;
//        [SerializeField] private float _acceleration; //8+
//        [SerializeField] private float _moveSpeed; //2 to 7
//        [SerializeField] private Vector2 _startDirection;

//        [SerializeField] private LayerMask _whatIsPlayer;
//        [SerializeField] private LayerMask _whatIsBlockade;
//        [SerializeField] private bool _diesToExplosion;


//        [Header("Behavior Settings")]
//        [SerializeField] private EnemyPatrolTypes _patrolType;
//        [SerializeField] private float _patrolTime;
//        [SerializeField] private float _idleTimeAfterPatrol;
//        [SerializeField] private float _idleTimeAfterAttack;

//        [Header("Attack Settings")]
//        [SerializeField] private float _sightRadius;
//        [SerializeField] private float _attackRadius;
//        [SerializeField] private GameObject _projectile;
//        [SerializeField] private float _delayBefoerProjectileSpawn;


//        private float _stateEndTime;
//        private float _attackCooldownTime;
//        private EnemyStates _currentState;
//        private float _onSightSignalCooldown;
//        private int _currentDamage;

//        void FixedUpdate()
//        {
//            HandleAI();

//        }

//        private void HandleAI()
//        {
//            switch (_currentState)
//            {
//                case EnemyStates.Idle:
//                    if (ShouldEndCurrentState())
//                        ChangeState(EnemyStates.Patrolling);
//                    break;
//                case EnemyStates.Patrolling:
//                    if (ShouldEndCurrentState())
//                        ChangeState(EnemyStates.Idle);
//                    else
//                        Patrolling();
//                    break;
//                case EnemyStates.Fleeing:
//                    if (ShouldEndCurrentState())
//                        ChangeState(EnemyStates.Patrolling);
//                    break;
//                case EnemyStates.Hurt:
//                case EnemyStates.Attack:
//                case EnemyStates.Disabled:
//                    break;
//            }

//            HandleSpineLocomotion(_currentDirection);

//            CheckStateTime();
                

//        }

//        private bool ShouldEndCurrentState()
//        {
//            if (Time.time > _stateEndTime)
//            {
//                return true;
                
//            }

//            return false;
//        }

//        private void ChangeState(EnemyStates newState)
//        {
//            switch (newState)
//            {
//                case EnemyStates.Patrolling:
//                    _stateEndTime = Time.time + _patrolTime;
//                    ChangeDirection();
//                    break;
//                case EnemyStates.Idle:
//                    _patrolEndTime = 0;
//                    _idleEndTime = Time.time + _idleTime;
//                    _directionBeforeIdle = _currentDirection;
//                    ChangeDirection(Vector2.zero);
//                    _sfxCtrl.PlayIdleSound();
//                    break;
//                case EnemyStates.Attack:
//                    _patrolEndTime = 0;
//                    _attackCooldownTime = Time.time + _idleTimeAfterAttack;
//                    _directionBeforeIdle = _currentDirection;
//                    _sfxCtrl.PlayAttackSound();
//                    break;
//                case EnemyStates.Hurt:
//                    _patrolEndTime = 0;
//                    _idleEndTime = Time.time + .5f;
//                    _directionBeforeIdle = _currentDirection;
//                    _sfxCtrl.PlayOnHurtSound();
//                    break;
//                case EnemyStates.Disabled:
//                    _rigidbody.simulated = false;
//                    _directionBeforeIdle = Vector2.zero;
//                    _sfxCtrl.PlayDeathSound();
//                    break;
//                case EnemyStates.Fleeing:
//                    _fleeEndTime = Time.time + 2f;
//                    break;


//            }

//            _enemyState = newState;
//        }
//    }
//}
