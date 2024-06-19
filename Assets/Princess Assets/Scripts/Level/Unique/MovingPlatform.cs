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

        private int _currentTargetIndex;
        private float _idleUntil;
        private bool _movementActive = true;
        private Vector2 _previousPosition;

        private List<GameObject> _riders;

        // Use this for initialization
        void Start()
        {
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
                MoveRiders(moveDiff);
                _previousPosition = nextTarget;

            }

        }

        private void MoveRiders(Vector2 forcedMove)
        {
            foreach (GameObject rider in _riders)
            {
                if(rider.CompareTag("Player"))
                {
                    CharacterController charCtrl = rider.GetComponent<CharacterController>();

                    if (charCtrl != null)
                        charCtrl.AddForcedMovement(forcedMove);
                }

                //TODO: Repeat for companion & monsters
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
            

            _riders.Add(collision.gameObject);

            
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //collision.transform.SetParent(null);
            _riders.Remove(collision.gameObject);
        }

        private void Connect(Rigidbody2D player)
        {
            FixedJoint2D joint = GetComponent<FixedJoint2D>();
            joint.connectedBody = player;
        }


    }
}
