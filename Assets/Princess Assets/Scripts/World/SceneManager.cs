using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private GameScenes _scene;
        [SerializeField] private string _sceneName;
        [SerializeField] private AudioClip _defaultSceneMusic;
        [SerializeField] private GameObject _checkPointReference;
        [SerializeField] private List<Vector3> _locations;

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
    }
}
