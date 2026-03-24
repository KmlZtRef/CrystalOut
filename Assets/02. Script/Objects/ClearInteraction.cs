using System;
using _02._Script.Logics;
using _02._Script.Player.Controls;
using Unity.Cinemachine;
using UnityEngine;

namespace _02._Script.Objects
{
	public class ClearInteraction : MonoBehaviour, IInteractable
	{
		[SerializeField] private ClearAreaAnimation anim;
		[SerializeField] private CinemachineCamera clearCam;

		private void Awake()
		{
			anim ??= GetComponent<ClearAreaAnimation>();
		}

		public void Interact(IInteractor interactor)
		{
			if (interactor is IClearable clearable)
			{
				clearable.ClearLevel();
				ClearStage();
				GameManager.Instance.StageClear();
			}
		}

		private void ClearStage()
		{
			anim.ActivateAnimation();
			clearCam.Priority = 5;
		}
	}
}