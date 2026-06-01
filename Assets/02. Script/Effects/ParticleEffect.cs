using System;
using UnityEngine;

namespace _02._Script.Effects
{
	[RequireComponent(typeof(ParticleSystem))]
	public class ParticleEffect : PlayableEffect
	{
		public ParticleSystem Particle { get; private set; }
		private void Start()
		{
			Particle = GetComponent<ParticleSystem>();
		}

		public override void Play()
		{
			Particle.Play();
		}

		public override void Pause()
		{
			Particle.Pause();
		}

		public override void Stop()
		{
			Particle.Stop();
		}
	}
}