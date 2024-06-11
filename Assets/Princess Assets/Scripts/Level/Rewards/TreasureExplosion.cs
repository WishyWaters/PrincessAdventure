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
        [SerializeField] private GameObject greenPotionPrefab;
        [SerializeField] private GameObject keyPrefab;

        [Header("Settings")]
        [SerializeField] private TreasureDrops _treasureType;
        [SerializeField] private float _tossMinRadius;
        [SerializeField] private float _tossMaxRadius;
        //[SerializeField] private bool _includeKey;

        [Header("Optional Effects")]
        [SerializeField] private AudioClip _fanfareClip;
        [SerializeField] private GameObject _fanfareEffect;

        public void ThrowTreasure()
        {

            int numOfCoins = NumberOfCoins();

            int numOfSilver = NumberOfSilverBars();

            int numOfGold = NumberOfGoldBars();

            int numOfRedPotion = NumberOfRedPotions();
            int numOfBluePotion = NumberOfBluePotions();
            int numOfGreenPotion = NumberOfGreenPotions();


            if (GameManager.GameInstance.GetCurrentHealth() <= 2)
                numOfRedPotion++;

            for(int i = 0; i < numOfCoins; i++)
            {
                if(i < numOfCoins)
                    CreateTreasureItem(coinPrefab);
                if(i < numOfSilver)
                    CreateTreasureItem(silverbarPrefab);
                if (i < numOfGold)
                    CreateTreasureItem(goldBarPrefab);
                if (i < numOfRedPotion)
                    CreateTreasureItem(redPotionPrefab);
                if (i < numOfBluePotion)
                    CreateTreasureItem(bluePotionPrefab);
                if (i < numOfGreenPotion)
                    CreateTreasureItem(greenPotionPrefab);
            }
            //if (_includeKey)
            //    CreateTreasureItem(keyPrefab);

            PlayFanFare();

        }

        private int NumberOfCoins()
        {
            int numOfCoins = 0;
            switch(_treasureType)
            {
                case TreasureDrops.MinCoins:
                    numOfCoins = UnityEngine.Random.Range(1, 7);
                    break;
                case TreasureDrops.MedCoins:
                    numOfCoins = UnityEngine.Random.Range(6, 11);
                    break;
                case TreasureDrops.LotCoins:
                    numOfCoins = UnityEngine.Random.Range(11, 21);
                    break;
                case TreasureDrops.MinAll:
                    numOfCoins = UnityEngine.Random.Range(6, 11);
                    break;
                case TreasureDrops.MedAll:
                    numOfCoins = UnityEngine.Random.Range(11, 21);
                    break;
                case TreasureDrops.LotAll:
                case TreasureDrops.Horde:
                    numOfCoins = UnityEngine.Random.Range(21, 31);
                    break;
                case TreasureDrops.SinglePotion:
                case TreasureDrops.ManyPotions:
                case TreasureDrops.PoisonPotion:
                    numOfCoins = UnityEngine.Random.Range(1, 7);
                    break;

            }
            return numOfCoins;
        }

        private int NumberOfSilverBars()
        {
            int numOf = 0;
            switch (_treasureType)
            {
                case TreasureDrops.MinCoins:
                case TreasureDrops.MedCoins:
                case TreasureDrops.LotCoins:
                    numOf = 0;
                    break;
                case TreasureDrops.MinAll:
                    numOf = UnityEngine.Random.Range(0, 1);
                    break;
                case TreasureDrops.MedAll:
                    numOf = UnityEngine.Random.Range(1, 2);
                    break;
                case TreasureDrops.LotAll:
                    numOf = UnityEngine.Random.Range(2, 3);
                    break;
                case TreasureDrops.Horde:
                    numOf = UnityEngine.Random.Range(3, 5);
                    break;
                case TreasureDrops.SinglePotion:
                case TreasureDrops.ManyPotions:
                case TreasureDrops.PoisonPotion:
                    numOf = 0;
                    break;

            }
            return numOf;
        }

        private int NumberOfGoldBars()
        {
            int numOf = 0;
            switch (_treasureType)
            {
                case TreasureDrops.MinCoins:
                case TreasureDrops.MedCoins:
                case TreasureDrops.LotCoins:
                case TreasureDrops.MinAll:
                    numOf = 0;
                    break;
                case TreasureDrops.MedAll:
                    numOf = UnityEngine.Random.Range(0, 1);
                    break;
                case TreasureDrops.LotAll:
                    numOf = UnityEngine.Random.Range(1, 2);
                    break;
                case TreasureDrops.Horde:
                    numOf = UnityEngine.Random.Range(2, 3);
                    break;
                case TreasureDrops.SinglePotion:
                case TreasureDrops.ManyPotions:
                case TreasureDrops.PoisonPotion:
                    numOf = 0;
                    break;

            }
            return numOf;
        }

        private int NumberOfRedPotions()
        {
            int numOf = 0;
            switch (_treasureType)
            {
                case TreasureDrops.MinCoins:
                case TreasureDrops.MedCoins:
                case TreasureDrops.LotCoins:
                    numOf = 0;
                    break;
                case TreasureDrops.MinAll:
                    numOf = UnityEngine.Random.Range(0, 1);
                    break;
                case TreasureDrops.MedAll:
                    numOf = UnityEngine.Random.Range(1, 2);
                    break;
                case TreasureDrops.LotAll:
                    numOf = UnityEngine.Random.Range(1, 3);
                    break;
                case TreasureDrops.Horde:
                    numOf = UnityEngine.Random.Range(2, 5);
                    break;
                case TreasureDrops.SinglePotion:
                    numOf = 1;
                    break;
                case TreasureDrops.ManyPotions:
                    numOf = UnityEngine.Random.Range(2, 5);
                    break;
                case TreasureDrops.PoisonPotion:
                    numOf = 0;
                    break;

            }
            return numOf;
        }

        private int NumberOfBluePotions()
        {
            int numOf = 0;
            switch (_treasureType)
            {
                case TreasureDrops.MinCoins:
                case TreasureDrops.MedCoins:
                case TreasureDrops.LotCoins:
                    numOf = 0;
                    break;
                case TreasureDrops.MinAll:
                    numOf = UnityEngine.Random.Range(0, 2);
                    break;
                case TreasureDrops.MedAll:
                    numOf = UnityEngine.Random.Range(1, 3);
                    break;
                case TreasureDrops.LotAll:
                    numOf = UnityEngine.Random.Range(1, 4);
                    break;
                case TreasureDrops.Horde:
                    numOf = UnityEngine.Random.Range(2, 5);
                    break;
                case TreasureDrops.SinglePotion:
                    numOf = UnityEngine.Random.Range(0, 2);
                    break;
                case TreasureDrops.ManyPotions:
                    numOf = UnityEngine.Random.Range(2, 6);
                    break;
                case TreasureDrops.PoisonPotion:
                    numOf = 0;
                    break;

            }
            return numOf;
        }

        private int NumberOfGreenPotions()
        {
            int numOf = 0;
            switch (_treasureType)
            {
                case TreasureDrops.MinCoins:
                case TreasureDrops.MedCoins:
                case TreasureDrops.LotCoins:
                case TreasureDrops.MinAll:
                case TreasureDrops.MedAll:
                case TreasureDrops.LotAll:
                case TreasureDrops.Horde:
                case TreasureDrops.SinglePotion:
                    numOf = 0;
                    break;
                case TreasureDrops.ManyPotions:
                    numOf = UnityEngine.Random.Range(0, 2);
                    break;
                case TreasureDrops.PoisonPotion:
                    numOf = UnityEngine.Random.Range(1, 2);
                    break;

            }
            return numOf;
        }

        private int NumberOfKeys()
        {
            int numOf = 0;
            switch (_treasureType)
            {
                case TreasureDrops.MinCoins:
                case TreasureDrops.MedCoins:
                case TreasureDrops.LotCoins:
                    numOf = 0;
                    break;
                case TreasureDrops.MinAll:
                case TreasureDrops.MedAll:
                    numOf = 1;
                    break;
                case TreasureDrops.LotAll:
                    numOf = 2;
                    break;
                case TreasureDrops.Horde:
                    numOf = 3;
                    break;
                case TreasureDrops.SinglePotion:
                case TreasureDrops.ManyPotions:
                case TreasureDrops.PoisonPotion:
                    numOf = 0;
                    break;

            }
            return numOf;
        }


        private void CreateTreasureItem(GameObject prefabToMake)
        {
            GameObject newTreasure = Instantiate(prefabToMake, this.transform.position, this.transform.rotation);

            PickupController pickupCtrl = newTreasure.GetComponent<PickupController>();

            //float x = GetRandomInRange();
            //float y = GetRandomInRange();
            float randomRadius = UnityEngine.Random.Range(_tossMinRadius, _tossMaxRadius);
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