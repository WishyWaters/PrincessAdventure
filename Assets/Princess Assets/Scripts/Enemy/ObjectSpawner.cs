using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class ObjectSpawner : MonoBehaviour
    {

        [SerializeField] private List<GameObject> _objectsToCreatePrefab;
        [SerializeField] private List<GameObject> _objectsToActivate;
        [SerializeField] private float _delay;

        public void StartSpawning()
        {
            StartCoroutine(SpawnThings());
        }

        private IEnumerator SpawnThings()
        {
            yield return new WaitForSeconds(_delay);


            foreach (GameObject go in _objectsToCreatePrefab)
            {
                Instantiate(go, this.transform.position, this.transform.rotation);
            }

            foreach (GameObject go in _objectsToActivate)
            {
                go.SetActive(true);
            }
        }
    }
}
