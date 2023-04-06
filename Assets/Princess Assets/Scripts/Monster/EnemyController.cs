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
        [SerializeField] private EnemySoundController _sfxCtrl;

        [Header("Settings")]
        [SerializeField] private int _health;
        [SerializeField] private bool _dealHeartDamage;
        [SerializeField] private int _coinDamage;
        [SerializeField] private float _acceleration; //8+
        [SerializeField] private float _moveSpeed; //2 to 7
        [SerializeField] private Vector2 _startDirection;

        [SerializeField] private LayerMask _whatIsPlayer;
        [SerializeField] private LayerMask _whatIsBlockade;
        [SerializeField] private bool _diesToExplosion;
        [SerializeField] private GameObject _deathEffect;

        [Header("Patrols")]
        [SerializeField] private EnemyPatrolTypes _patrolType;
        [SerializeField] private float _patrolTime;
        [SerializeField] private float _idleTime;

        [Header("Attacks")]
        [SerializeField] private float _sightRadius;
        [SerializeField] private float _attackRadius;
        [SerializeField] private GameObject _projectile;
        [SerializeField] private float _idleTimeAfterAttack;
        [SerializeField] private float _projectileWaitTime;

        private readonly Vector2[] _directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        private Vector2 _currentDirection;
        private float _currentAcceleration;
        private Vector2 _currentMovement;
        private int _animatorDirection;
        private Vector2 _previousDirection;
        private GameObject _currentDirectionGameObject;
        private string _animationPrefix = "Front_";
        private Vector2 _lastActiveAxis;
        private Coroutine _currentCoroutine;
        private bool _allowNewSpineAnimations; 
        private EnemyStates _enemyState;
        private float _onSightSignalCooldown;
        private int _currentDamage;

        //patrol
        private float _patrolEndTime;
        private float _idleEndTime;
        private Vector2 _directionBeforeIdle;
        private float _fleeEndTime;
        private float _attackCooldownTime;



        #region Unity Functions

        void Start()
        {
            EnableController();
        }

        void FixedUpdate()
        {
            HandleAI();

            if (_rigType == EnemyRigTypes.CustomHumanoid)
                HandleAnimatorLocomotion(_currentDirection);
            else
            {
                if (_enemyState == EnemyStates.Patrolling || _enemyState == EnemyStates.Idle || _enemyState == EnemyStates.Fleeing)
                    HandleSpineLocomotion(_currentDirection);
            }
                

        }
        #endregion

        #region Basic AI
        private void ChangeState(EnemyStates newState)
        {
            switch(newState)
            {
                case EnemyStates.Patrolling:
                    _idleEndTime = 0;
                    _patrolEndTime = Time.time + _patrolTime;
                    ChangeDirection();
                    break;
                case EnemyStates.Idle:
                    _patrolEndTime = 0;
                    _idleEndTime = Time.time + _idleTime;
                    _directionBeforeIdle = _currentDirection;
                    ChangeDirection(Vector2.zero);
                    _sfxCtrl.PlayIdleSound();
                    break;
                case EnemyStates.Attack:
                    _patrolEndTime = 0;
                    _attackCooldownTime = Time.time + _idleTimeAfterAttack;
                    _directionBeforeIdle = _currentDirection;
                    _sfxCtrl.PlayAttackSound();
                    break;
                case EnemyStates.Hurt:
                    _patrolEndTime = 0;
                    _idleEndTime = Time.time + .5f;
                    _directionBeforeIdle = _currentDirection;
                    _sfxCtrl.PlayOnHurtSound();
                    break;
                case EnemyStates.Disabled:
                    _rigidbody.simulated = false;
                    _directionBeforeIdle = Vector2.zero;
                    _sfxCtrl.PlayDeathSound();
                    break;
                case EnemyStates.Fleeing:
                    _fleeEndTime = Time.time + 2f;
                    break;


            }

            _enemyState = newState;
        }

        private void HandleAI()
        {
            switch(_enemyState)
            {
                case EnemyStates.Idle:
                    CheckIdleTime();
                    break;
                case EnemyStates.Patrolling:
                    Patrolling();
                    break;
                case EnemyStates.Attack:
                    //CheckIdleTime();  Attack moves to Idle after animation
                    break;
                case EnemyStates.Fleeing:
                    CheckFleeTime();
                    break;
                case EnemyStates.Hurt:
                    //CheckIdleTime(); Hurt moves to Flee after animation
                    break;

            }


        }

        private void CheckIdleTime()
        {
            if (Time.time > _idleEndTime)
                ChangeState(EnemyStates.Patrolling);

        }

        private void CheckFleeTime()
        {
            if (Time.time > _fleeEndTime)
                ChangeState(EnemyStates.Patrolling);

        }

        private void Patrolling()
        {
            float sightRadius = _sightRadius + GameManager.GameInstance.GetPlayerNoticeModifier();

            Collider2D[] playersInSight = Physics2D.OverlapCircleAll((Vector2)this.transform.position, sightRadius, _whatIsPlayer);

            if (playersInSight.Length == 0)
            {
                CheckPatrolTime();
            }
            else
            {

                //Attempt Melee Attack
                if (_attackCooldownTime < Time.time)
                {
                    Collider2D[] playersInAttackRange = Physics2D.OverlapCircleAll((Vector2)this.transform.position, _attackRadius, _whatIsPlayer);

                    if (playersInAttackRange.Length > 0)
                    {
                        HandleAttack(playersInSight[0].transform.position);
                    }
                    else if (ShouldUseRangeAttack(playersInSight[0].transform.position))
                    {
                        StartCoroutine(LaunchProjectile(playersInSight[0].transform.position));
                        HandleAttack(playersInSight[0].transform.position);

                    }
                    else
                    {
                        ChasePlayer(playersInSight[0].transform.position);
                    }
                }
            }
        }

        private void CheckPatrolTime()
        {
            if (Time.time > _patrolEndTime)
                ChangeState(EnemyStates.Idle);

        }

        private void ChasePlayer(Vector3 playerPosition)
        {
            if(_onSightSignalCooldown <= Time.time)
            {
                _onSightSignalCooldown = Time.time + 30;
                _sfxCtrl.PlaySightSound();
            }

            Vector3 direction = (playerPosition - this.transform.position).normalized;
            //Debug.DrawLine(playerPosition, playerPosition + direction * 10, Color.red, Mathf.Infinity);
            ChangeDirection((Vector2)direction);
        }

        private void FleeTarget(Vector3 targetPosition)
        {
            Vector3 direction = (this.transform.position- targetPosition).normalized;
            //Debug.DrawLine(playerPosition, playerPosition + direction * 10, Color.red, Mathf.Infinity);
            ChangeDirection((Vector2)direction);
        }

        private bool ShouldUseRangeAttack(Vector3 playerPosition)
        {
            if (_projectile == null)
                return false;

            if ((playerPosition.x >= this.transform.position.x - 1f)
                && (playerPosition.x <= this.transform.position.x + 1f))
                return true;

            if ((playerPosition.y >= this.transform.position.y - 1f)
                && (playerPosition.y <= this.transform.position.y + 1f))
                return true;

            return false;
        }

        private void HandleAttack(Vector3 playerPosition)
        {
            //Update to face player
            _currentDirection = (Vector2)(playerPosition - this.transform.position).normalized;

            ChangeState(EnemyStates.Attack);

            if (_rigType == EnemyRigTypes.CustomHumanoid)
            {
                if (_currentCoroutine != null)
                    StopCoroutine(_currentCoroutine);
                _currentCoroutine = StartCoroutine(DoAnimatorAttack(_currentDirection));
            }
            else
            {
                HandleSpineDirection(_currentDirection);
                SetSpineAnimation("Attack", false);
            }
        }

        private void CheckAttackCollision()
        {
            Collider2D[] playersInAttackRange = Physics2D.OverlapCircleAll((Vector2)this.transform.position + _previousDirection, .5f, _whatIsPlayer);

            foreach(Collider2D player in playersInAttackRange)
            {
                if (player.gameObject.tag == "Player" && player.gameObject.layer == 6)
                {
                    _sfxCtrl.PlayAttackImpactSound();

                    if (_dealHeartDamage)
                        GameManager.GameInstance.DamagePrincess(this.transform.position);

                    if (_coinDamage > 0)
                        GameManager.GameInstance.CoinDamagePrincess(this.transform.position, _coinDamage);
                }
                else if (player.gameObject.tag == "Companion")
                {
                    _sfxCtrl.PlayAttackImpactSound();
                    GameManager.GameInstance.ActivatePrincess(true);
                }
            }


        }

        #endregion

        private void EnableController()
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            _previousDirection = Vector2.zero;

            if (_rigType == EnemyRigTypes.CustomHumanoid)
            {
                ResetAnimatorRigs();
                HandleAnimatorDirection(_startDirection);
            }
            else
            {
                _allowNewSpineAnimations = true;
                _enemyAnimator.AnimationState.Complete += OnSpineAnimationComplete;

                HandleSpineDirection(_startDirection);
                SetSpineAnimation("Idle", true);
            }
            ChangeState(EnemyStates.Idle);
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
            if(_currentDirection == Vector2.zero)
                _currentDirection = _directionBeforeIdle;

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
                SetSpineAnimation("Walk", true);

            }
            else
            {
                SetSpineAnimation("Idle", true);
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

        private void SetSpineAnimation(string animationName, bool loop)
        {
            //Names are: Idle, Walk, Death, Hurt and Attack
            if (_allowNewSpineAnimations || animationName == "Death")
            {
                if (animationName == "Attack" || animationName == "Hurt" || animationName == "Death")
                {
                    _allowNewSpineAnimations = false;

                    if (animationName == "Attack")
                        CheckAttackCollision();

                }

                animationName = _animationPrefix + animationName;

                if (_enemyAnimator.AnimationName == animationName)
                    return;

                _enemyAnimator.AnimationState.SetAnimation(0, animationName, loop);
            }
        }

        public void OnSpineAnimationComplete(TrackEntry trackEntry)
        {
            // Add your implementation code here to react to complete events
            if (_enemyAnimator.AnimationName.Contains("Death"))
                Destroy(this.gameObject);
            else if (_enemyAnimator.AnimationName.Contains("Attack"))
            {
                ChangeState(EnemyStates.Idle);
                SetSpineAnimation("Idle", true);
            }
            else if (_enemyAnimator.AnimationName.Contains("Hurt"))
            {
                ChangeState(EnemyStates.Fleeing);
                SetSpineAnimation("Walk", true);
            }

            _allowNewSpineAnimations = true;
            
            
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
            float maxAcceleration = _acceleration;

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

            if(collision.gameObject.tag == "Player" && collision.gameObject.layer == 6)
            {
                if(_dealHeartDamage)
                    GameManager.GameInstance.DamagePrincess(this.transform.position);

                if (_coinDamage > 0)
                    GameManager.GameInstance.CoinDamagePrincess(this.transform.position, _coinDamage);
            }
            else if(collision.gameObject.tag == "Companion")
            {
                GameManager.GameInstance.ActivatePrincess(true);
            }
            else
            {
                //Debug.Log("Collision Direction Change");
                ChangeDirection();
            }
        }

        public void DamageEnemy(Vector3 positionOfPain, bool isExplosion)
        {
            if (_diesToExplosion && isExplosion)
                DestroyEnemy();
            else
            {
                _currentDamage++;

                if (_currentDamage >= _health)
                    DestroyEnemy();
                else
                {
                    FleeTarget(positionOfPain);
                    ChangeState(EnemyStates.Hurt);
                    if (_rigType == EnemyRigTypes.CustomHumanoid)
                    {
                        if (_currentCoroutine != null)
                            StopCoroutine(_currentCoroutine);
                        _currentCoroutine = StartCoroutine(DoAnimatorHurt(_currentDirection));
                    }
                    else
                    {
                        //Names are: Idle, Walk, Death, Hurt and Attack
                        HandleSpineDirection(_currentDirection);
                        SetSpineAnimation("Hurt", false);
                    }
                    
                }
            }
        }

        private void DestroyEnemy()
        {
            ChangeState(EnemyStates.Disabled);
            

            if (_rigType == EnemyRigTypes.CustomHumanoid)
            {
                if (_currentCoroutine != null)
                    StopCoroutine(_currentCoroutine);
                _currentCoroutine = StartCoroutine(DoDie(_currentDirection));
            }
            else
            {
                //Names are: Idle, Walk, Death, Hurt and Attack
                HandleSpineDirection(_currentDirection);

                SetSpineAnimation("Death", false);
            }
            TreasureExplosion treasure = this.GetComponent<TreasureExplosion>();
            if (treasure != null)
                treasure.ThrowTreasure();

            Instantiate(_deathEffect, this.transform.position, this.transform.rotation);
            PaintItBlack();
        }

        private void PaintItBlack()
        {
            Color black = Color.black;

            if(_rigType == EnemyRigTypes.CustomHumanoid)
            {
                SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>(true);
                foreach (SpriteRenderer child in children)
                {
                    child.color = black;
                }
            }
            else
            {
                _enemyAnimator.skeleton.SetColor(black);
            }


            
        }

        public void ReflectEnemy(Vector2 direction)
        {
            if (_enemyState == EnemyStates.Patrolling || _enemyState == EnemyStates.Idle || _enemyState == EnemyStates.Fleeing)
            {
                ChangeState(EnemyStates.Fleeing);
                ChangeDirection(direction);
            }
        }

        #region Coroutines
        private IEnumerator DoAnimatorAttack(Vector2 direction)
        {

            _animator.SetTrigger("Attack 1");

            // make sure each attack is going in the direction player is pressing
            if (direction != Vector2.zero)
            {
                _lastActiveAxis = direction;
                HandleAnimatorDirection(direction);
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

                yield return null;
            }

            CheckAttackCollision();

            ChangeState(EnemyStates.Idle);

        }

        private IEnumerator DoAnimatorHurt(Vector2 direction)
        {
            _animator.SetTrigger("Hurt");

            // make sure each attack is going in the direction player is pressing
            if (direction != Vector2.zero)
            {
                _lastActiveAxis = direction;
                HandleAnimatorDirection(direction);
                _currentAcceleration = 1.2f;
            }
            else
                _currentAcceleration = 0.8f;

            // wait for animator state to change to attack
            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt") == false)
                yield return null;

            _animator.ResetTrigger("Hurt");


            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt")
                       && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {

                yield return null;
            }

            ChangeState(EnemyStates.Fleeing);
        }


        private IEnumerator DoDie(Vector2 direction)
        {
            _animator.SetTrigger("Die");

            // make sure each attack is going in the direction player is pressing
            if (direction != Vector2.zero)
            {
                _lastActiveAxis = direction;
                HandleAnimatorDirection(direction);
                _currentAcceleration = 1.2f;
            }
            else
                _currentAcceleration = 0.8f;

            // wait for animator state to change to attack
            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Die") == false)
                yield return null;

            _animator.ResetTrigger("Die");

            float moveTime = 0f;
            while (moveTime < .3f)
            {
                moveTime += Time.deltaTime;

                yield return null;
            }
            

            Destroy(this.gameObject);
        }

        private IEnumerator LaunchProjectile(Vector3 playerPosition)
        {

            float timePassed = 0f;

            //Wait for animation
            while (timePassed < _projectileWaitTime)
            {
                timePassed += Time.deltaTime;
                yield return null;
            }

            Vector3 direction = (playerPosition - this.transform.position).normalized;
            Vector2 directionToFire = GetClosestDirection(direction);
            Vector3 projSpawnPoint = this.transform.position + (Vector3)directionToFire;
            if (directionToFire == Vector2.left || directionToFire == Vector2.right)
                projSpawnPoint.y += .6f;
            else if (directionToFire == Vector2.up)
                projSpawnPoint.x += .6f;


            GameObject projectile = Instantiate(_projectile, projSpawnPoint, new Quaternion());
            ProjectileController projCtrl = projectile.GetComponent<ProjectileController>();
            projCtrl.InitializeProjectile((Vector3)directionToFire);
        }


        #endregion
    }

}
