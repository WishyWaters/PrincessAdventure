using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class BombController : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private GameObject _magicSphere;
        [SerializeField] private LayerMask _layerMask;

        private float _loadTime = 1.2f;
        private bool _isActive = false;
        private float _startTime;
        private float _endTime;
        private float _origScale = .15f;
        private float _timeSpent;
        private bool _exploding;

        // Start is called before the first frame update
        void Start()
        {
            _startTime = Time.time;
            _endTime = Time.time + _loadTime;
            _magicSphere.transform.localScale = new Vector3(0, 0, 0);
            
            Collider2D[] col = Physics2D.OverlapCircleAll((Vector2)this.transform.position, .5f, _layerMask);
            if (col.Length > 1)
                Explode();
            

        }

        // Update is called once per frame
        void Update()
        {
            if (!_isActive)
            {
                _timeSpent += Time.deltaTime;
                if (Time.time < _endTime)
                {
                    //get percent of time until end
                    float loadPercent = _timeSpent / _loadTime;
                    float newScale = _origScale * loadPercent;
                    _magicSphere.transform.localScale = new Vector3(newScale, newScale, newScale);
                }
                else
                {
                    _magicSphere.transform.localScale = new Vector3(_origScale, _origScale, _origScale);
                    _isActive = true;
                    StartAudio();
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Explode();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {

            Explode();

        }

        public void Explode()
        {
            if (!_exploding)
            {
                _exploding = true;
                Instantiate(_explosionPrefab, this.transform.position, this.transform.rotation);

                Destroy(this.gameObject);
            }
        }

        private void StartAudio()
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = SoundManager.SoundInstance.GetSfxVolume();
                audioSource.Play();
            }
        }


        //private void SetEffectPosition(Vector2 direction, Vector3 worldPosition)
        //{
        //    if (direction == Vector2.down)
        //    {
        //        _loadingPrefab.transform.localPosition = worldPosition + new Vector3(0, -.75f, 0);
        //        _bombPrefab.transform.localPosition = worldPosition + new Vector3(0, -.75f, 0);
        //    }
        //    else if (direction == Vector2.right)
        //    {
        //        _loadingPrefab.transform.localPosition = worldPosition + new Vector3(1.5f, .75f, 0);
        //        _bombPrefab.transform.localPosition = worldPosition + new Vector3(1.5f, .75f, 0);
        //    }
        //    else if (direction == Vector2.up)
        //    {

        //        _loadingPrefab.transform.localPosition = worldPosition + new Vector3(0, 2f, 0);
        //        _bombPrefab.transform.localPosition = worldPosition + new Vector3(0, 2f, 0);
        //    }
        //    else if (direction == Vector2.left)
        //    {
        //        _loadingPrefab.transform.localPosition = worldPosition + new Vector3(-1.5f, .75f, 0);
        //        _bombPrefab.transform.localPosition = worldPosition + new Vector3(-1.5f, .75f, 0);
        //    }
        //}
    }
}
