using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class ChangeSceneMusicTrigger : MonoBehaviour
    {
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

        }
    }
}