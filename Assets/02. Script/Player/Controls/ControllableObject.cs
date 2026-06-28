using _02._Script.EventParams;
using GameManagements;
using UnityEngine;

namespace _02._Script.Player.Controls
{
	public abstract class ControllableObject : MonoBehaviour
	{
		public IMovement Movement { get; protected set; }
		public ISight Sight { get; protected set; }
		public IInteractor Interactor { get; protected set; }
		public ICatchHandler Catch { get; protected set; }
		
		/// <summary>
		/// Initialize Components in Here.
		/// </summary>
		protected virtual void Awake()
		{
			Movement = GetComponent<IMovement>();
			Sight = GetComponent<ISight>();
			Interactor = GetComponent<IInteractor>();
			Catch = GetComponent<ICatchHandler>();
		}

		public virtual void BindEvents(InputSo input)
		{
			input.OnMoveAction += Movement.MoveHandle;
			input.OnJumpAction += Movement.JumpHandle;
			input.OnLookAction += Sight.LookHandle;
			input.OnCatchAction += Interactor.InteractHandle;
			input.OnDropAction += Catch.OnDrop;
			input.OnRotateAction += Catch.OnRotate;

			input.OnPauseAction += HandlePauseGame;

			Interactor.OnInteractAction += Catch.OnInteract;
		}

		public virtual void UnbindEvents(InputSo input)
		{
			input.OnMoveAction -= Movement.MoveHandle;
			input.OnJumpAction -= Movement.JumpHandle;
			input.OnLookAction -= Sight.LookHandle;
			input.OnCatchAction -= Interactor.InteractHandle;
			input.OnDropAction -= Catch.OnDrop;
			input.OnRotateAction -= Catch.OnRotate;
			
			input.OnPauseAction -= HandlePauseGame;
			
			Interactor.OnInteractAction -= Catch.OnInteract;
			Movement.StopMovement();
		}

		private void HandlePauseGame()
		{
			MessageBus.Publish(new OnGamePaused());
		}
	}
}