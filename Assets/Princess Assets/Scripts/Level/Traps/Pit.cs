using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class Pit : MonoBehaviour
    {
        [SerializeField] Transform _respawnPoint;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("Companion"))
            {
                GameManager.GameInstance.FallPrincess(_respawnPoint.position);
            }

        }

        void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("Companion"))
            {
                GameManager.GameInstance.FallPrincess(_respawnPoint.position);
            }

        }
    }
}
