using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{

    public class TriggerScriptedMoveController : MonoBehaviour
    {
        [SerializeField] private ScriptedMovementController _moveController;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_moveController == null || _moveController.gameObject == null)
                return;

            if (collision.CompareTag("Player") || collision.CompareTag("Companion"))
            {
                _moveController.BeginMovement();
            }
        }
    }
}
