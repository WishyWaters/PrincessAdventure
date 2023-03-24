using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class CreateSummonController : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private GameObject _summonEffectPrefab;
        [SerializeField] private GameObject _failureEffectPrefab;
        [SerializeField] private AudioClip _summonSound;
        [SerializeField] private AudioClip _failureSound;

        public void HandleSummon(Vector2 direction)
        {
            Vector3 targetPosition = GetWorldPosition(direction, this.transform.position);

            if (CheckValidSummonTarget(targetPosition))
                DoSummon(targetPosition);
            else
                FailSummon(targetPosition);

        }

        private bool CheckValidSummonTarget(Vector3 targetPosition)
        {

            Collider2D[] col = Physics2D.OverlapCircleAll((Vector2)targetPosition, .3f, _layerMask);
            if (col.Length > 0)
                return false;
            else
                return true;
        }

        private void DoSummon(Vector3 targetPosition)
        {
            Instantiate(_summonEffectPrefab, targetPosition, this.transform.rotation);

            if (_summonSound != null)
                SoundManager.SoundInstance.PlayEffectSound(_summonSound);


            GameManager.GameInstance.ActivateCompanion(targetPosition);
        }

        private void FailSummon(Vector3 targetPosition)
        {
            Instantiate(_failureEffectPrefab, targetPosition, this.transform.rotation);

            if (_failureSound != null)
                SoundManager.SoundInstance.PlayEffectSound(_failureSound);
        }

        private Vector3 GetWorldPosition(Vector2 direction, Vector3 worldPosition)
        {
            if (direction == Vector2.down)
            {
                return worldPosition + new Vector3(0, -1f, 0);
            }
            else if (direction == Vector2.right)
            {
                return worldPosition + new Vector3(1.25f, .25f, 0);
            }
            else if (direction == Vector2.up)
            {
                return worldPosition + new Vector3(0, 1f, 0);
            }
            else //left
            {
                return worldPosition + new Vector3(-1.25f, .25f, 0);
            }
        }
    }
}