using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{

    public class DestructibleController : MonoBehaviour
    {
        [SerializeField] private GameObject _destroyedPrefab;

        public void RemoveDestructable()
        {

            Instantiate(_destroyedPrefab, this.transform.position, this.transform.rotation);

            Destroy(this.gameObject);

        }
    }
}
