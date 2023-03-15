using UnityEngine;
using System.Collections;


namespace PrincessAdventure
{
    public class LandingController : MonoBehaviour
    {
        [SerializeField] private GameObject _landingEffectPrefab;
        [SerializeField] private int _soundId;

        private GameObject _landingEffect;

        public void HandleLanding()
        {
            if (_landingEffect == null)
                _landingEffect = Instantiate(_landingEffectPrefab, this.transform);

            ParticleSystem landPSys = _landingEffect.GetComponent<ParticleSystem>();

            if (_landingEffect != null && landPSys != null)
                landPSys.Play();

        }
    

    }
}