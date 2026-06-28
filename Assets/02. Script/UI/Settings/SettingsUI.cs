using System.Collections.Generic;
using _02._Script.Datas;
using _02._Script.EventParams;
using _02._Script.Settings;
using GameManagements;
using UnityEngine;
using UnityEngine.UI;

namespace _02._Script.UI.Settings
{
	public class SettingsUI : OpenablePanelUI
	{
		[SerializeField] private SettingOptionCategoryData[] settingCategories;
		[SerializeField] private SettingOptionsView settingOptionsViewPrf;
		[SerializeField] private string onCloseCategoryName;
		[SerializeField] private Button closeButton;
		[SerializeField] private Transform categoryContainer;
		[SerializeField] private Transform categoryButtonContainer;
		[SerializeField] private SettingOptionUIPrefabs prefabList;
		[SerializeField] private SettingCategorySelectButton categorySelectButtonPrf;
		
		private List<SettingOptionsView> _categoryViews = new List<SettingOptionsView>();
		private readonly Dictionary<string, SettingDataInjector> _injectorCache = new();
		private int _selectedCategory = 0;
		
		public override void Initialize()
		{
			closeButton.onClick.AddListener(OnCloseButtonClick);

			int i = 0;
			foreach (var category in settingCategories)
			{
				// Generate Category Contents
				var categoryView = Instantiate(settingOptionsViewPrf, categoryContainer);
				categoryView.Initialize(category, prefabList);
				categoryView.gameObject.SetActive(false);
				categoryView.OnSettingValueChanged += HandleSettingValueChanged;
				_categoryViews.Add(categoryView);
				
				// Generate Category Select Buttons
				var btn = Instantiate(categorySelectButtonPrf,  categoryButtonContainer);
				btn.Init();
				btn.SetIcon(category.CategoryIcon);
				btn.Index = i;
				btn.OnButtonClicked += SetCategory;
				i++;
			}
			
			RenderCategory();

			if (DataCenter.Modified)
			{
				MessageBus.Publish(new RequestForcedLoadValue());
			}
			else
			{
				MessageBus.Publish(new RequestSetToInjector());
				SaveSettings();
			}
		}

		private void OnCloseButtonClick()
		{
			SaveSettings();
			
			ClosePanel();
			
			if (string.IsNullOrEmpty(onCloseCategoryName))
				return;
			
			MessageBus.Publish(new RequestChangeCategory() {CategoryName = onCloseCategoryName});
		}

		private void HandleSettingValueChanged(object value, string propertyName)
		{
			SettingDataInjector injector = FindInjector(propertyName);
			injector.SetValue(value);
		}
		
		private SettingDataInjector FindInjector(string propertyName)
		{
			if (_injectorCache.TryGetValue(propertyName, out var findInjector))
			{
				return findInjector;
			}

			var injector = new SettingDataInjector(propertyName);
			_injectorCache.Add(propertyName, injector);
			return injector;
		}

		private void SetCategory(int delta)
		{
			_selectedCategory = delta;
			RenderCategory();
		}

		private void RenderCategory()
		{
			for (int i = 0; i < settingCategories.Length; i++)
			{
				var category = _categoryViews[i];
				category.gameObject.SetActive(i == _selectedCategory);
			}
		}

		private void SaveSettings()
		{
			foreach (var injector in _injectorCache)
			{
				injector.Value.Inject();
			}
			var context = DataCenter.GetContext();
			MessageBus.Publish(new OnSettingValueApplied() {DataContext = context});

			context.SaveToFile();
		}
	}
}