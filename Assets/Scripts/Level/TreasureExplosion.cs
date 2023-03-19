using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{


    public class TreasureExplosion : MonoBehaviour
    {

        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private GameObject silverbarPrefab;
        [SerializeField] private GameObject goldBarPrefab;
        [SerializeField] private GameObject redPotionPrefab;
        [SerializeField] private GameObject bluePotionPrefab;

        [SerializeField] private GameObject openObject;
        [SerializeField] private GameObject closeObject;

        [SerializeField] private AudioClip _openClip;
        [SerializeField] private GameObject _openEffect;

        public void ThrowTreasure()
        {
            //5 to 20 coins
            int numOfCoins = UnityEngine.Random.Range(20, 50);
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

            StopInteractions();
        }

        private void CreateTreasureItem(GameObject prefabToMake)
        {
            GameObject newTreasure = Instantiate(prefabToMake, this.transform.position, this.transform.rotation);

            PickupController pickupCtrl = newTreasure.GetComponent<PickupController>();
    
            float x = GetRandomInRange();
            float y = GetRandomInRange();
            pickupCtrl.MoveItemToTarget(newTreasure.transform.position + new Vector3(x, y, 0));
        }


        private float GetRandomInRange()
        {
            float result = UnityEngine.Random.Range(-1.5f, 1.5f);
            if (result >= 0f)
                result += .5f;
            else if (result < 0f)
                result -= .5f;

            return result;
        }

        private void PlayFanFare()
        {
            if (_openClip != null)
                SoundManager.SoundInstance.PlayEffectSound(_openClip);

            GameObject effect = Instantiate(_openEffect, this.transform);

            ParticleSystem effectPSys = effect.GetComponent<ParticleSystem>();

            if (effect != null && effectPSys != null)
                effectPSys.Play();


        }

        private void StopInteractions()
        {
            BoxCollider2D col = this.GetComponent<BoxCollider2D>();
            col.enabled = false;

            openObject.SetActive(true);
            closeObject.SetActive(false);

        }
    }
}