using System;
using System.Collections;
using _02._Script.Datas;
using Unity.Cinemachine;
using UnityEngine;

namespace _02._Script.Objects
{
	public class TransparentDoor : TriggerableMono
	{
		[SerializeField] private ParticleSystem particle;
		[SerializeField] private GameObject doorVisual;
		[SerializeField] private CinemachineCamera cinemachineCamera;
		
		private Collider _collider;

		private bool _useCam;
		
		private void Start()
		{
			InitAndReset();
		}

		public void InitAndReset()
		{
			_collider = GetComponentInChildren<Collider>();
			particle.Stop();
			particle.gameObject.SetActive(false);
			doorVisual.SetActive(true);
			_collider.enabled = true;
			cinemachineCamera.Priority = 0;
		}

		[ContextMenu("Active")]
		public void OpenDoor()
		{
			StartCoroutine(OpenDoorCoroutine());
		}

		private IEnumerator OpenDoorCoroutine()
		{
			if (_useCam) cinemachineCamera.Priority = 3;
			yield return new WaitForSeconds(0.3f);
			particle.gameObject.SetActive(true);
			particle.Play();
			yield return new WaitForSeconds(1.0f);
			doorVisual.SetActive(false);
			_collider.enabled = false;
			yield return new WaitForSeconds(3.0f);
			if (_useCam) cinemachineCamera.Priority = 0;
		}

		public override void Trigger()
		{
			OpenDoor();
		}

		public override void InjectData(SettingDataContext context)
		{
			_useCam = context.moveCamOnObjectActive;
		}
	}
}