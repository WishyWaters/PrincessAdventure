using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class PlaySoundOnStart : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;

        // Start is called before the first frame update
        void Start()
        {
            SoundManager.SoundInstance.PlayEffectSound(_clip);
        }


    }
}
