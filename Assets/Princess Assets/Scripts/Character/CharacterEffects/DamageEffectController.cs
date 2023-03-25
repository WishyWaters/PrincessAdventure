using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PrincessAdventure
{
    public class DamageEffectController : MonoBehaviour
    {
        [SerializeField] private GameObject _damageHeartEffectPrefab;
        [SerializeField] private List<AudioClip> _heartDmgSounds;
        [SerializeField] private GameObject _damageCoinEffectPrefab;
        [SerializeField] private List<AudioClip> _coinDmgSounds;


        public void SpawnDamageEffect()
        {
            Instantiate(_damageHeartEffectPrefab, this.transform);

            int soundIndex = Random.Range(0, _heartDmgSounds.Count);
            SoundManager.SoundInstance.PlayEffectSound(_heartDmgSounds[soundIndex]);
        }

        public void SpawnCoinDamageEffect()
        {
            Instantiate(_damageCoinEffectPrefab, this.transform);

            int soundIndex = Random.Range(0, _coinDmgSounds.Count);
            SoundManager.SoundInstance.PlayEffectSound(_coinDmgSounds[soundIndex]);

        }


    }
}