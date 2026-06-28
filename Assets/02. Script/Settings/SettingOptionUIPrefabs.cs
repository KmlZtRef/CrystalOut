using System;
using System.Linq;
using _02._Script.UI.Settings;
using _02._Script.UI.Settings.SettingOptions;
using UnityEngine;

namespace _02._Script.Settings
{
	[CreateAssetMenu(fileName = "Setting Option UI Prefabs", menuName = "Settings/Setting Option UI Prefabs", order = 0)]
	public class SettingOptionUIPrefabs : ScriptableObject
	{
		[SerializeField] private SettingOption[] optionPrefabs;
		
		public SettingOption GetPrefab(Type valueType) => optionPrefabs.FirstOrDefault(prf => prf.GetValueType() == valueType);
	}
}