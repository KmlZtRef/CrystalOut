using System;
using UnityEngine;

namespace _02._Script.Sounds
{
	[RequireComponent(typeof(AudioSource))]
	public class ClearAreaSound : MonoBehaviour
	{
		[SerializeField] private AudioClip audioClip;
		
		private AudioSource _audioSource;

		private void Start()
		{
			_audioSource = GetComponent<AudioSource>();
			_audioSource.clip = audioClip;
			_audioSource.playOnAwake = false;
		}

		public void PlaySound()
		{
			_audioSource?.Play();
		}
	}
}