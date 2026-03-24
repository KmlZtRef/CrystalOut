using System;

namespace _02._Script.Player.Controls
{
	public interface IInteractor
	{
		public void InteractHandle();
		public event Action<IInteractable> OnInteractAction;
	}
}