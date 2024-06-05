using UnityEngine;
using System.Collections;

namespace PrincessAdventure
{
    public class TeleportInScene : MonoBehaviour
    {
        [SerializeField] Transform _targetLocation;
        [SerializeField] AudioClip _teleportClip;
        [SerializeField] FadeTypes _fade = FadeTypes.Enter;
        [SerializeField] int _changeMusic = 0;

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                if (_changeMusic == 1)
                {
                    LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();
                    SoundManager.SoundInstance.ChangeMusic(levelMgr.GetDefaultLevelMusic(), true);
                }
                else if (_changeMusic == 2)
                {
                    LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();
                    SoundManager.SoundInstance.ChangeMusic(levelMgr.GetSecondaryLevelMusic(), true);
                }
            }

            //Debug.Log("item enter " + col.name);
            if (col.CompareTag("Player") || col.CompareTag("Companion"))
            {
                SoundManager.SoundInstance.PlayEffectSound(_teleportClip);
                GameManager.GameInstance.TeleportPlayerWithinScene(_targetLocation.position, _fade);
            }

            

        }
    }
}