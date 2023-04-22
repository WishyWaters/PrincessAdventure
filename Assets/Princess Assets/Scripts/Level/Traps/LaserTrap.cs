using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class LaserTrap : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private AudioClip _laserSound;
        [SerializeField] private GameObject _hitBox;

        [Header("Settings")]
        [SerializeField] private float _cooldown;
        [SerializeField] private float _startOffset;

        private float _flipCountDown;
        private bool _isLaserOn;

        // Start is called before the first frame update
        void Start()
        {
            _hitBox.SetActive(false);
            _isLaserOn = false;
            _flipCountDown = _startOffset;
        }

        // Update is called once per frame
        void Update()
        {
            _flipCountDown -= Time.deltaTime;

            if (_flipCountDown < 0)
            {
                FlipLaserActivation();
            }
        }

        private void FlipLaserActivation()
        {
            if (_isLaserOn)
            {
                _isLaserOn = false;
                _hitBox.SetActive(false);
                _flipCountDown = _cooldown;
            }
            else
            {
                _isLaserOn = true;
                _particle.Play();
                SoundManager.SoundInstance.PlayClipAt(_laserSound, this.transform.position);
                _hitBox.SetActive(true);
                _flipCountDown = .8f;
            }
        }

        private IEnumerator ActivateLaserHitBox()
        {
            yield return new WaitForSeconds(.2f);
            _hitBox.SetActive(true);
        }
    }


}
