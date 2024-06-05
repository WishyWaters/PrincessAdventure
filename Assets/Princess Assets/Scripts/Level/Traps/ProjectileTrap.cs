using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class ProjectileTrap : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private AudioClip _throwSound;
        [SerializeField] private Vector2 _direction;
        [SerializeField] private float _shotDelay;
        [SerializeField] private bool _isActive;

        private float _shotTime;
        private GameObject _projectile;

        private void Start()
        {
            if(_isActive)
                _shotTime = Time.time + _shotDelay;
        }

        private void Update()
        {
            if(_isActive && _shotTime < Time.time)
            {
                ThrowFireball();
                _shotTime = Time.time + _shotDelay;
            }
        }

        public void ThrowFireball()
        {
            _projectile = Instantiate(_projectilePrefab, this.transform.position + (Vector3)_direction, this.transform.rotation);

            if (_throwSound != null)
                SoundManager.SoundInstance.PlayEffectSound(_throwSound);

            ProjectileController fireballCtrl = _projectile.GetComponent<ProjectileController>();
            if (fireballCtrl != null)
                fireballCtrl.InitializeProjectile(_direction);

        }

        
    }
}
