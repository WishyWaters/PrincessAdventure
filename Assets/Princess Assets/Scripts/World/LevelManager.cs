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

        private LevelMessages _levelMessages;

        private void Start()
        {
            if(_levelMessageData != null)
                _levelMessages = JsonUtility.FromJson<LevelMessages>(_levelMessageData.text);

        }

        public void SaveLevelDetails()
        {

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

    }
}
