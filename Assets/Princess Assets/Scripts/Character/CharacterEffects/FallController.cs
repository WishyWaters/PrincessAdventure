using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class FallController : MonoBehaviour
    {
        [SerializeField] private GameObject _starPrefab;
        [SerializeField] private GameObject _starCreatorPrefab;
        [SerializeField] private GameObject _fallEffectPrefab;

        [SerializeField] private List<AudioClip> _fallShouts;
        [SerializeField] private AudioClip _fallingStarClip;
        [SerializeField] private AudioClip _starMagicClip;
        [SerializeField] private List<AudioClip> _princessLandClips; //win3,4,5,6

        private bool _finishedFalling = false;
        private GameObject _princess;

        public void StartFallingPrincess(Vector3 spawnPoint, GameObject princess)
        {
            _princess = princess;
            _finishedFalling = false;
            SetPrincessAlpha(0f);

            Instantiate(_fallEffectPrefab, _princess.transform.position, new Quaternion());
            SoundManager.SoundInstance.PlayEffectSound(_fallShouts[Random.Range(0, _fallShouts.Count)]);


            StartCoroutine(HandleFall(spawnPoint));
        }

        public bool IsFinishedFalling()
        {
            return _finishedFalling;
        }



        private IEnumerator HandleFall(Vector3 spawnPoint)
        {
            bool beginFall = false;
            Vector3 starStart = _princess.transform.position;
            Vector3 starDestination = spawnPoint;

            GameObject star = Instantiate(_starPrefab, starStart, new Quaternion());

            float percentOfJourney = 0;
            float timePassed = 0f;

            //Wait a frame
            while (!beginFall)
            {
                beginFall = true;
                yield return null;
            }

            SoundManager.SoundInstance.PlayEffectSound(_fallingStarClip);

            while (percentOfJourney < 1f)
            {
                timePassed += Time.deltaTime;
                percentOfJourney = timePassed / 1f;

                Vector2 nextPosition = Vector3.Lerp(starStart, starDestination, percentOfJourney);

                star.transform.position = nextPosition;
                _princess.transform.position = nextPosition;

                yield return null;
            }

            _finishedFalling = true;

            SoundManager.SoundInstance.PlayEffectSound(_starMagicClip);
            Instantiate(_starCreatorPrefab, starDestination, new Quaternion());

            Destroy(star.gameObject);


        }

        public void EndFall()
        {
            SoundManager.SoundInstance.PlayEffectSound(_princessLandClips[Random.Range(0, _princessLandClips.Count)]);

            SetPrincessAlpha(1f);
            DamageEffectController dmgCtrl = this.GetComponent<DamageEffectController>();
            dmgCtrl.SpawnDamageWithNoSound();
        }


        private void SetPrincessAlpha(float alpha)
        {
            SpriteRenderer[] children = _princess.GetComponentsInChildren<SpriteRenderer>(true);
            Color newColor;
            foreach (SpriteRenderer child in children)
            {
                newColor = child.color;
                newColor.a = alpha;
                child.color = newColor;
            }
        }

    }
}