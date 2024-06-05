using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class ExplosionController : MonoBehaviour
    {
        [SerializeField] private AudioClip _blastSound;
        [SerializeField] private float _blastRadius;

        // Start is called before the first frame update
        void Start()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, _blastRadius);

            if(hits != null)
            {
                foreach (Collider2D hit in hits)
                    CheckHit(hit);
            }

            SoundManager.SoundInstance.PlayEffectSound(_blastSound);
        }

        private void CheckHit(Collider2D hit)
        {
            if(hit.tag == "Player" && hit.gameObject.layer == 6)
            {
                GameManager.GameInstance.DamagePrincess(this.transform.position);
            }
            else if (hit.tag == "Companion")
            {
                GameManager.GameInstance.ActivatePrincess(true);
            }
            else if (hit.tag == "Bomb")
            {
                BombController bCtrl = hit.GetComponent<BombController>();

                bCtrl.Explode();
            }
            else if (hit.tag == "Enemy")
            {
                EnemyActionController enemyCtrl = hit.GetComponent<EnemyActionController>();

                enemyCtrl.DamageEnemy(this.transform.position, true);
            }
            else if (hit.tag == "Boomshroom")
            {
                BoomshroomController shroomCtrl = hit.GetComponent<BoomshroomController>();

                shroomCtrl.StartExplode();
            }
            else if (hit.tag == "Destructible")
            {
                DestructibleController destCtrl = hit.GetComponent<DestructibleController>();

                destCtrl.RemoveDestructable();
            }
            else if (hit.tag == "Ice")
            {
                DestructibleController destCtrl = hit.GetComponent<DestructibleController>();

                destCtrl.RemoveDestructable();
            }
            else if (hit.tag == "Guard")
            {
                GuardianMonsterController guardCtrl = hit.GetComponent<GuardianMonsterController>();

                guardCtrl.DamageEnemy();
            }
        }
    
    }
}
