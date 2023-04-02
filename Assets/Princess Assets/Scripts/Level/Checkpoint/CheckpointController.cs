using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PrincessAdventure
{
    public class CheckpointController : MonoBehaviour
    {
        [SerializeField] private GameObject _activeCheckpointEffectReference;
        [SerializeField] private GameObject _onActivateEffectPrefab;

        private bool _checkpointIsActive;
        private float _regenTime;

        // Start is called before the first frame update
        void Start()
        {
            //TODO: Load _checkpointIsActive from ??
            CheckIfActive();
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void CheckIfActive()
        {
            if(_checkpointIsActive)
            {
                ActivateCheckpoint();
            }
            else
                _activeCheckpointEffectReference.SetActive(false);
        }

        private void ActivateCheckpoint()
        {
            if(!_checkpointIsActive)
            {
                Instantiate(_onActivateEffectPrefab, this.transform);
                _activeCheckpointEffectReference.SetActive(true);
                _checkpointIsActive = true;
                GameManager.GameInstance.UpdatePlayerGameScene();
                GameManager.GameInstance.HealPrincess();
                GameManager.GameInstance.ManaPotionUse();
            }    
        }

        private void PlayerRegen()
        {
            if (GameManager.GameInstance.DoesPrincessNeedRegen())
            {
                if (_regenTime > 2f)
                {
                    _regenTime = 0;
                    Instantiate(_onActivateEffectPrefab, this.transform);
                    GameManager.GameInstance.HealPrincess();
                    GameManager.GameInstance.ManaPotionUse();
                }
                else
                    _regenTime += Time.deltaTime;
            }
        }

        #region Physics2d

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log("item enter " + col.name);
            if (collision.tag == "Player")
            {
                ActivateCheckpoint();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                PlayerRegen();
            }

        }

        #endregion
    }
}
