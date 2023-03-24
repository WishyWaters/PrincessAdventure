using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Spine;
using Spine.Unity;

namespace PrincessAdventure
{

    public class EnemyController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private EnemyRigTypes _rigType;
        [SerializeField] private SkeletonAnimation _enemyAnimator;
        [SerializeField] private CustomizableCharacters.CustomizableCharacter _customizableCharacter;
        [SerializeField] private Animator _animator;

        [Header("Settings")]
        [SerializeField] private int _health;
        [SerializeField] private bool _dealHeartDamage;
        [SerializeField] private int _coinDamage;
        [SerializeField] private TreasureDrops _treasureDrop;
        [SerializeField] private float _acceleration; //13
        [SerializeField] private float _moveSpeed; //7
        [SerializeField] private Vector2 _startDirection;

        [SerializeField] private LayerMask _whatIsPlayer;
        [SerializeField] private LayerMask _whatIsBlockade;

        [Header("Patrols")]
        [SerializeField] private EnemyPatrolTypes _patrolType;
        [SerializeField] private float _walkTime;
        [SerializeField] private float _idleTime;

        [Header("Attacks")]
        [SerializeField] private float _attackRadius;
        [SerializeField] private GameObject _projectile;
        [SerializeField] private float _idleTimeAfterAttack;


        private readonly Vector2[] _directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        private Vector2 _currentDirection;
        private float _currentAcceleration;
        private Vector2 _currentMovement;
        private int _animatorDirection;
        private Vector2 _previousDirection;
        private GameObject _currentDirectionGameObject;
        private string _animationPrefix = "Front_";
        private Vector2 _lastActiveAxis;

        private EnemyStates _enemyState;

        //patrol
        private bool _isWalking;
        private float _walkEndTime;
        private float _idleEndTime;
        private Vector2 _directionBeforeIdle;

        //attacking
        private bool _alreadyAttacked;

        private float _sightRange;
        private float _attackRange;
        private bool _playerInSight;
        private bool _playerInAttackRange;


        #region Unity Functions

        void Start()
        {
            EnableController();
        }

        void FixedUpdate()
        {
            //TODO: Get player object
            _playerInSight = Physics2D.OverlapCircleAll((Vector2)this.transform.position, _sightRange, _whatIsPlayer).Length > 0 ? true : false;
            _playerInAttackRange = Physics2D.OverlapCircleAll((Vector2)this.transform.position, _attackRange, _whatIsPlayer).Length > 0 ? true : false;

            if (!_playerInSight && !_playerInAttackRange)
                Patroling();
            else if (_playerInSight && !_playerInAttackRange)
                ChasePlayer();
            else if (_playerInAttackRange)
                AttackPlayer();

        }
        #endregion

        #region Basic AI
        private void Patroling()
        {
            if (_isWalking)
            {
                Debug.Log("Walking");
                if (Time.time > _walkEndTime)
                {
                    Debug.Log("Walk to Idle");
                    _directionBeforeIdle = _currentDirection;
                    _currentDirection = Vector2.zero;
                    _isWalking = false;
                    _idleEndTime = Time.time + _idleTime;
                }
            }
            else
            {
                Debug.Log("Idling");
                if (Time.time > _idleEndTime)
                {
                    Debug.Log("Idle to Walk");
                    ChangeDirection();
                }
            }

            if (_rigType == EnemyRigTypes.CustomHumanoid)
                HandleAnimatorLocomotion(_currentDirection);
            else
                HandleSpineLocomotion(_currentDirection);


        }

        private void ChasePlayer()
        {

        }

        private void AttackPlayer()
        {

        }

        #endregion

        private void EnableController()
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            _previousDirection = Vector2.zero;

            if(_rigType == EnemyRigTypes.CustomHumanoid)
            {
                ResetAnimatorRigs();
                HandleAnimatorDirection(_startDirection);
            }
            else
            {
                HandleSpineDirection(_startDirection);
                UpdateSpineAnimation("Idle", true);
            }

            _currentDirection = _startDirection;
            _directionBeforeIdle = _startDirection;
        }

        private void Move(Vector2 amount)
        {
            Vector2 nextPosition = _rigidbody.position;
            nextPosition += amount * Time.deltaTime;

            _rigidbody.MovePosition(nextPosition);

        }

        private void ChangeDirection()
        {
            _isWalking = true;
            _walkEndTime = Time.time + _walkTime;
            switch (_patrolType)
            {
                case EnemyPatrolTypes.backForth:
                    _currentDirection = _directionBeforeIdle;
                    _currentDirection *= -1;
                    break;
                case EnemyPatrolTypes.sharpTurn:
                    _currentDirection = _directionBeforeIdle;

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
        }


        #region spine animation

        private void HandleSpineLocomotion(Vector2 moveAxis)
        {
            var maxAcceleration = _acceleration;

            if (moveAxis != Vector2.zero)
            {

                HandleSpineDirection(moveAxis);
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
                _currentMovement = _lastActiveAxis * targetSpeed;
                Move(_currentMovement);
                UpdateSpineAnimation("Walk", true);

            }
            else
            {
                UpdateSpineAnimation("Idle", true);
            }
        }

        private void HandleSpineDirection(Vector2 moveAxis)
        {
            var direction = GetClosestDirection(moveAxis);
            if (direction == _previousDirection)
                return;

            if (direction == Vector2.right)
            {
                _enemyAnimator.skeleton.SetSkin("Side");
                _enemyAnimator.skeleton.SetSlotsToSetupPose();
                this.transform.localScale = new Vector3(1, 1, 1);
                _animationPrefix = "Side_";

            }
            else if (direction == Vector2.left)
            {
                _enemyAnimator.skeleton.SetSkin("Side");
                _enemyAnimator.skeleton.SetSlotsToSetupPose();
                this.transform.localScale = new Vector3(-1, 1, 1);
                _animationPrefix = "Side_";

            }
            else if (direction == Vector2.up)
            {
                _enemyAnimator.skeleton.SetSkin("Back");
                _enemyAnimator.skeleton.SetSlotsToSetupPose();
                _animationPrefix = "Back_";

            }
            else if (direction == Vector2.down)
            {
                _enemyAnimator.skeleton.SetSkin("Front");
                _enemyAnimator.skeleton.SetSlotsToSetupPose();
                _animationPrefix = "Front_";

            }

            _previousDirection = direction;
        }

        private void UpdateSpineAnimation(string animationName, bool loop)
        {
            animationName = _animationPrefix + animationName;

            if (_enemyAnimator.AnimationName == animationName)
                return;

            _enemyAnimator.AnimationState.SetAnimation(0, animationName, loop);
        }

        #endregion

        #region custom humanoid animation
        private void ResetAnimatorRigs()
        {
            _customizableCharacter.UpRig.SetActive(false);
            _customizableCharacter.SideRig.SetActive(false);
            _customizableCharacter.DownRig.SetActive(false);
        }

        private void HandleAnimatorLocomotion(Vector2 moveAxis)
        {
            var maxAcceleration = _acceleration;

            if (moveAxis != Vector2.zero)
            {
                HandleAnimatorDirection(moveAxis);

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
            _currentMovement = _lastActiveAxis * targetSpeed;

            var animatorSpeed = Mathf.InverseLerp(0, _moveSpeed, targetSpeed);
            _animator.SetFloat("Speed", animatorSpeed);
            _animator.SetFloat("Direction", _animatorDirection);

            Move(_currentMovement);
        }

        private void HandleAnimatorDirection(Vector2 moveAxis)
        {
            var direction = GetClosestDirection(moveAxis);
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


        #endregion

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




        void OnCollisionEnter2D(Collision2D collision)
        {

            if(_dealHeartDamage && collision.gameObject.tag == "Player" && collision.gameObject.layer == 6)
            {
                GameManager.GameInstance.DamagePrincess();
            }
            else if(collision.gameObject.tag == "Companion")
            {
                GameManager.GameInstance.ActivatePrincess(true);
            } else
            {
                Debug.Log("Collision Direction Change");
                ChangeDirection();
            }
        }

       


    }

}
