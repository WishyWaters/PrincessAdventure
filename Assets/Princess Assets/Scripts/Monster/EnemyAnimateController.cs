using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

namespace PrincessAdventure
{
    public class EnemyAnimateController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private EnemyRigTypes _rigType;
        [SerializeField] private GameObject _deathEffectPrefab;

        [Header("Spine References")]
        [SerializeField] private SkeletonAnimation _spineAnimator;

        [Header("Custom Humanoids References")]
        [SerializeField] private CustomizableCharacters.CustomizableCharacter _customizableCharacter;
        [SerializeField] private Animator _animator;

        [Header("Script References")]
        [SerializeField] private EnemyBehaviorController _behaviorCtrl;
        [SerializeField] private EnemySoundController _sfxCtrl;
        [SerializeField] private EnemyActionController _actionCtrl;

        private readonly Vector2[] _directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        //spine vars
        private bool _allowNewSpineAnimations;
        private string _animationPrefix = "Front_";
        private Vector2 _previousDirection;

        //cust vars
        private Coroutine _currentAnimatorCoroutine;
        private int _animatorDirection;
        private GameObject _currentDirectionGameObject;

        private void Start()
        {
            EnableController(Vector2.zero);
        }

        public void EnableController(Vector2 direction)
        {
            if (_rigType == EnemyRigTypes.CustomHumanoid)
            {
                ResetAnimatorRigs();
                HandleAnimatorDirection(direction);
            }
            else
            {
                _allowNewSpineAnimations = true;
                _spineAnimator.AnimationState.Complete += OnSpineAnimationComplete;

                SetSpineAnimation("Idle", true, direction);
            }
        }


        private void HandleSpineDirection(Vector2 moveAxis)
        {
            var direction = GetClosestDirection(moveAxis);
            //if (direction == _previousDirection)
            //    return;

            if (direction == Vector2.right)
            {
                _spineAnimator.skeleton.SetSkin("Side");
                _spineAnimator.skeleton.SetSlotsToSetupPose();
                this.transform.localScale = new Vector3(1, 1, 1);
                _animationPrefix = "Side_";

            }
            else if (direction == Vector2.left)
            {
                _spineAnimator.skeleton.SetSkin("Side");
                _spineAnimator.skeleton.SetSlotsToSetupPose();
                this.transform.localScale = new Vector3(-1, 1, 1);
                _animationPrefix = "Side_";

            }
            else if (direction == Vector2.up)
            {
                _spineAnimator.skeleton.SetSkin("Back");
                _spineAnimator.skeleton.SetSlotsToSetupPose();
                _animationPrefix = "Back_";

            }
            else if (direction == Vector2.down)
            {
                _spineAnimator.skeleton.SetSkin("Front");
                _spineAnimator.skeleton.SetSlotsToSetupPose();
                _animationPrefix = "Front_";

            }

            _previousDirection = direction;

        }

        private void SetSpineAnimation(string animationSuffix, bool loop, Vector2 direction)
        {
            HandleSpineDirection(direction);
            //Suffixes are: Idle, Walk, Death, Hurt and Attack
            string animationName = _animationPrefix + animationSuffix;

            if (_spineAnimator.AnimationName == animationName)
                return;

            
            if (animationSuffix == "Death")
            {
                _allowNewSpineAnimations = false;
                _spineAnimator.AnimationState.SetAnimation(0, animationName, loop);
            }
            else if (_allowNewSpineAnimations)
            {
                if(animationSuffix == "Attack" || animationSuffix == "Hurt")
                    _allowNewSpineAnimations = false;

                //HandleSpineDirection(direction);
                _spineAnimator.AnimationState.SetAnimation(0, animationName, loop);

            } 

                
            
        }

        private void OnSpineAnimationComplete(TrackEntry trackEntry)
        {
            // Add your implementation code here to react to complete events
            if (_spineAnimator.AnimationName.Contains("Death"))
                Destroy(this.gameObject);
            else if (_spineAnimator.AnimationName.Contains("Attack"))
            {
                _actionCtrl.AttemptIdleAfterAttack();
            }
            else if (_spineAnimator.AnimationName.Contains("Hurt"))
            {
                _actionCtrl.AttemptFleeFromPain();
            }

            _allowNewSpineAnimations = true;


        }

        private Vector2 GetClosestDirection(Vector2 from)
        {
            var maxDot = -Mathf.Infinity;
            var ret = Vector3.zero;

            for (int i = 0; i < _directions.Length; i++)
            {
                var t = Vector3.Dot(from, _directions[i]);
                if (t > maxDot)
                {
                    ret = _directions[i];
                    maxDot = t;
                }
            }

            return ret;
        }

        private void PaintItBlack()
        {
            Color black = Color.black;

            if (_rigType == EnemyRigTypes.CustomHumanoid)
            {
                SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>(true);
                foreach (SpriteRenderer child in children)
                {
                    child.color = black;
                }
            }
            else
            {
                _spineAnimator.skeleton.SetColor(black);
            }



        }

        #region public methods

        public void AnimateMovement(Vector2 moveAxis, float definedSpeed, float targetSpeed, Vector2 direction)
        {
            if (_rigType == EnemyRigTypes.CustomHumanoid)
            {
                HandleAnimatorDirection(moveAxis);
                var animatorSpeed = Mathf.InverseLerp(0, definedSpeed, targetSpeed);
                _animator.SetFloat("Speed", animatorSpeed);
                _animator.SetFloat("Direction", _animatorDirection);
            }
            else
            {
                if (targetSpeed > 0)
                    SetSpineAnimation("Walk", true, direction);
                else
                    SetSpineAnimation("Idle", true, direction);
            }

        }

        public void AnimateHurt(Vector2 direction)
        {
            if (_rigType == EnemyRigTypes.CustomHumanoid)
            {
                if (_currentAnimatorCoroutine != null)
                    StopCoroutine(_currentAnimatorCoroutine);
                _currentAnimatorCoroutine = StartCoroutine(DoAnimatorHurt(direction));
            }
            else
            {
                //Names are: Idle, Walk, Death, Hurt and Attack
                SetSpineAnimation("Hurt", false, direction);
            }


            
        }

        public void AnimateDeath(Vector2 direction)
        {
            Instantiate(_deathEffectPrefab, this.transform.position, this.transform.rotation);

            PaintItBlack();

            if (_rigType == EnemyRigTypes.CustomHumanoid)
            {
                if (_currentAnimatorCoroutine != null)
                    StopCoroutine(_currentAnimatorCoroutine);
                _currentAnimatorCoroutine = StartCoroutine(DoDie(direction));
            }
            else
            {
                //Names are: Idle, Walk, Death, Hurt and Attack
                SetSpineAnimation("Death", false, direction);
            }

        }

        public void AnimateAttack(Vector2 direction)
        {
            if (_rigType == EnemyRigTypes.CustomHumanoid)
            {
                if (_currentAnimatorCoroutine != null)
                    StopCoroutine(_currentAnimatorCoroutine);
                _currentAnimatorCoroutine = StartCoroutine(DoAnimatorAttack(direction));
            }
            else
            {
                SetSpineAnimation("Attack", false, direction);
            }
        }

        #endregion

        #region custom humanoid animation

        private void ResetAnimatorRigs()
        {
            _customizableCharacter.UpRig.SetActive(false);
            _customizableCharacter.SideRig.SetActive(false);
            _customizableCharacter.DownRig.SetActive(false);
        }

        

        private void HandleAnimatorDirection(Vector2 moveAxis)
        {
            var direction = GetClosestDirection(moveAxis);
            //if (direction == _previousDirection)
            //    return;

            _currentDirectionGameObject?.SetActive(false);

            if (direction == Vector2.right)
            {
                _currentDirectionGameObject = _customizableCharacter.SideRig;
                var scale = _customizableCharacter.SideRig.transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                _currentDirectionGameObject.transform.localScale = scale;
                _animatorDirection = 1;
            }
            else if (direction == Vector2.left)
            {
                _currentDirectionGameObject = _customizableCharacter.SideRig;
                var scale = _customizableCharacter.SideRig.transform.localScale;
                scale.x = Mathf.Abs(scale.x) * -1;
                _currentDirectionGameObject.transform.localScale = scale;
                _animatorDirection = 1;
            }
            else if (direction == Vector2.up)
            {
                _currentDirectionGameObject = _customizableCharacter.UpRig;
                _animatorDirection = 0;
            }
            else if (direction == Vector2.down)
            {
                _currentDirectionGameObject = _customizableCharacter.DownRig;
                _animatorDirection = 2;
            }

            _currentDirectionGameObject?.SetActive(true);
            _previousDirection = direction;
        }


        private IEnumerator DoAnimatorAttack(Vector2 direction)
        {

            _animator.SetTrigger("Attack 1");

            HandleAnimatorDirection(direction);

            
            // wait for animator state to change to attack
            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") == false)
                yield return null;

            _animator.ResetTrigger("Attack 1");


            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1")
                       && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {

                yield return null;
            }

            _actionCtrl.AttemptIdleAfterAttack();

        }

        private IEnumerator DoAnimatorHurt(Vector2 direction)
        {
            _animator.SetTrigger("Hurt");

            HandleAnimatorDirection(direction);

            // wait for animator state to change to attack
            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt") == false)
                yield return null;

            _animator.ResetTrigger("Hurt");


            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt")
                       && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {

                yield return null;
            }

            _actionCtrl.AttemptFleeFromPain();
        }


        private IEnumerator DoDie(Vector2 direction)
        {
            _animator.SetTrigger("Die");

            HandleAnimatorDirection(direction);

            // wait for animator state to change to attack
            while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Die") == false)
                yield return null;

            _animator.ResetTrigger("Die");

            float moveTime = 0f;
            while (moveTime < .3f)
            {
                moveTime += Time.deltaTime;

                yield return null;
            }


            Destroy(this.gameObject);
        }

        #endregion


    }
}
