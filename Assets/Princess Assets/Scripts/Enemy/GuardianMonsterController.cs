using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class GuardianMonsterController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Vector2 _direction;
        [SerializeField] private float _shotDelay;
        [SerializeField] private int _health;
        [SerializeField] private float _projectileWaitTime;

        [Header("References")]
        [SerializeField] private EnemyAnimateController _animateCtrl;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> _onHurt;
        [SerializeField] private List<AudioClip> _onAttack;
        [SerializeField] private AudioClip _onDie;
        [SerializeField] private ProjectileTrap _projectileTrap;

        private int _currentDamage = 0;
        private float _shotTime;
        private bool _isActive;
        

        // Start is called before the first frame update
        void Start()
        {
            _isActive = true;
            _shotTime = Time.time + _shotDelay;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isActive)
                return;

            if (Time.time > _shotTime)
                Attack();

            HandleIdle();
        }

        private void HandleIdle()
        {
            _animateCtrl.AnimateMovement(_direction, 0, 0, _direction);

        }

        public void PlayAttackSound()
        {
            if (_onAttack.Count == 0)
                return;
            int soundIndex = Random.Range(0, _onAttack.Count);

            _audioSource.PlayOneShot(_onAttack[soundIndex]);
        }

        private void PlayOnHurtSound()
        {
            if (_onHurt.Count == 0)
                return;

            int soundIndex = Random.Range(0, _onHurt.Count);

            _audioSource.PlayOneShot(_onHurt[soundIndex]);
        }

        private void PlayDeathSound()
        {
            SoundManager.SoundInstance.PlayClipAt(_onDie, this.transform.position);
        }

        private void Attack()
        {
            _shotTime = Time.time + _shotDelay;
            _animateCtrl.AnimateAttack(_direction);
            StartCoroutine(LaunchProjectile());
        }

        public void DamageEnemy()
        {
            _currentDamage++;

            if (_currentDamage >= _health)
                DestroyGaurdian();
            else
            {
                _animateCtrl.AnimateHurt(_direction);
                PlayOnHurtSound();

            }
            
        }

        private void DestroyGaurdian()
        {
            _isActive = false;
            _animateCtrl.AnimateDeath(_direction);
            PlayDeathSound();

        }

        private IEnumerator LaunchProjectile()
        {

            float timePassed = 0f;

            //Wait for animation
            while (timePassed < _projectileWaitTime)
            {
                timePassed += Time.deltaTime;
                yield return null;
            }

            PlayAttackSound();
            _projectileTrap.ThrowFireball();

        }



    }
}
