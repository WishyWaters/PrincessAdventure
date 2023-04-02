using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class BoomshroomController : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private Color _tintColor;
        [SerializeField] private List<SpriteRenderer> _renderers;
        [SerializeField] private bool _ExplodeOnTouch;

        private bool _exploding;
        private float _boomCount;
        private Color _originalColor;

        // Start is called before the first frame update
        void Start()
        {
            _originalColor = _renderers[0].color;
        }

        // Update is called once per frame
        void Update()
        {
            if(_exploding)
            {
                _boomCount += Time.deltaTime;
                if (_boomCount > 1)
                    Explode();
                else
                    TintSprite();
            }
        }

        public void StartExplode()
        {
            if (!_exploding)
            {
                _exploding = true;
                _boomCount = 0;
            }
        }

        private void Explode()
        {

            Instantiate(_explosionPrefab, this.transform.position, this.transform.rotation);

            Destroy(this.gameObject);

        }

        private void TintSprite()
        {
            float percentDone = _boomCount / 1f;
            foreach (SpriteRenderer renderer in _renderers)
            {
                renderer.color = Color.Lerp(_originalColor, _tintColor, percentDone);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(_ExplodeOnTouch)
                StartExplode();
        }

        //Small boomshrooms are triggers
        void OnTriggerEnter2D(Collider2D collision)
        {
            if(_ExplodeOnTouch)
                StartExplode();

        }
    }
}
