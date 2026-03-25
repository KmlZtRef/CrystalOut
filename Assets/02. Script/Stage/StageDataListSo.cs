using UnityEngine;

namespace _02._Script.Stage
{
	[CreateAssetMenu(fileName = "Stage Data List", menuName = "Stages/Stage Data List", order = 0)]
	public class StageDataListSo : ScriptableObject
	{
		public StageDataSo[] list;
	}
}