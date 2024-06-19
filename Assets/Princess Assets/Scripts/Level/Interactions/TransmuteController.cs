using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class TransmuteController : MonoBehaviour
    {
        [SerializeField] private GameObject _pickupPrefab;
        [SerializeField] private AudioClip _fanfareClip;
        [SerializeField] private GameObject _fanfareEffect;
        [SerializeField] private GameObject _objectToActivate;
        [SerializeField] private int _oneTimeSaveId;


        private bool _isDrained;
        // Start is called before the first frame update

        void Start()
        {
            if (_isDrained)
                DisableObject();
        }

        public void TriggerTransmute()
        {
            DisableObject();
            PlayFanFare();

            if (_pickupPrefab != null)
                TransmuteToItem();

            if (_objectToActivate != null)
                _objectToActivate.SetActive(true);
        }

        private void TransmuteToItem()
        {
            Instantiate(_pickupPrefab, this.transform.position, this.transform.rotation);

        }

        private void DisableObject()
        {
            _isDrained = true;
            this.gameObject.SetActive(false);
        }

        private void PlayFanFare()
        {
            if (_fanfareClip != null)
                SoundManager.SoundInstance.PlayEffectSound(_fanfareClip);

            if (_fanfareEffect != null)
            {
                GameObject effect = Instantiate(_fanfareEffect, this.transform.position, this.transform.rotation);

                ParticleSystem effectPSys = effect.GetComponent<ParticleSystem>();

                if (effect != null && effectPSys != null)
                    effectPSys.Play();
            }


        }
    }
}
