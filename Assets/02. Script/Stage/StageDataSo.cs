using UnityEngine;

namespace _02._Script.Stage
{
	[CreateAssetMenu(fileName = "Stage Data", menuName = "Stages/Stage Data", order = 0)]
	public class StageDataSo : ScriptableObject
	{
		[field: SerializeField] public string StageName { get; private set; }
		[field: SerializeField] [field: TextArea] public string StageDescription { get; private set; }
		[field: SerializeField] public string StageSceneName { get; private set; }
	}
}