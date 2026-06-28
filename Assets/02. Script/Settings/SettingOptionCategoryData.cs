using System.Collections.Generic;
using UnityEngine;

namespace _02._Script.Settings
{
	[CreateAssetMenu(fileName = "Setting Options Data", menuName = "Settings/Setting Option Category Data", order = 0)]
	public class SettingOptionCategoryData : ScriptableObject
	{
		[field: SerializeField] public string SettingCategoryName { get; private set; }
		
		[field: SerializeField] public List<SettingOptionData> Options { get; private set; }
		
		[field: SerializeField] public Sprite CategoryIcon { get; private set; }
	}
}