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

        // Start is called before the first frame update
        void Start()
        {
            _fadePartSys = _fadeEffect.GetComponent<ParticleSystem>();
            _fadePartSys.Stop();
        }

        // Update is called once per frame
        void Update()
        {
            if (_fadePartSys.isPlaying && Time.time > _fadeTimeout)
                FadeOff();
        }

        public void FadeOn()
        {
            _fadeTimeout = Time.time + .3f;

            if (!_fadePartSys.isPlaying)
            {
                _fadePartSys.Play();
                setAlpha(.3f);
                this.gameObject.layer = 7; //princess fade layer
            }

        }

        public void FadeOff()
        {
            _fadePartSys.Stop();
            setAlpha(1f);
            this.gameObject.layer = 6;  //princess default layer

        }

        public void setAlpha(float alpha)
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