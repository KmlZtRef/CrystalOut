using UnityEngine;

namespace _02._Script.Player.Controls
{
	public interface ISight
	{
		public void LookHandle(Vector2 delta);
	}
}