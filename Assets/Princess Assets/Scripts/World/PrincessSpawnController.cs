using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class PrincessSpawnController : MonoBehaviour
    {
        [SerializeField] private GameObject _starPrefab;
        [SerializeField] private GameObject _starExposionPrefab;
        [SerializeField] private AudioClip _fallingStarClip;
        [SerializeField] private AudioClip _starMagicClip;
        [SerializeField] private AudioClip _starExplosionClip;
        [SerializeField] private List<AudioClip> _princessLandClips; //win3,4,5,6

        [SerializeField] private List<AudioClip> _princessDeathClips;
        [SerializeField] private GameObject _deathExposionPrefab;
        [SerializeField] private AudioClip _risingStarClip; //shine light 5


        public void StartPrincessSpawn(Vector3 spawnPoint)
        {
            StartCoroutine(HandleStarFall(spawnPoint));
        }

        private IEnumerator HandleStarFall(Vector3 spawnPoint)
        {
            bool beginFall = false;
            Vector3 starStart = spawnPoint + new Vector3(0, 8);
            Vector3 starDestination = spawnPoint;

            GameObject star = Instantiate(_starPrefab, starStart, new Quaternion());

            float percentOfJourney = 0;
            float timePassed = 0f;

            //Wait a frame
            while(!beginFall)
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
                yield return null;
            }

            SoundManager.SoundInstance.PlayEffectSound(_starMagicClip);
            Instantiate(_starExposionPrefab, starDestination, new Quaternion());
            SoundManager.SoundInstance.PlayEffectSound(_starExplosionClip);

            Destroy(star.gameObject);
            GameManager.GameInstance.LoadPlayer(starDestination, Vector2.down);

            SoundManager.SoundInstance.PlayEffectSound(_princessLandClips[Random.Range(0, _princessLandClips.Count)]);

            GameManager.GameInstance.ResumeGameplay();
        }

        public void StartPrincessDeath(Vector3 deathPosition)
        {
            StartCoroutine(HandleDeathEffects(deathPosition));
        }

        private IEnumerator HandleDeathEffects(Vector3 deathPosition)
        {
            Vector3 starStart = deathPosition;
            Vector3 starDestination = deathPosition + new Vector3(0, 8);

            SoundManager.SoundInstance.PlayEffectSound(_princessDeathClips[Random.Range(0, _princessDeathClips.Count)]);

            SoundManager.SoundInstance.PlayEffectSound(_starMagicClip);
            Instantiate(_deathExposionPrefab, deathPosition, new Quaternion());

            GameObject star = Instantiate(_starPrefab, starStart, new Quaternion());

            float percentOfJourney = 0;
            float timePassed = 0f;

            //Wait for smoke to clear
            while (timePassed < .5f)
            {
                timePassed += Time.deltaTime;
                yield return null;
            }

            timePassed = 0f;
            SoundManager.SoundInstance.PlayEffectSound(_fallingStarClip);
            while (percentOfJourney < 1f)
            {
                timePassed += Time.deltaTime;
                percentOfJourney = timePassed / 2f;

                Vector2 nextPosition = Vector3.Lerp(starStart, starDestination, percentOfJourney);

                star.transform.position = nextPosition;
                yield return null;
            }


            Destroy(star.gameObject);
            GameManager.GameInstance.LoadDefeatGui();


        }

    }
}
