using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PrincessAdventure
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameScenes _scene;
        [SerializeField] private string _levelName;
        [SerializeField] private AudioClip _defaultLevelMusic;
        [SerializeField] private AudioClip _secondaryLevelMusic;
        [SerializeField] private GameObject _checkPointReference;
        [SerializeField] private List<Transform> _levelEntrances;
        [SerializeField] private TextAsset _levelMessageData;

        private LevelSave _levelDetails;
        private LevelMessages _levelMessages;
        private List<GameObject> _affectedObjectsToLoad = new List<GameObject>();
        private bool _levelIsLoaded;

        public void LoadLevelDetails(int saveId)
        {
            if (_scene != GameScenes.MainMenu && _scene != GameScenes.CharCreator)
            {
                if (_levelMessageData != null)
                    _levelMessages = JsonUtility.FromJson<LevelMessages>(_levelMessageData.text);

                _levelDetails = SaveDataManager.LoadLevelDetails(saveId, _scene);

                _levelIsLoaded = true;
                LoadAffectedObjects();
            }

            
        }

        private void LoadAffectedObjects()
        {
            foreach(GameObject affectedObj in _affectedObjectsToLoad)
            {
                CallbackAndLoadAffected(affectedObj);
            }
        }

        private void CallbackAndLoadAffected(GameObject affectedObject)
        {
            
            if (affectedObject.GetComponent<AffectedObjectController>() != null)
            {
                AffectedObjectController afObjCtrl = affectedObject.GetComponent<AffectedObjectController>();

                if (DoesToggleSaveExist(afObjCtrl.GetToggleSaveId()))
                {
                    afObjCtrl.LoadToggleStatus(GetLevelToggle(afObjCtrl.GetToggleSaveId()));
                }
            }
            else if(affectedObject.GetComponent<PickupController>() != null)
            {
                PickupController pickupCtrl = affectedObject.GetComponent<PickupController>();
                pickupCtrl.LoadPickupStatus((LevelManager)this);
            }
            else if (affectedObject.GetComponent<Interaction>() != null)
            {
                Interaction interaction = affectedObject.GetComponent<Interaction>();
                interaction.LeverToggleCallback();
            }
            else if (affectedObject.GetComponent<BossSpawner>() != null)
            {
                BossSpawner boss = affectedObject.GetComponent<BossSpawner>();
                boss.SetWasActivated((LevelManager)this);
            }
            else if (affectedObject.GetComponent<EnemyBehaviorController>() != null)
            {
                EnemyBehaviorController enemy = affectedObject.GetComponent<EnemyBehaviorController>();
                enemy.SetEnemyActive((LevelManager)this);
            }
            else if (affectedObject.GetComponent<ScriptedMovementController>() != null)
            {
                ScriptedMovementController move = affectedObject.GetComponent<ScriptedMovementController>();
                move.ActivateMovement((LevelManager)this);
            }
            else if (affectedObject.GetComponent<PuzzleCheckController>() != null)
            {
                PuzzleCheckController puzzleCheck = affectedObject.GetComponent<PuzzleCheckController>();
                puzzleCheck.PuzzleLoad((LevelManager)this);
            }
            else if(affectedObject.GetComponent<TransmuteController>() != null)
            {
                TransmuteController transmuteCtrl = affectedObject.GetComponent<TransmuteController>();
                transmuteCtrl.LoadCallBack((LevelManager)this);
            }
        }

        public void AddToCallBackList(GameObject affectedObject)
        {
            if (_levelIsLoaded)
                CallbackAndLoadAffected(affectedObject);
            else
                _affectedObjectsToLoad.Add(affectedObject);
        }

        public void SaveLevelDetails(int saveId)
        {
            SaveDataManager.SaveLevelDetails(saveId, _scene, _levelDetails);
        }

        public GameScenes GetCurrentLevel()
        {
            return _scene;
        }

        public GameObject GetLevelCheckPoint()
        {
            return _checkPointReference;
        }

        public AudioClip GetDefaultLevelMusic()
        {
            return _defaultLevelMusic;
        }

        public AudioClip GetSecondaryLevelMusic()
        {
            return _secondaryLevelMusic;
        }

        public Transform GetLevelEntryPoint(int entryIndex)
        {
            return _levelEntrances[entryIndex];
        }


        public string GetMessageText(int msgId)
        {
            if (_levelMessages.messages.Exists(x => x.id == msgId))
                return _levelMessages.messages.Where(x => x.id == msgId).FirstOrDefault().message;
            else
                return "";
        }

        public bool DoesToggleSaveExist(int id)
        {
            if (_levelDetails.toggles.Exists(x => x.id == id))
                return true;

            return false;
        }

        public bool GetLevelToggle(int id)
        {
            if (_levelDetails.toggles.Exists(x => x.id == id))
                 return _levelDetails.toggles.Where(x => x.id == id).First().wasToggled;

            return false;
        }

        public void SetLevelToggle(int id, bool value)
        {
            if (_levelDetails.toggles.Exists(x => x.id == id))
                _levelDetails.toggles.Where(x => x.id == id).First().wasToggled = value;
            else
            {
                _levelDetails.toggles.Add(new ToggleSave(id, value));
            }
        }

        private bool DoesReferenceSaveExist(int id)
        {
            if (_levelDetails.references.Exists(x => x.id == id))
                return true;

            return false;
        }

        private int GetReferenceValue(int id)
        {
            if (_levelDetails.references.Exists(x => x.id == id))
                return _levelDetails.references.Where(x => x.id == id).First().referenceId;

            return 0;
        }

        private void SetReferenceValue(int id, int value)
        {
            if (_levelDetails.references.Exists(x => x.id == id))
                _levelDetails.references.Where(x => x.id == id).First().referenceId = value;
            else
            {
                _levelDetails.references.Add(new ReferenceSave(id, value));
            }
        }

    }
}
