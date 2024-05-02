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
        [SerializeField] private GameObject _checkPointReference;
        [SerializeField] private List<Transform> _levelEntrances;
        [SerializeField] private TextAsset _levelMessageData;

        private LevelSave _levelDetails;
        private LevelMessages _levelMessages;

        private void Awake()
        {
            if (_scene != GameScenes.MainMenu && _scene != GameScenes.CharCreator)
            {
                if (_levelMessageData != null)
                    _levelMessages = JsonUtility.FromJson<LevelMessages>(_levelMessageData.text);

                _levelDetails = SaveDataManager.LoadLevelDetails(GameManager.GameInstance.GetSaveId(), _scene);
            }
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

        public AudioClip GetLevelMusic()
        {
            return _defaultLevelMusic;
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

        public bool DoesReferenceSaveExist(int id)
        {
            if (_levelDetails.references.Exists(x => x.id == id))
                return true;

            return false;
        }

        public int GetReferenceValue(int id)
        {
            if (_levelDetails.references.Exists(x => x.id == id))
                return _levelDetails.references.Where(x => x.id == id).First().referenceId;

            return 0;
        }

        public void SetReferenceToggle(int id, int value)
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
