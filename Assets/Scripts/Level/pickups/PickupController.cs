using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class PickupController : MonoBehaviour
    {
        [SerializeField] PickUps _itemType;
        [SerializeField] AudioClip _clip;


        void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("item enter " + col.name);
            if (col.gameObject.tag == "Player")
            {
                GameManager.GameInstance.PickupItem(_itemType);

                if (_clip != null)
                    SoundManager.SoundInstance.PlayEffectSound(_clip);

               Destroy(this.gameObject);
            }

        }
    }
}
