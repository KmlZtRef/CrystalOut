using UnityEngine;

namespace _02._Script.Stage
{
	[CreateAssetMenu(fileName = "Stage Data", menuName = "Stages/Stage Data", order = 0)]
	public class StageDataSo : ScriptableObject
	{
		public string StageName;
		[TextArea] public string StageDescription;
		public GameObject StagePrefab;
	}
}