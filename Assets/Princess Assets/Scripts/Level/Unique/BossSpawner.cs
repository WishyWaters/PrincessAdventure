using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] private AudioClip _bossMusic;
        [SerializeField] private List<GameObject> _objectsToActivateOnStart;
        [SerializeField] private List<GameObject> _objectsToDeactivateOnEnd;

        [SerializeField] private List<GameObject> _objectsToToggleAffectedOnStart;
        [SerializeField] private List<GameObject> _objectsToToggleAffectedOnEnd;

        [SerializeField] private AudioClip _victoryStinger;

        [SerializeField] private GameObject _starShardToActivate;

        private bool _wasActivated;

        private void Start()
        {
            //TODO: Check if boss was defeated w/ level manager

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!_wasActivated)
                BeginBossFight();
        }

        private void BeginBossFight()
        {
            _wasActivated = true;

            SoundManager.SoundInstance.ChangeMusic(_bossMusic, true);

            foreach (GameObject go in _objectsToActivateOnStart)
            {
                go.SetActive(true);
            }

            foreach (GameObject go in _objectsToToggleAffectedOnStart)
            {
                AffectedObjectController affected = go.GetComponent<AffectedObjectController>();
                affected.ToggleTheObject();
            }


        }

        public void EndBossFight()
        {
            LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();
            //TODO: Save boss defeated

            SoundManager.SoundInstance.ChangeMusic(levelMgr.GetLevelMusic(), true);

            foreach (GameObject go in _objectsToDeactivateOnEnd)
            {
                go.SetActive(false);
            }

            foreach (GameObject go in _objectsToToggleAffectedOnEnd)
            {
                AffectedObjectController affected = go.GetComponent<AffectedObjectController>();
                affected.ToggleTheObject();
            }

            _starShardToActivate.SetActive(true);
        }

    }
}
