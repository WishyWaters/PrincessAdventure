using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class ExplosionController : MonoBehaviour
    {
        [SerializeField] private AudioClip _blastSound;

        // Start is called before the first frame update
        void Start()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, 2f);

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

            //TODO: If destructable, then destroy it!
        }
    
    }
}
