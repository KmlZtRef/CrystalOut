using System;
using UnityEngine;

namespace _02._Script.Sounds
{
	[RequireComponent(typeof(AudioSource))]
	public class CollisionSoundPlayer : MonoBehaviour
	{
		[SerializeField] private AudioClip audioClip;
		[SerializeField] private float minForce;
		[SerializeField] private float minPitch = -1f;
		[SerializeField] private float maxPitch = 1f;
		[SerializeField] private float minVolume = 0f;
		[SerializeField] private float maxVolume = 1f;
		
		private AudioSource _audioSource;

		private void Start()
		{
			_audioSource = GetComponent<AudioSource>();
			_audioSource.clip = audioClip;
		}

		private void OnCollisionEnter(Collision other)
		{
			float force = other.impulse.magnitude;

			if (force > minForce)
			{
				float volume = (-1 / (force + 1 / (maxVolume-minVolume))) + maxVolume;
				float pitch = (1 / (force + 1 / (maxPitch-minPitch)) + minPitch);
				_audioSource.volume = volume;
				_audioSource.pitch = pitch;
				_audioSource.Play();
			}
		}
	}
}