using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class DestroyAfterEffectsDone : MonoBehaviour
    {
        [SerializeField] float _liveTime;

        private float _deathTime;
        // Start is called before the first frame update
        void Start()
        {
            _deathTime = Time.time + _liveTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (_deathTime < Time.time)
                Destroy(this.gameObject);
        }
    }
}
