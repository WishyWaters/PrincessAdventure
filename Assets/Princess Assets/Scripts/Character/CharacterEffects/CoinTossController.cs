using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{ 
    public class CoinTossController : MonoBehaviour
    {
        [Header("Treasure Prefabs")]
        [SerializeField] private GameObject coinPrefab;

        private float _tossMinRadius = .5f;
        private float _tossMaxRadius = 1.5f;

        public void ThrowTreasure(int coinsToToss)
        {
            for (int i = 0; i < coinsToToss; i++)
            {
                CreateTreasureItem(coinPrefab);
            }
        }

        private void CreateTreasureItem(GameObject prefabToMake)
        {
            GameObject newTreasure = Instantiate(prefabToMake, this.transform.position, this.transform.rotation);

            PickupController pickupCtrl = newTreasure.GetComponent<PickupController>();

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
    }
}
