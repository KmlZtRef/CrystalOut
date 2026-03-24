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

		public virtual void BindEvents(InputSO input)
		{
			input.OnMoveAction += Movement.MoveHandle;
			input.OnJumpAction += Movement.JumpHandle;
			input.OnLookAction += Sight.LookHandle;
			input.OnCatchAction += Interactor.InteractHandle;
			input.OnDropAction += Catch.OnDrop;

			Interactor.OnInteractAction += Catch.OnInteract;
		}

		public virtual void UnbindEvents(InputSO input)
		{
			input.OnMoveAction -= Movement.MoveHandle;
			input.OnJumpAction -= Movement.JumpHandle;
			input.OnLookAction -= Sight.LookHandle;
			input.OnCatchAction -= Interactor.InteractHandle;
			input.OnDropAction -= Catch.OnDrop;
			
			Interactor.OnInteractAction -= Catch.OnInteract;
			Movement.StopMovement();
		}
	}
}