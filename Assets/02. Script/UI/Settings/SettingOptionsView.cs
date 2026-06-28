using System;
using System.Collections.Generic;
using _02._Script.Datas;
using _02._Script.EventParams;
using _02._Script.Settings;
using GameManagements;
using UnityEngine;
using Tmp = TMPro.TextMeshProUGUI;

namespace _02._Script.UI.Settings
{
	public class SettingOptionsView : MonoBehaviour
	{
		[SerializeField] private Tmp categoryText;
		[SerializeField] private Transform contents;
		
		public event Action<object, string> OnSettingValueChanged;
		
		private string _categoryName;

		
		public void Initialize(SettingOptionCategoryData categoryData, SettingOptionUIPrefabs prefabs)
		{
			_categoryName = categoryData.SettingCategoryName;
			categoryText.text = _categoryName;

			foreach (var option in categoryData.Options)
			{
				var prefab = prefabs.GetPrefab(option.GetDataType());
				var instance = Instantiate(prefab, contents);
				instance.Initialize(option);
				instance.LateInitialize();
				instance.OnValueChanged += HandleSettingValueChanged;
			}
		}

		private void HandleSettingValueChanged(object value, string propertyName)
		{
			OnSettingValueChanged?.Invoke(value, propertyName);
			
			
		}

		
	}
}