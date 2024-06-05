using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class ProjectileController : MonoBehaviour
    {

        [SerializeField] private GameObject _hitEffectPrefab;
        [SerializeField] private AudioClip _fireSound;
        [SerializeField] private AudioClip _hitSound;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private bool _altAngled = false;
        [SerializeField] private AudioSource _audioSource;

        private float _timeToLive = 5f;
        private Vector2 _direction = Vector2.zero;
        private float _deathTime;
        private bool isActive = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_deathTime < Time.time)
                Explode();
            else
                Move();
        }

        public void InitializeProjectile(Vector2 direction)
        {
            _direction = direction;
            _deathTime = Time.time + _timeToLive;
            isActive = true;

            Collider2D[] col = Physics2D.OverlapCircleAll((Vector2)this.transform.position, .3f, _layerMask);
            if (col.Length > 1)
                Explode();
            else
            {
                SetRotation();
                if (_fireSound != null)
                    _audioSource.PlayOneShot(_fireSound);
                    
            }
        }

        public void ReflectProjectile(Vector2 newDirection)
        {
            _direction = newDirection;
            _deathTime += 1;
            SetRotation();
        }

        private void Move()
        {
            transform.position += (Vector3)_direction * _speed * Time.deltaTime;
        }

        public void Explode()
        {
            SpawnHitEffect();
            Destroy(this.gameObject);
        }

        private void SpawnHitEffect()
        {
            GameObject hitEffect = Instantiate(_hitEffectPrefab, this.transform.position, this.transform.rotation);


            ParticleSystem partSys = hitEffect.GetComponent<ParticleSystem>();
            if (hitEffect != null && partSys != null)
                partSys.Play();

            if (_hitSound != null)
                SoundManager.SoundInstance.PlayClipAt(_hitSound, this.transform.position);

        }

        private void SetRotation()
        {
            if (_direction == Vector2.down)
            {
                if(_altAngled)
                    this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 270);
                else
                    this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
            else if (_direction == Vector2.right)
            {
                if (_altAngled)
                    this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                else
                    this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            }
            else if (_direction == Vector2.up)
            {
                if (_altAngled)
                    this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
                else
                    this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 270);
            }
            else if (_direction == Vector2.left)
            {
                if (_altAngled)
                    this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
                else
                    this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            //activates on any hard collisions
            if (isActive && !collision.isTrigger)
            {
                if (collision.CompareTag("Player") && collision.gameObject.layer == 6)
                {
                    GameManager.GameInstance.DamagePrincess(this.transform.position);
                }
                else if (collision.CompareTag("Companion"))
                {
                    GameManager.GameInstance.ActivatePrincess(true);
                }
                else if (collision.CompareTag("Bomb"))
                {
                    BombController bCtrl = collision.GetComponent<BombController>();

                    bCtrl.Explode();
                }
                else if (collision.CompareTag("Enemy"))
                {
                    EnemyActionController enemyCtrl = collision.GetComponent<EnemyActionController>();

                    enemyCtrl.DamageEnemy(this.transform.position, false);
                }
                else if (collision.CompareTag("Boomshroom"))
                {
                    BoomshroomController shroomCtrl = collision.GetComponent<BoomshroomController>();

                    shroomCtrl.StartExplode();
                }
                else if (collision.CompareTag("Ice"))
                {
                    DestructibleController iceCtrl = collision.GetComponent<DestructibleController>();

                    iceCtrl.RemoveDestructable();
                }
                else if (collision.CompareTag("Guard"))
                {
                    GuardianMonsterController guardCtrl = collision.GetComponent<GuardianMonsterController>();

                    guardCtrl.DamageEnemy();
                }


                Explode();
            }

            //Only hits very specific trigger objects
            if (isActive && collision.isTrigger)
            {

                if (collision.CompareTag("OpenFire"))
                {
                    SmallFireController fireCtrl = collision.GetComponent<SmallFireController>();

                    fireCtrl.LightFire();

                    Explode();
                }
                else if (collision.CompareTag("Projectile"))
                {
                    

                    Explode();
                }


            }


        }
    }
}
