using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{


    public class TreasureExplosion : MonoBehaviour
    {

        [Header("Treasure Prefabs")]
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private GameObject silverbarPrefab;
        [SerializeField] private GameObject goldBarPrefab;
        [SerializeField] private GameObject redPotionPrefab;
        [SerializeField] private GameObject bluePotionPrefab;

        [Header("Settings")]
        [SerializeField] private TreasureDrops _treasureType;
        [SerializeField] private AudioClip _fanfareClip;
        [SerializeField] private GameObject _fanfareEffect;
        [SerializeField] private float _tossRadius;

        public void ThrowTreasure()
        {

            //5 to 20 coins
            int numOfCoins = UnityEngine.Random.Range(5, 25);
            //0 to 2 silver
            int numOfSilver = UnityEngine.Random.Range(0, 3);
            //0 to 1 gold bar/red p/ blue p
            int numOfGold = UnityEngine.Random.Range(0, 2);
            int numOfRedPotion = UnityEngine.Random.Range(0, 3);
            int numOfBluePotion = UnityEngine.Random.Range(0, 3);
            if (GameManager.GameInstance.GetCurrentHealth() <= 2)
                numOfRedPotion++;

            for(int i = 0; i < numOfCoins; i++)
            {
                CreateTreasureItem(coinPrefab);
                if(i < numOfSilver)
                    CreateTreasureItem(silverbarPrefab);
                if (i < numOfGold)
                    CreateTreasureItem(goldBarPrefab);
                if (i < numOfRedPotion)
                    CreateTreasureItem(redPotionPrefab);
                if (i < numOfBluePotion)
                    CreateTreasureItem(bluePotionPrefab);
            }

            PlayFanFare();

        }

        private void CreateTreasureItem(GameObject prefabToMake)
        {
            GameObject newTreasure = Instantiate(prefabToMake, this.transform.position, this.transform.rotation);

            PickupController pickupCtrl = newTreasure.GetComponent<PickupController>();

            //float x = GetRandomInRange();
            //float y = GetRandomInRange();
            float randomRadius = UnityEngine.Random.Range(.8f, _tossRadius);
            Vector3 rndPositionFromCircle = RandomCircle(newTreasure.transform.position, randomRadius);

            pickupCtrl.MoveItemToTarget(rndPositionFromCircle);
        }

        Vector3 RandomCircle(Vector3 center, float radius)
        {
            float ang = Random.value * 360;
            Vector3 pos;
            pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            pos.z = center.z;
            return pos;
        }

        private void PlayFanFare()
        {
            if (_fanfareClip != null)
                SoundManager.SoundInstance.PlayEffectSound(_fanfareClip);

            if (_fanfareEffect != null)
            {
                GameObject effect = Instantiate(_fanfareEffect, this.transform);

                ParticleSystem effectPSys = effect.GetComponent<ParticleSystem>();

                if (effect != null && effectPSys != null)
                    effectPSys.Play();
            }


        }

    }
}