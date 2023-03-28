using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class SparkleController : MonoBehaviour
    {
        [SerializeField] private GameObject _sparkleEffectPrefab;
        [SerializeField] private AudioClip _sparkleSound;
        [SerializeField] private LayerMask _layersToReflect;

        [SerializeField] private GameObject _deflectHitPrefab;
        [SerializeField] private AudioClip _deflectProjectileSound;  //magic_deflect_spell_impact1/2
        [SerializeField] private AudioClip _deflectOtherSound;
        private GameObject _sparkleEffect;
        private Vector2 _direction;

        public void HandleSparkleCast(Vector2 direction)
        {
            _direction = direction;
            if (_sparkleEffect == null)
                _sparkleEffect = Instantiate(_sparkleEffectPrefab, this.transform);

            //Move & Rotate to match direction
            SetEffectPosition(direction);
            _sparkleEffect.SetActive(true);

            ParticleSystem partSys = _sparkleEffect.GetComponent<ParticleSystem>();

            if (_sparkleEffect != null && partSys != null)
                partSys.Play();

            if (_sparkleSound != null)
                SoundManager.SoundInstance.PlayEffectSound(_sparkleSound);
        }

        private void SetEffectPosition(Vector2 direction)
        {
            if (direction == Vector2.down)
            {
                _sparkleEffect.transform.localRotation = Quaternion.Euler(0, 0, 0);
                _sparkleEffect.transform.localPosition = new Vector3(0, 0, 0);
            }
            else if (direction == Vector2.right)
            {
                _sparkleEffect.transform.localRotation = Quaternion.Euler(0, 0, 90);
                _sparkleEffect.transform.localPosition = new Vector3(.5f, .2f, 0);
            }
            else if (direction == Vector2.up)
            {
                _sparkleEffect.transform.localRotation = Quaternion.Euler(0, 0, 180);
                _sparkleEffect.transform.localPosition = new Vector3(0, .7f, 0);
            }
            else if (direction == Vector2.left)
            {
                _sparkleEffect.transform.localRotation = Quaternion.Euler(0, 0, 270);
                _sparkleEffect.transform.localPosition = new Vector3(-.5f, .5f, 0);
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.tag == "Enemy")
            {
                
                Instantiate(_deflectHitPrefab, (Vector3)collision.ClosestPoint(this.transform.position), this.transform.rotation);
                SoundManager.SoundInstance.PlayEffectSound(_deflectOtherSound);
                EnemyController enemyCtrl = collision.gameObject.GetComponent<EnemyController>();
                enemyCtrl.ReflectEnemy(_direction);
            }
            else if (collision.tag == "Projectile")
            {
                Instantiate(_deflectHitPrefab, (Vector3)collision.ClosestPoint(this.transform.position), this.transform.rotation);
                SoundManager.SoundInstance.PlayEffectSound(_deflectProjectileSound);
                ProjectileController projCtrl = collision.gameObject.GetComponent<ProjectileController>();
                projCtrl.ReflectProjectile(_direction);
            }
            else if (collision.tag == "Pickup")
            {
                PickupController pickupCtrl = collision.gameObject.GetComponent<PickupController>();
                pickupCtrl.PickupItem();
            }
            else if (collision.tag == "Transmute")
            {
                Instantiate(_deflectHitPrefab, (Vector3)collision.ClosestPoint(this.transform.position), this.transform.rotation);

                TransmuteController transCtrl = collision.gameObject.GetComponent<TransmuteController>();
                transCtrl.TransmuteToItem();
            }
        }
    }
}
