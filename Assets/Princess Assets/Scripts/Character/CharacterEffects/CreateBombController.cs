using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class CreateBombController : MonoBehaviour
    {
        [SerializeField] private GameObject _bombPrefab;
        [SerializeField] private AudioClip _dropBombSound;

        public void HandleBombDrop(Vector2 direction)
        {
            GameObject bomb = Instantiate(_bombPrefab, GetWorldPosition(direction, this.transform.position), this.transform.rotation);

            if (_dropBombSound != null)
                SoundManager.SoundInstance.PlayEffectSound(_dropBombSound);

        }


        private Vector3 GetWorldPosition(Vector2 direction, Vector3 worldPosition)
        {
            if (direction == Vector2.down)
            {
                return worldPosition + new Vector3(0, -1.25f, 0);
            }
            else if (direction == Vector2.right)
            {
                return worldPosition + new Vector3(1.5f, .75f, 0);
            }
            else if (direction == Vector2.up)
            {
                return worldPosition + new Vector3(0, 2f, 0);
            }
            else //left
            {
                return worldPosition + new Vector3(-1.5f, .75f, 0);
            }
        }
    }
}
