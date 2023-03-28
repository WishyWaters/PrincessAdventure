using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace PrincessAdventure
{
    public class EnemySoundController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioSource _audioSource;

        [Header("Sounds")]
        [SerializeField] private List<AudioClip> _attack;
        [SerializeField] private List<AudioClip> _onAttackImpact;
        [SerializeField] private List<AudioClip> _onSight;
        [SerializeField] private List<AudioClip> _onHurt;
        [SerializeField] private List<AudioClip> _onIdle;

        [SerializeField] private AudioClip _onDie;


        private void Start()
        {
            _audioSource.volume = SoundManager.SoundInstance.GetMasterVolume();
        }

        public void PlayAttackSound()
        {
            int soundIndex = Random.Range(0, _attack.Count);

            _audioSource.PlayOneShot(_attack[soundIndex]);
        }

        public void PlayAttackImpactSound()
        {
            int soundIndex = Random.Range(0, _onAttackImpact.Count);

            _audioSource.PlayOneShot(_onAttackImpact[soundIndex]);
        }

        public void PlaySightSound()
        {
            int soundIndex = Random.Range(0, _onSight.Count);

            _audioSource.PlayOneShot(_onSight[soundIndex]);
        }

        public void PlayOnHurtSound()
        {
            int soundIndex = Random.Range(0, _onHurt.Count);

            _audioSource.PlayOneShot(_onHurt[soundIndex]);
        }

        public void PlayIdleSound()
        {
            if (Random.Range(0f, 1f) < .2f)//Play idles on 20% of calls
            {
                int soundIndex = Random.Range(0, _onIdle.Count);

                _audioSource.PlayOneShot(_onIdle[soundIndex]);
            }
        }

        public void PlayDeathSound()
        {
            AudioSource.PlayClipAtPoint(_onDie, this.transform.position, SoundManager.SoundInstance.GetMasterVolume());
        }
    }
}