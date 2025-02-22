using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class SparkleReflectController : MonoBehaviour
    {

        [SerializeField] private GameObject _deflectHitPrefab;
        [SerializeField] private AudioClip _deflectProjectileSound;  //magic_deflect_spell_impact1/2
        [SerializeField] private AudioClip _deflectOtherSound;
        private Vector2 _direction;

        public void HandleSparkleReflect(Vector2 direction)
        {
            _direction = direction;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.CompareTag("Enemy"))
            {
                EnemyActionController enemyCtrl = collision.gameObject.GetComponent<EnemyActionController>();

                if (enemyCtrl.CanEnemyBeReflected())
                {
                    Instantiate(_deflectHitPrefab, (Vector3)collision.ClosestPoint(this.transform.position), this.transform.rotation);

                    SoundManager.SoundInstance.PlayEffectSound(_deflectOtherSound);
                    enemyCtrl.ReflectEnemy(_direction);
                }
            }
            else if (collision.CompareTag("Projectile"))
            {
                Instantiate(_deflectHitPrefab, (Vector3)collision.ClosestPoint(this.transform.position), this.transform.rotation);
                SoundManager.SoundInstance.PlayEffectSound(_deflectProjectileSound);
                ProjectileController projCtrl = collision.gameObject.GetComponent<ProjectileController>();
                projCtrl.ReflectProjectile(_direction);
            }
            else if (collision.CompareTag("Transmute"))
            {
                Instantiate(_deflectHitPrefab, (Vector3)collision.ClosestPoint(this.transform.position), this.transform.rotation);

                TransmuteController transCtrl = collision.gameObject.GetComponent<TransmuteController>();
                transCtrl.TriggerTransmute();
            }
            else if (collision.CompareTag("OpenFire"))
            {
                Instantiate(_deflectHitPrefab, (Vector3)collision.ClosestPoint(this.transform.position), this.transform.rotation);

                SmallFireController fireCtrl = collision.GetComponent<SmallFireController>();

                fireCtrl.Extinguish();
            }
            else if (collision.CompareTag("NPC"))
            {
                Instantiate(_deflectHitPrefab, (Vector3)collision.ClosestPoint(this.transform.position), this.transform.rotation);

                MessageInteractionController msgCtrl = collision.GetComponentInChildren<MessageInteractionController>();

                StartCoroutine(ShowHeartMessageOnDelay(msgCtrl));
            }
        }

        IEnumerator ShowHeartMessageOnDelay(MessageInteractionController msgCtrl)
        {
            yield return new WaitForSecondsRealtime(.3f);
            msgCtrl.ShowHeartMessage();
        }
    }
}
