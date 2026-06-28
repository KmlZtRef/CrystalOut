using UnityEngine;

namespace _02._Script.Options
{
	[CreateAssetMenu(fileName = "Option Data So", menuName = "Options/Option Data", order = 0)]
	public class OptionData : ScriptableObject
	{
		public string optionName;
		public string parentName;
		public OptionNameId[] datas;
	}
}