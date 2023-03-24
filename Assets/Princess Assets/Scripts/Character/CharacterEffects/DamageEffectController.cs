using UnityEngine;
using System.Collections;


namespace PrincessAdventure
{
    public class DamageEffectController : MonoBehaviour
    {
        [SerializeField] private GameObject _damageEffectPrefab;
        [SerializeField] private int _soundId;

        private GameObject _dmgEffect;

        public void SpawnDamageEffect()
        {
            if (_dmgEffect == null)
                _dmgEffect = Instantiate(_damageEffectPrefab, this.transform);

            ParticleSystem dbgPSys = _dmgEffect.GetComponent<ParticleSystem>();

            if (_dmgEffect != null && dbgPSys != null)
                dbgPSys.Play();



        }
    

    }
}