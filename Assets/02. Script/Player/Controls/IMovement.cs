using UnityEngine;

namespace _02._Script.Player.Controls
{
	public interface IMovement
	{
		public void MoveHandle(Vector2 direction);
		public void JumpHandle();
		public void StopMovement();
	}
}