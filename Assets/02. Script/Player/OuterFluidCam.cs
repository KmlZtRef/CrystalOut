using _02._Script.Player.Controls;
using Unity.Cinemachine;

namespace _02._Script.Player
{
	public class OuterFluidCam : ControllableObject
	{
		private CinemachineCamera _camera;

		private void Start()
		{
			_camera = GetComponentInChildren<CinemachineCamera>();
		}

		public void SetPriority(int priority)
		{
			_camera.Priority = priority;
		}
	}
}
