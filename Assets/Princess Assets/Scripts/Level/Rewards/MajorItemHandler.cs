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
        [SerializeField] private int _treasureId;

        public void HandleTreasure()
        {

            GameManager.GameInstance.PickupMajorTreasure(_treasureType, _treasureId);

            PlayFanFare();

        }


        private void PlayFanFare()
        {
            if (_fanfareClip != null)
                SoundManager.SoundInstance.PlayEffectSound(_fanfareClip);
        }

    }
}