using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _02._Script.Player.Controls
{
	[CreateAssetMenu(fileName = "InputSO", menuName = "Scriptable Objects/InputSO")]
	public class InputSO : ScriptableObject, InputSystem_Actions.IPlayerActions
	{
		public event Action<Vector2> OnMoveAction;
		public event Action<Vector2> OnLookAction;
		public event Action OnJumpAction;
		public event Action OnSwitchPlayerAction;
		public event Action OnCatchAction;
		public event Action OnDropAction;
		public event Action<float> OnRotateAction;
        
		private InputSystem_Actions _input;
	
		private void OnEnable()
		{
			if (_input == null)
			{
				_input = new InputSystem_Actions();
				_input.Player.SetCallbacks(this);
			}
			_input.Player.Enable();
		}

		private void OnDisable()
		{
			_input.Player.Disable();
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			OnMoveAction?.Invoke(context.ReadValue<Vector2>());
		}

		public void OnLook(InputAction.CallbackContext context)
		{
			OnLookAction?.Invoke(context.ReadValue<Vector2>());
		}

		public void OnJump(InputAction.CallbackContext context)
		{
			if (context.performed)
				OnJumpAction?.Invoke();
		}

		public void OnSwitchPlayer(InputAction.CallbackContext context)
		{
			if (context.canceled)
				OnSwitchPlayerAction?.Invoke();
		}

		public void OnCatch(InputAction.CallbackContext context)
		{
			if (context.performed)
				OnCatchAction?.Invoke();
		}

		public void OnDrop(InputAction.CallbackContext context)
		{
			if (context.performed)
				OnDropAction?.Invoke();
		}

		public void OnRotate(InputAction.CallbackContext context)
		{
			Vector2 value =  context.ReadValue<Vector2>();
			float scrollDelta = value.y;
			OnRotateAction?.Invoke(scrollDelta);
		}
	}
}
 