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

        private bool _allowNewSpineAnimations;
        private string _animationPrefix = "Front_";
        private Vector2 _previousDirection;

        private void Start()
        {
            EnableController(Vector2.zero);
        }

        public void EnableController(Vector2 direction)
        {

            _allowNewSpineAnimations = true;
            _spineAnimator.AnimationState.Complete += OnSpineAnimationComplete;

            SetSpineAnimation("Idle", true, direction);
          
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

        public void AnimateMovement(Vector2 moveAxis, float speed, Vector2 direction)
        {
            
            if (speed > 0)
            {
                SetSpineAnimation("Walk", true, direction);
            }
            else
            {
                SetSpineAnimation("Idle", true, direction);
            }

        }

        public void AnimateHurt(Vector2 direction)
        {
            //if (_rigType == EnemyRigTypes.CustomHumanoid)
            //{
            //    if (_currentCoroutine != null)
            //        StopCoroutine(_currentCoroutine);
            //    _currentCoroutine = StartCoroutine(DoAnimatorHurt(direction));
            //}
            //else
            //{
            //    //Names are: Idle, Walk, Death, Hurt and Attack
            //    HandleSpineDirection(direction);
            //    SetSpineAnimation("Hurt", false);
            //}

            
            SetSpineAnimation("Hurt", false, direction);
        }

        public void AnimateDeath(Vector2 direction)
        {
            Instantiate(_deathEffectPrefab, this.transform.position, this.transform.rotation);

            PaintItBlack();

            //if (_rigType == EnemyRigTypes.CustomHumanoid)
            //{
            //    if (_currentCoroutine != null)
            //        StopCoroutine(_currentCoroutine);
            //    _currentCoroutine = StartCoroutine(DoDie(_currentDirection));
            //}
            //else
            //{
            //    //Names are: Idle, Walk, Death, Hurt and Attack
            //    HandleSpineDirection(direction);

            //    SetSpineAnimation("Death", false);
            //}


            SetSpineAnimation("Death", false, direction);
        }

        public void AnimateAttack(Vector2 direction)
        {
            //if (_rigType == EnemyRigTypes.CustomHumanoid)
            //{
            //    if (_currentCoroutine != null)
            //        StopCoroutine(_currentCoroutine);
            //    _currentCoroutine = StartCoroutine(DoAnimatorAttack(direction));
            //}
            //else
            //{
            //    HandleSpineDirection(direction);
            //    SetSpineAnimation("Attack", false);
            //}

            
            SetSpineAnimation("Attack", false, direction);

        }

        #endregion


    }
}
