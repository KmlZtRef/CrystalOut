using System;
using _02._Script.EventParams;
using GameManagements;
using UnityEngine;

namespace _02._Script.Effects
{
	public class PlayerBodyParticle : ParticleEffect
	{
		protected override void Start()
		{
			base.Start();
			Stop();
			
			MessageBus.Subscribe<OnPlayerStateChanged>(HandlePlayerStateChanged);
		}

		private void HandlePlayerStateChanged(OnPlayerStateChanged param)
		{
			if (param.PlayerState)
				Play();
			else
				Stop();
		}

		private void OnDestroy()
		{
			MessageBus.Unsubscribe<OnPlayerStateChanged>(HandlePlayerStateChanged);
		}
	}
}