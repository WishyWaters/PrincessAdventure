using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{ 
    public class EnemyActionController : MonoBehaviour
    {
        [Header("Script References")]
        [SerializeField] private EnemyBehaviorController _behaviorCtrl;
        [SerializeField] private EnemySoundController _sfxCtrl;

        [Header("Settings")]
        [SerializeField] private int _health;
        [SerializeField] private bool _dealHeartDamage;
        [SerializeField] private int _coinDamage;
        [SerializeField] private bool _diesToExplosion;
        [SerializeField] private bool _canBeReflected;
        [SerializeField] private bool _fleeOnDamage;


        [Header("Melee")]
        [SerializeField] private float _attackRadius;
        [SerializeField] private float _meleeWaitTime;
        [SerializeField] private float _idleTimeAfterAttack;

        [Header("Range")]
        [SerializeField] private GameObject _projectile;
        [SerializeField] private float _projectileWaitTime;
        [SerializeField] private float _leftRightOffset;
        [SerializeField] private float _upOffset;


        [Header("Create Thing")]
        [SerializeField] private bool _onHitCreate;
        [SerializeField] private GameObject _thingToCreatePrefab;


        [Header("Teleport")]
        [SerializeField] private bool _onHitTeleport;
        [SerializeField] private List<Transform> _teleportDestinations;
        [SerializeField] private GameObject _teleportEffectPrefab;

        private readonly Vector2[] _directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };


        private float _currentDamage;
        private float _attackCooldownEndTime;

        private Vector3 _lastPositionOfPain;

        void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.gameObject.tag == "Player" && collision.gameObject.layer == 6)
            {
                if (_dealHeartDamage)
                    GameManager.GameInstance.DamagePrincess(this.transform.position);

                if (_coinDamage > 0)
                    GameManager.GameInstance.CoinDamagePrincess(this.transform.position, _coinDamage);
            }
            else if (collision.gameObject.tag == "Companion")
            {
                GameManager.GameInstance.ActivatePrincess(true);
            }
            else
            {
                //Debug.Log("Collision Direction Change");
                _behaviorCtrl.AttemptNewDirection();
            }
        }



        public bool AttemptAction(Collider2D[] playersInSight, LayerMask whatIsPlayerMask, Vector2 direction)
        {
            bool canDoSomething = false;

            //Attempt Melee Attack
            if (_attackCooldownEndTime < Time.time)
            {
                Collider2D[] playersInAttackRange = Physics2D.OverlapCircleAll((Vector2)this.transform.position + direction, _attackRadius, whatIsPlayerMask);

                if (playersInAttackRange.Length > 0)
                {
                    StartCoroutine(CheckAttackCollision( whatIsPlayerMask, direction));
                    HandleAttack(playersInAttackRange[0].transform.position);
                    canDoSomething = true;
                }
                else if (ShouldUseRangeAttack(playersInSight[0].transform.position))
                {
                    StartCoroutine(LaunchProjectile(playersInSight[0].transform.position));
                    HandleAttack(playersInSight[0].transform.position);
                    canDoSomething = true;

                }
            }

            return canDoSomething;
        }

        private IEnumerator CheckAttackCollision(LayerMask whatIsPlayerMask, Vector2 direction)
        {

            yield return new WaitForSeconds(_meleeWaitTime);

            if (_currentDamage < _health)
            {
                Collider2D[] playersInAttackRange = Physics2D.OverlapCircleAll((Vector2)this.transform.position + direction, _attackRadius, whatIsPlayerMask);

                foreach (Collider2D player in playersInAttackRange)
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
            _attackCooldownEndTime = Time.time + _idleTimeAfterAttack;
            //Update to face player
            Vector2 newDirection = (Vector2)(playerPosition - this.transform.position).normalized;
            _behaviorCtrl.AttemptNewDirection(newDirection);

            _behaviorCtrl.AttemptStateChange(EnemyStates.Attack);


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
                    _lastPositionOfPain = positionOfPain;
                    _behaviorCtrl.AttemptStateChange(EnemyStates.Hurt);

                    if (_onHitCreate)
                        DoCreateThing();

                    if (_onHitTeleport)
                        DoTeleport();

                    
                }
            }
        }

        private void DoTeleport()
        {
            if (_teleportDestinations.Count == 0)
                return;

            int teleportIndex = Random.Range(0, _teleportDestinations.Count);
            Instantiate(_teleportEffectPrefab, this.transform.position, this.transform.rotation);
            this.gameObject.transform.position = _teleportDestinations[teleportIndex].position;
            
        }

        private void DoCreateThing()
        {
            Instantiate(_thingToCreatePrefab, this.transform.position, this.transform.rotation);
        }

        public void AttemptFleeFromPain()
        {
            if (_fleeOnDamage)
                _behaviorCtrl.AttemptFleeTarget(_lastPositionOfPain);
            else
                _behaviorCtrl.AttemptStateChange(EnemyStates.Idle, .1f);

        }

        public void AttemptIdleAfterAttack()
        {
            _behaviorCtrl.AttemptStateChange(EnemyStates.Idle, _idleTimeAfterAttack);
        }


        private void DestroyEnemy()
        {
            _behaviorCtrl.AttemptStateChange(EnemyStates.Disabled);
        }

        public void ReflectEnemy(Vector2 direction)
        {
            if (_canBeReflected)
            {
                _behaviorCtrl.AttemptReflect(direction);
            }
        }

        public bool CanEnemyBeReflected()
        {
            return _canBeReflected;
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
                projSpawnPoint.y += _leftRightOffset;
            else if (directionToFire == Vector2.up)
                projSpawnPoint.y += _upOffset;


            GameObject projectile = Instantiate(_projectile, projSpawnPoint, new Quaternion());
            ProjectileController projCtrl = projectile.GetComponent<ProjectileController>();
            projCtrl.InitializeProjectile((Vector3)directionToFire);
        }
    }
}
