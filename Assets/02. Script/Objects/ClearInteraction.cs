using System;
using _02._Script.Logics;
using _02._Script.Player.Controls;
using _02._Script.Sounds;
using Unity.Cinemachine;
using UnityEngine;

namespace _02._Script.Objects
{
	public class ClearInteraction : MonoBehaviour, IInteractable
	{
		[SerializeField] private ClearAreaAnimation anim;
		[SerializeField] private ClearAreaSound sound;
		[SerializeField] private CinemachineCamera clearCam;
		
		public event Action OnInteracted;
		public event Action OnAnimEnd;

		private void Awake()
		{
			anim ??= GetComponent<ClearAreaAnimation>();
		}

		private void Start()
		{
			anim.OnAnimationEnd += OnAnimationEnd;
		}

		public void Interact(IInteractor interactor)
		{
			if (interactor is IClearable clearable)
			{
				clearable.ClearLevel();
				ClearStage();
				
				OnInteracted?.Invoke();
				// Legacy:
				// GameManager.Instance.StageClear();
			}
		}

		private void ClearStage()
		{
			anim.Activate();
			sound.PlaySound();
			clearCam.Priority = 5;
		}

		private void OnAnimationEnd()
		{
			OnAnimEnd?.Invoke();
		}
	}
}