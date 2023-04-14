using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class MajorItemHandler : MonoBehaviour
    {


        [Header("Settings")]
        [SerializeField] private MajorTreasures _treasureType;
        [SerializeField] private AudioClip _fanfareClip;


        public void HandleTreasure()
        {

            GameManager.GameInstance.PickupMajorTreasure(_treasureType, 0);

            PlayFanFare();

        }


        private void PlayFanFare()
        {
            if (_fanfareClip != null)
                SoundManager.SoundInstance.PlayEffectSound(_fanfareClip);
        }

    }
}