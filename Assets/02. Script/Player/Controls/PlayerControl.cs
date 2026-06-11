using System;
using _02._Script.EventParams;
using GameManagements;
using UnityEngine;

namespace _02._Script.Player.Controls
{
	public class PlayerControl : ControllableObject
	{
		[SerializeField] private InputSO input;
		[SerializeField] private OuterFluidCam outerCam;

		private ObjectCatchHandler _catch;
		private bool _outerFluid = false;

		protected void Start()
		{
			if (input != null)
			{
				BindEvents(input);
				
				input.OnSwitchPlayerAction += SwitchControl;
			}
		}

		private void SwitchControl() // 나중에 FSM으로 변경 예정
		{
			if (_outerFluid)
			{
				outerCam.UnbindEvents(input);
				BindEvents(input);
				
				outerCam.SetPriority(0);
				
				_outerFluid = false;
			}
			else
			{
				outerCam.BindEvents(input);
				UnbindEvents(input);
				
				outerCam.SetPriority(2);
				
				_outerFluid = true;
			}
			
			MessageBus.Publish(new OnPlayerStateChanged() {PlayerState = _outerFluid});
		}

		public void DisableControl()
		{
			outerCam.UnbindEvents(input);
			UnbindEvents(input);
			input.OnSwitchPlayerAction -= SwitchControl;
			
			gameObject.SetActive(false);
		}
	}
}