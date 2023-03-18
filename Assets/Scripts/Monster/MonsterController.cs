using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Spine;
using Spine.Unity;

namespace PrincessAdventure
{

    public class MonsterController : MonoBehaviour
    {
        [SerializeField] SkeletonAnimation _monsterAnimator;
        [SerializeField] bool _dealHeartDamage;
        Rigidbody2D _rigidbody;

        private Vector2 _currentDirection;

        // Use this for initialization
        void Start()
        {
            _rigidbody = this.GetComponent<Rigidbody2D>();
            _currentDirection = Vector2.right;
            Walk();
        }

        // Update is called once per frame
        void Update()
        {

            MoveHorizontal();
        }


        private void MoveHorizontal()
        {
            Vector2 nextPosition = _rigidbody.position;
            nextPosition += _currentDirection * 2 * Time.deltaTime;

            _rigidbody.MovePosition(nextPosition);

        }

        private void Walk()
        {
            _monsterAnimator.skeleton.SetSkin("Side");
            _monsterAnimator.skeleton.SetSlotsToSetupPose();
            _monsterAnimator.AnimationState.SetAnimation(0, "Side_Walk", true);

        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            ChangeDirection();

            if(_dealHeartDamage && collision.gameObject.tag == "Player")
            {
                GameManager.GameInstance.DamagePrincess();
            }
        }

        private void ChangeDirection()
        {
            //reverse direction on collision
            if (_currentDirection == Vector2.right)
            {
                _currentDirection = Vector2.left;
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                _currentDirection = Vector2.right;
                this.transform.localScale = new Vector3(1, 1, 1);
            }
        }


    }

}
