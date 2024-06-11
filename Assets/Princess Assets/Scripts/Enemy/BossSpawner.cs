using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] private int _bossToggleSaveId;

        [SerializeField] private AudioClip _bossMusic;
        [SerializeField] private List<GameObject> _objectsToActivateOnStart;
        [SerializeField] private List<GameObject> _objectsToDeactivateOnEnd;

        [SerializeField] private List<GameObject> _objectsToToggleAffectedOnStart;
        [SerializeField] private List<GameObject> _objectsToToggleAffectedOnEnd;

        [SerializeField] private AudioClip _victoryStinger;

        //[SerializeField] private GameObject _starShardToActivate;


        [SerializeField] private float _cameraMoveTime;
        [SerializeField] private GameObject _cameraMoveTarget;

        private bool _wasActivated;

        private void Start()
        {
            LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();

            levelMgr.AddToCallBackList(this.gameObject);
        }

        public void SetWasActivated(LevelManager levelMgr)
        {
            if (levelMgr.DoesToggleSaveExist(_bossToggleSaveId))
            {
                if (levelMgr.GetLevelToggle(_bossToggleSaveId) == true)
                    _wasActivated = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!_wasActivated && collision.CompareTag("Player"))
                StartCoroutine(BeginBossFight());
        }

        private IEnumerator BeginBossFight()
        {
            _wasActivated = true;

            GameManager.GameInstance.UpdateCameraFollow(_cameraMoveTarget);

            float timeCount = 0;
            while (timeCount < _cameraMoveTime)
            {
                timeCount += Time.deltaTime;
                yield return null;
            }

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

            timeCount = 0;
            while (timeCount < _cameraMoveTime)
            {
                timeCount += Time.deltaTime;
                yield return null;
            }

            GameManager.GameInstance.UpdateCameraFollowToPlayer();


        }

        public void EndBossFight()
        {
            LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();
            

            SoundManager.SoundInstance.ChangeMusic(levelMgr.GetDefaultLevelMusic(), true);

            foreach (GameObject go in _objectsToDeactivateOnEnd)
            {
                go.SetActive(false);
            }

            foreach (GameObject go in _objectsToToggleAffectedOnEnd)
            {
                AffectedObjectController affected = go.GetComponent<AffectedObjectController>();
                affected.ToggleTheObject();
            }

            //_starShardToActivate.SetActive(true);
        }

    }
}
