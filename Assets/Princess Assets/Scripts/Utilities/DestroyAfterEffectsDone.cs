using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class DestroyAfterEffectsDone : MonoBehaviour
    {
        [SerializeField] float _liveTime;
        [SerializeField] AudioClip _startClip;

        private float _deathTime;
        // Start is called before the first frame update
        void Start()
        {
            _deathTime = Time.time + _liveTime;

            if (_startClip != null)
                SoundManager.SoundInstance.PlayClipAt(_startClip, this.transform.position);
        }

        // Update is called once per frame
        void Update()
        {
            if (_deathTime < Time.time)
                Destroy(this.gameObject);
        }
    }
}
