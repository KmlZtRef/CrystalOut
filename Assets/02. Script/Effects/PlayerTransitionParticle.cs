using System;
using System.Collections;
using _02._Script.EventParams;
using GameManagements;
using UnityEngine;

namespace _02._Script.Effects
{
	public class PlayerTransitionParticle : ParticleEffect
	{
		[SerializeField] private float _delay;
		
		// 혹시 모를 불상사 방지
		private Coroutine _delayedPlayParticleCoroutine;
		private WaitForSeconds _wait;
		
		protected override void Start()
		{
			base.Start();
			
			MessageBus.Subscribe<OnPlayerStateChanged>(HandlePlayerStateChanged);
			
			_wait = new WaitForSeconds(_delay);
		}

		private void HandlePlayerStateChanged(OnPlayerStateChanged param)
		{
			if (!param.PlayerState) // 유체이탈 -> 플레이어 상태 전환
			{
				if (_delayedPlayParticleCoroutine != null)
					StopCoroutine(_delayedPlayParticleCoroutine);
				
				StartCoroutine(DelayedPlayParticle());
			}
		}

		private IEnumerator DelayedPlayParticle()
		{
			Stop();
			yield return _wait;
			Play();
		}

		private void OnDestroy()
		{
			MessageBus.Unsubscribe<OnPlayerStateChanged>(HandlePlayerStateChanged);
		}
	}
}