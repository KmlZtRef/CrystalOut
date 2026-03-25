using _02._Script.Player.Controls;

namespace _02._Script.Objects
{
	public class InteractableMovingPlatform : MovingPlatform, IInteractable
	{
		public void Interact(IInteractor interactor)
		{
			StartMoving();
		}
	}
}