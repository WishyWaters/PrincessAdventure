using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class ScriptedMovementController : MonoBehaviour
    {

        [Header("Movement Settings")]
        [SerializeField] private ScriptedMoveTypes _moveType;
        [SerializeField] private float _acceleration; //8+
        [SerializeField] private float _moveSpeed; //2 to 7
        [SerializeField] private bool _startOnAwake;
        [SerializeField] private float _idleTimeBetweenMoves;
        [SerializeField] private int _onlyOnceSaveId;
        [SerializeField] private Vector2 _startDirection;

        [Header("References")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private EnemyAnimateController _animateCtrl;
        [SerializeField] private GameObject _finishedEffect;

        [Header("Targets")]
        [SerializeField] private List<Transform> _targets;

        private bool _activeMovement = false;

        private float _currentAcceleration;
        private Vector2 _lastActiveAxis;
        private Vector2 _moveAxis;

        private int _targetIndex;
        private float _idleUntil;


        // Start is called before the first frame update
        void Start()
        {
            LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();

            _moveAxis = _startDirection;
            _lastActiveAxis = _startDirection;

            if (_onlyOnceSaveId > 0 && levelMgr.DoesToggleSaveExist(_onlyOnceSaveId))
            {
                if (levelMgr.GetLevelToggle(_onlyOnceSaveId) == true)
                {
                    Destroy(this.gameObject);
                    return;
                }
                    
            }

            if (_startOnAwake)
                BeginMovement();
        }

        // Update is called once per frame
        void Update()
        {
            if (_activeMovement && Time.time > _idleUntil)
            {
                if (NearTarget())
                    StartNextTask();
                else
                    _moveAxis = (_targets[_targetIndex].position - this.gameObject.transform.position).normalized;
            }

            HandleLocomotion(_moveAxis);
        }

        public void BeginMovement()
        {
            _activeMovement = true;

            if(_onlyOnceSaveId > 0)
            {
                LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();
                levelMgr.SetLevelToggle(_onlyOnceSaveId, true);
            }
        }

        private void StartNextTask()
        {
            _targetIndex++;

            if (_targetIndex == _targets.Count)
                EndMovement();
            else
            {
                if (_idleTimeBetweenMoves > 0)
                    _idleUntil = Time.time + _idleTimeBetweenMoves;

                _moveAxis = Vector2.zero;
            }
        }

        private void EndMovement()
        {
            switch(_moveType)
            {
                case ScriptedMoveTypes.Loop:
                    _targetIndex = -1;
                    StartNextTask();
                    break;
                case ScriptedMoveTypes.MoveThenDestroy:
                    Destroy(this.gameObject);
                    break;
                case ScriptedMoveTypes.MoveThenIdle:
                    _activeMovement = false;
                    break;
            }
        }

        private bool NearTarget()
        {
            if (_targets == null || _targets.Count == 0)
                return true;

            if(Vector3.Distance(_targets[_targetIndex].position, this.gameObject.transform.position) > 1)
                return false;

            return true;
        }

        private void HandleLocomotion(Vector2 moveAxis)
        {
            var maxAcceleration = _acceleration;

            if (moveAxis != Vector2.zero)
            {
                _currentAcceleration =
                    Mathf.MoveTowards(_currentAcceleration, 1, maxAcceleration * Time.deltaTime);
                _lastActiveAxis = moveAxis;
            }
            else
            {
                maxAcceleration /= 2;
                _currentAcceleration = Mathf.MoveTowards(_currentAcceleration, 0, maxAcceleration * Time.deltaTime);
            }

            var targetSpeed = _moveSpeed * _currentAcceleration;

            if (targetSpeed > 0)
            {
                Vector2 movement = _lastActiveAxis * targetSpeed;
                Move(movement);
                _animateCtrl.AnimateMovement(_lastActiveAxis, _moveSpeed, targetSpeed, _lastActiveAxis);
            }
            else
            {
                _animateCtrl.AnimateMovement(_lastActiveAxis, _moveSpeed, targetSpeed, _lastActiveAxis);
            }

            

        }

        private void Move(Vector2 amount)
        {
            Vector2 nextPosition = _rigidbody.position;
            nextPosition += amount * Time.deltaTime;

            _rigidbody.MovePosition(nextPosition);

        }

        //TODO:  pause movement on collision
        private void OnCollisionEnter2D(Collision2D collision)
        {
            _idleUntil = Time.time + 3f;
            _moveAxis = Vector2.zero;

        }
    }
}
