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

        private float _timeToLive = 5f;
        private float _speed = 10f;
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
                if (_fireSound != null)
                    SoundManager.SoundInstance.PlayEffectSound(_fireSound);
            }
        }

        public void Move()
        {
            transform.position += (Vector3)_direction * _speed * Time.deltaTime;
        }

        public void Explode()
        {
            SpawnHitEffect();
            Destroy(this.gameObject);
        }

        public void SpawnHitEffect()
        {
            GameObject hitEffect = Instantiate(_hitEffectPrefab, this.transform.position, this.transform.rotation);


            ParticleSystem partSys = hitEffect.GetComponent<ParticleSystem>();
            if (hitEffect != null && partSys != null)
                partSys.Play();

            if (_hitSound != null)
                SoundManager.SoundInstance.PlayEffectSound(_hitSound);

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (isActive && !collision.isTrigger)
            {
                if (collision.tag == "Player" && collision.gameObject.layer == 6)
                {
                    GameManager.GameInstance.DamagePrincess(this.transform.position);
                }
                else if (collision.tag == "Companion")
                {
                    GameManager.GameInstance.ActivatePrincess(true);
                }
                else if (collision.tag == "Bomb")
                {
                    BombController bCtrl = collision.GetComponent<BombController>();

                    bCtrl.Explode();
                }

                Explode();
            }
        }
    }
}
