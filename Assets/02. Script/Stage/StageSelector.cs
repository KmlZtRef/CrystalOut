using UnityEngine;

namespace _02._Script.Stage
{
	public class StageSelector : MonoBehaviour
	{
		public void Test()
		{
			StageManager.Instance.LoadStage();
		}
	}
}