using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PrincessAdventure
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private GameScenes _scene;
        [SerializeField] private string _sceneName;
        [SerializeField] private AudioClip _defaultSceneMusic;
        [SerializeField] private GameObject _checkPointReference;
        [SerializeField] private List<Vector3> _locations;
        [SerializeField] private TextAsset _sceneMessageData;

        private SceneMessages _sceneMessages;

        private void Start()
        {
            _sceneMessages = JsonUtility.FromJson<SceneMessages>(_sceneMessageData.text);

        }

        public GameScenes GetCurrentScene()
        {
            return _scene;
        }

        public GameObject GetSceneCheckPoint()
        {
            return _checkPointReference;
        }

        public AudioClip GetSceneMusic()
        {
            return _defaultSceneMusic;
        }


        public string GetMessageText(int msgId)
        {
            if (_sceneMessages.messages.Exists(x => x.id == msgId))
                return _sceneMessages.messages.Where(x => x.id == msgId).FirstOrDefault().message;
            else
                return "";
        }

    }
}
