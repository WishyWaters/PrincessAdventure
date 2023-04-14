using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class SceneTransitionController : MonoBehaviour
    {
        [SerializeField] GameScenes _targetScene;
        [SerializeField] int _targetEntryIndex;
        [SerializeField] AudioClip _teleportClip;
        [SerializeField] FadeTypes _fadeType;

        void OnTriggerEnter2D(Collider2D col)
        {
            //Debug.Log("item enter " + col.name);
            if (col.CompareTag("Player") || col.CompareTag("Companion"))
            {
                SoundManager.SoundInstance.PlayEffectSound(_teleportClip);

                GameManager.GameInstance.MoveToNewScene(_targetScene, _targetEntryIndex, _fadeType);
            }

        }
    }
}
