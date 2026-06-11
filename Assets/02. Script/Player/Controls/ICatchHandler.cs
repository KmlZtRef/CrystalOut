namespace _02._Script.Player.Controls
{
	public interface ICatchHandler
	{
		public void OnInteract(IInteractable interactable);
		public void OnDrop();
		public void OnRotate(float amount);
	}
}