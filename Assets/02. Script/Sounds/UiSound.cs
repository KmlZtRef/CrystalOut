using System;
using UnityEngine;

namespace _02._Script.Sounds
{
	[RequireComponent(typeof(AudioSource))]
	public class UiSound : MonoBehaviour
	{
		[SerializeField] private AudioClip audioClip;
		
		private AudioSource _audioSource;

		private void Start()
		{
			_audioSource = GetComponent<AudioSource>();
			_audioSource.playOnAwake = false;
			_audioSource.clip = audioClip;
		}

		public void PlaySound()
		{
			_audioSource?.Play();
		}

		public void PlaySoundOneshot()
		{
			_audioSource?.PlayOneShot(audioClip);
		}
	}
}