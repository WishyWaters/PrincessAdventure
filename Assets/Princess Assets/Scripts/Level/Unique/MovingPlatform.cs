using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private ScriptedMoveTypes _moveType;
        [SerializeField] private int _moveSpeed;
        [SerializeField] private float _idleTimeBetweenMoves;
        [SerializeField] private List<Transform> _targets;
        [SerializeField] private Rigidbody2D _platformRb;
        [SerializeField] private List<GameObject> _riders;

        private int _currentTargetIndex;
        private float _idleUntil;
        private bool _movementActive = true;
        private Vector2 _previousPosition;

        
        
        // Use this for initialization
        void Start()
        {
            if(_riders == null)
                _riders = new List<GameObject>();

            transform.position = _targets[0].position;
            _previousPosition = _targets[0].position;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(NearTarget())
            {
                _currentTargetIndex++;
                if(_currentTargetIndex >= _targets.Count)
                    _currentTargetIndex = 0;

                if (_idleTimeBetweenMoves > 0)
                    _idleUntil = Time.time + _idleTimeBetweenMoves;
            }

            if(_movementActive && Time.time > _idleUntil)
            {
                

                Vector2 nextTarget = Vector2.MoveTowards(transform.position, _targets[_currentTargetIndex].position, _moveSpeed * Time.deltaTime);
                _platformRb.MovePosition(nextTarget);
                //transform.position = Vector2.MoveTowards(transform.position, _targets[_currentTargetIndex].position, _moveSpeed * Time.deltaTime);

                Vector2 moveDiff = nextTarget - _previousPosition;
                _previousPosition = nextTarget;
                MoveRiders(moveDiff);
                

            }

        }

        private void MoveRiders(Vector2 forcedMove)
        {
            for (int i = _riders.Count - 1; i >= 0; i--)
            {
                if (_riders[i] == null)
                { 
                    _riders.RemoveAt(i);
                    continue;
                }

                if (_riders[i].CompareTag("Player"))
                {
                    CharacterController charCtrl = _riders[i].GetComponent<CharacterController>();

                    if (charCtrl != null)
                        charCtrl.AddForcedMovement(forcedMove);
                }
                else if (_riders[i].CompareTag("Companion"))
                {
                    CompanionController compCtrl = _riders[i].GetComponent<CompanionController>();

                    if (compCtrl != null)
                        compCtrl.AddForcedMovement(forcedMove);
                }
                else
                {
                    _riders[i].GetComponent<Rigidbody2D>().MovePosition(_previousPosition);
                }
                
            }

        }

        private bool NearTarget()
        {
            if (_targets == null || _targets.Count == 0)
                return true;

            if (Vector2.Distance(_targets[_currentTargetIndex].position, transform.position) > .2f)
                return false;

            return true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_riders.IndexOf(collision.gameObject) < 0)
            {

                if (collision.CompareTag("Player") || collision.CompareTag("Companion"))
                {
                    _riders.Add(collision.gameObject);
                    GameManager.GameInstance.IncrementFallValue(1);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_riders.IndexOf(collision.gameObject) >= 0)
            {
                if (collision.CompareTag("Player") || collision.CompareTag("Companion"))
                {
                    _riders.Remove(collision.gameObject);
                    GameManager.GameInstance.IncrementFallValue(-1);
                }
            }
        }



    }
}
