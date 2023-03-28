using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class FadeController : MonoBehaviour
    {
        [SerializeField] GameObject _fadeEffect;
        private ParticleSystem _fadePartSys;
        private float _fadeTimeout;

        private readonly int _fadeManaPerSecond = 10;
        private float _fadeManaTicker;


        // Start is called before the first frame update
        void Start()
        {
            _fadePartSys = _fadeEffect.GetComponent<ParticleSystem>();
            _fadePartSys.Stop();
        }

        // Update is called once per frame
        void Update()
        {
            if (_fadePartSys.isPlaying)
                _fadeManaTicker += Time.deltaTime;

            if (!FadeIsPaid() || (_fadePartSys.isPlaying && Time.time > _fadeTimeout))
                FadeOff();
            
        }

        public void FadeOn()
        {
            _fadeTimeout = Time.time + .3f;

            if (FadeIsPaid() && !_fadePartSys.isPlaying)
            {
                _fadePartSys.Play();
                setAlpha(.3f);
                this.gameObject.layer = 7; //princess fade layer
            }

        }

        private bool FadeIsPaid()
        {
            if (_fadeManaTicker < 1)
                return true;
            else
            {
                if (GameManager.GameInstance.HasMana(_fadeManaPerSecond))
                {
                    GameManager.GameInstance.ManaSpend(_fadeManaPerSecond);
                    _fadeManaTicker = 0;
                    return true;
                }
                else
                    return false;
            }
        }

        private void FadeOff()
        {
            _fadePartSys.Stop();
            setAlpha(1f);
            this.gameObject.layer = 6;  //princess default layer

        }

        private void setAlpha(float alpha)
        {
            SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>(true);
            Color newColor;
            foreach (SpriteRenderer child in children)
            {
                newColor = child.color;
                newColor.a = alpha;
                child.color = newColor;
            }
        }
    }
}