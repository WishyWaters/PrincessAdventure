using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class PickupController : MonoBehaviour
    {
        [Header("Pick Up Options")]
        [SerializeField] PickUps _itemType;
        [SerializeField] AudioClip _clip;
        [SerializeField] GameObject _item;
        [SerializeField] bool _saveOnPickup;
        [SerializeField] int _toggleSaveId;

        [Header("Spawner Options")]
        [SerializeField] private bool _spawnOnPickup;
        [SerializeField] private GameObject _objectSpawner;

        private Transform _startMarker;
        private Transform _endMarker;
        private float _startTime;
        private float _journeyLength;

        private float _gravity = .05f;
        private float _itemHeight = 0f;
        private float _bounceSpeed = .01f;



        private void Start()
        {
            if (_saveOnPickup)
            {
                LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();

                levelMgr.AddToCallBackList(this.gameObject);
            }

        }

        public void LoadPickupStatus(LevelManager levelMgr)
        {
            if (levelMgr.GetLevelToggle(_toggleSaveId) == true)
                this.gameObject.SetActive(false);
            
        }

        private void Update()
        {
            if (Time.timeScale > 0)
                BounceItem();
        }

        private void BounceItem()
        {
            //Item Bounce...
            //Calculate height of item
            if (_itemHeight > 0)
                _bounceSpeed -= _gravity * Time.deltaTime;

            _itemHeight += _bounceSpeed;

            if (_itemHeight < 0)
            {
                _itemHeight = _itemHeight * -1;
                if (_bounceSpeed < 0)
                    _bounceSpeed = _bounceSpeed * -1f * .6f;
                if (_bounceSpeed < .01)
                {
                    //_itemHeight = 0;
                    _bounceSpeed = .01f;
                }
            }

            _item.transform.localPosition = new Vector3(0, _itemHeight, 0);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            //Debug.Log("item enter " + col.name);
            if (col.tag == "Player" || col.tag == "Companion")
            {
                PickupItem();
            }

        }

        public void PickupItem()
        {
            GameManager.GameInstance.PickupItem(_itemType);

            if (_clip != null)
                SoundManager.SoundInstance.PlayEffectSound(_clip);

            UpdateSave();
            if (_spawnOnPickup)
                SpawnThing();

            Destroy(this.gameObject);
        }


        public void MoveItemToTarget(Vector3 target)
        {
            _startTime = Time.time;
            _startMarker = this.transform;
            _journeyLength = Vector3.Distance(_startMarker.position, target);

            StartCoroutine(DoMovement(target));

        }

        private IEnumerator DoMovement(Vector3 destination)
        {
            //Turn off collider
            CircleCollider2D col = this.GetComponent<CircleCollider2D>();
            col.enabled = false;

            //update item bounce speed
            _itemHeight = 1f;
            _bounceSpeed = -.01f;

            while (this.transform.position != destination)
            {
                //Calculate object movement
                float distCovered = (Time.time - _startTime);
                float fractionOfJourney = distCovered / _journeyLength;

                this.gameObject.transform.position = Vector3.Lerp(_startMarker.position, destination, fractionOfJourney);

                yield return null;
            }

            //turn on collider
            col.enabled = true;
        }

        private void UpdateSave()
        {
            if (_toggleSaveId == 0)
                return;

            //Call level manager and update object data
            LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();

            levelMgr.SetLevelToggle(_toggleSaveId, true);
        }

        private void SpawnThing()
        {
            ObjectSpawner spawner = _objectSpawner.GetComponent<ObjectSpawner>();

            if(spawner != null)
            {
                spawner.StartSpawning();
            }
        }

    }
}
