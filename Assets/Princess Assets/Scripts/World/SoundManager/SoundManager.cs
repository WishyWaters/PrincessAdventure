using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{

    public class SoundManager : MonoBehaviour
    {


        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _effectsSource;


		public static SoundManager SoundInstance;

		private float sfxVolume = .5f;

		#region Unity Functions
		private void Awake()
		{
			if (SoundInstance == null)
			{
				DontDestroyOnLoad(this);
				SoundInstance = this;
			}
			else if (SoundInstance != this)
			{
				Destroy(gameObject);
			}
		}
        #endregion


        public void PlayEffectSound(AudioClip clip)
        {
			if(clip != null)
				_effectsSource.PlayOneShot(clip);
        }

		public void ChangeMasterVolume(float value)
        {
			AudioListener.volume = value;
        }

		public float GetMasterVolume()
        {
			return sfxVolume;

		}

    }
}
