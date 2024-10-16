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


        void Start()
        {
            if (_oneTimeSaveId > 0)
            {
                LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();

                levelMgr.AddToCallBackList(this.gameObject);
            }

        }

        public void LoadCallBack(LevelManager levelMgr)
        {
            if(levelMgr.DoesToggleSaveExist(_oneTimeSaveId))
            {
                if (levelMgr.GetLevelToggle(_oneTimeSaveId))
                    TriggerTransmute();

            }
        }

        public void TriggerTransmute()
        {
            PlayFanFare();

            if(_oneTimeSaveId > 0)
            {
                LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();
                levelMgr.SetLevelToggle(_oneTimeSaveId, true);
            }

            if (_pickupPrefab != null)
                TransmuteToItem();

            if (_objectToActivate != null)
                _objectToActivate.SetActive(true);

            DisableObject();
        }

        private void TransmuteToItem()
        {
            Instantiate(_pickupPrefab, this.transform.position, this.transform.rotation);

        }

        private void DisableObject()
        {
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
