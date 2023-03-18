using UnityEngine;
using System.Collections;


namespace PrincessAdventure
{
    public class PotionPickupEffectController : MonoBehaviour
    {
        [SerializeField] private GameObject _redPotionEffectPrefab;
        [SerializeField] private GameObject _bluePotionEffectPrefab;
        [SerializeField] private GameObject _greenPotionEffectPrefab;

        [SerializeField] private AudioClip _drinkSound;
        [SerializeField] private AudioClip _redSound;
        [SerializeField] private AudioClip _blueSound;
        [SerializeField] private AudioClip _greenSound;

        private GameObject _redEffect;
        private GameObject _blueEffect;
        private GameObject _greenEffect;


        public void SpawnPotionEffect(PickUps pickup)
        {
            if (_drinkSound != null)
                SoundManager.SoundInstance.PlayEffectSound(_drinkSound);

            switch (pickup)
            {
                case PickUps.RedPotion:
                    HandleRedPotion();
                    break;
                case PickUps.BluePotion:
                    HandleBluePotion();
                    break;
                case PickUps.GreenPotion:
                    HandleGreenPotion();
                    break;
            }

        }

        public void HandleRedPotion()
        {
            if (_redEffect == null)
                _redEffect = Instantiate(_redPotionEffectPrefab, this.transform);

            ParticleSystem redPSys = _redEffect.GetComponent<ParticleSystem>();

            if (_redEffect != null && redPSys != null)
                redPSys.Play();

            if (_redSound != null)
                SoundManager.SoundInstance.PlayEffectSound(_redSound);
        }

        public void HandleBluePotion()
        {
            if (_blueEffect == null)
                _blueEffect = Instantiate(_bluePotionEffectPrefab, this.transform);

            ParticleSystem bluePSys = _blueEffect.GetComponent<ParticleSystem>();

            if (_blueEffect != null && bluePSys != null)
                bluePSys.Play();

            if (_blueSound != null)
                SoundManager.SoundInstance.PlayEffectSound(_blueSound);
        }

        public void HandleGreenPotion()
        {
            if (_greenEffect == null)
                _greenEffect = Instantiate(_greenPotionEffectPrefab, this.transform);

            ParticleSystem greenPSys = _greenEffect.GetComponent<ParticleSystem>();

            if (_greenEffect != null && greenPSys != null)
                greenPSys.Play();

            if (_greenSound != null)
                SoundManager.SoundInstance.PlayEffectSound(_greenSound);
        }

    }
}