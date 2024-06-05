using UnityEngine;
using System.Collections;

namespace PrincessAdventure
{
    public class IceSpikeTrap : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _onHitEffect;
        [SerializeField] private AudioClip _onHitSound;
        [SerializeField] private AudioClip _regenSound;
        [SerializeField] private PolygonCollider2D _hitBox;
        [SerializeField] private SpriteRenderer _topRenderer;
        [SerializeField] private SpriteRenderer _bottomRenderer;
        [SerializeField] private Sprite _littleIce;
        [SerializeField] private Sprite _bigIceTop;
        [SerializeField] private Sprite _bigIceBottom;

        [Header("Settings")]
        [SerializeField] private float _regenTime;

        private float _regenCountDown = 0;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_regenCountDown <= 0)
                return;

            _regenCountDown -= Time.deltaTime;

            if (_regenCountDown <= 0)
                ActivateIceSpike();
        }

        private void ActivateIceSpike()
        {
            _hitBox.enabled = true;
            _topRenderer.enabled = true;
            _bottomRenderer.sprite = _bigIceBottom;
            SoundManager.SoundInstance.PlayEffectSound(_regenSound);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _hitBox.enabled = false;
            Instantiate(_onHitEffect, this.transform.position, this.transform.rotation);

            _topRenderer.enabled = false;
            _bottomRenderer.sprite = _littleIce;
            SoundManager.SoundInstance.PlayEffectSound(_onHitSound);

            _regenCountDown = _regenTime;

            if (collision.CompareTag("Player") && collision.gameObject.layer == 6)
            {
                GameManager.GameInstance.DamagePrincess(this.transform.position);
            }
            else if (collision.CompareTag("Companion"))
            {
                GameManager.GameInstance.ActivatePrincess(true);
            }
            else if (collision.CompareTag("Enemy"))
            {
                EnemyActionController enemyCtrl = collision.GetComponent<EnemyActionController>();

                enemyCtrl.DamageEnemy(this.transform.position, false);
            }

        }
    }
}

