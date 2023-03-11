using UnityEngine;
using System.Collections;


namespace PrincessAdventure
{
    public class CliffJump : MonoBehaviour
    {

        [SerializeField] private Vector2 _fallDirection;

        private float _waitTime = .5f;
        private float _targetTime = 0f;
        private CharacterController _charCtrl;



        void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("Enter");
            if(_charCtrl == null)
                _charCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

            _targetTime = Time.time + _waitTime;

        }

        void OnTriggerExit2D(Collider2D col)
        {
            Debug.Log("Exit");
            _targetTime = 0f;
        }

        void OnTriggerStay2D(Collider2D col)
        {
            //Debug.Log("Stay");
            if (_targetTime != 0f && _targetTime <= Time.time)
            {
                _charCtrl.AttemptCliffJump(_fallDirection);
                _targetTime = 0f;
            }
                
        }

    }
}