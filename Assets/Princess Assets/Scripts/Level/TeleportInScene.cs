using UnityEngine;
using System.Collections;

namespace PrincessAdventure
{
    public class TeleportInScene : MonoBehaviour
    {
        [SerializeField] Transform _targetLocation;
        [SerializeField] AudioClip _teleportClip;
        [SerializeField] FadeTypes _fade = FadeTypes.Enter;

        void OnTriggerEnter2D(Collider2D col)
        {
            //Debug.Log("item enter " + col.name);
            if (col.CompareTag("Player") || col.CompareTag("Companion"))
            {
                SoundManager.SoundInstance.PlayEffectSound(_teleportClip);
                GameManager.GameInstance.TeleportPlayerWithinScene(_targetLocation.position, _fade);
            }

            

        }
    }
}