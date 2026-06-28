using System;
using _02._Script.Datas;
using _02._Script.EventParams;
using _02._Script.Settings;
using GameManagements;
using UnityEngine;
using Tmp = TMPro.TextMeshProUGUI;

namespace _02._Script.UI.Settings.SettingOptions
{
	public abstract class SettingOption : MonoBehaviour
	{
		[SerializeField] protected Tmp text;
		
		public event Action<object, string> OnValueChanged;
		protected object Value;
		protected string InjectionPropertyName;

		public virtual void Initialize(SettingOptionData optionData)
		{
			text.text = optionData.OptionName;
			InjectionPropertyName = optionData.InjectionPropertyName;
			Value = optionData.GetDefaultValue();
			
			MessageBus.Subscribe<RequestForcedLoadValue>(ForcedLoadValue);
			MessageBus.Subscribe<RequestSetToInjector>(SetDataToInjector);
		}

		private void SetDataToInjector(RequestSetToInjector param)
		{
			ValueChanged();
		}

		public virtual void LateInitialize()
		{
			RenderValue();
		}

		public abstract Type GetValueType();
		public abstract void RenderValue();

		protected virtual void ValueChanged()
		{
			OnValueChanged?.Invoke(Value, InjectionPropertyName);
		}

		protected virtual void ForcedLoadValue(RequestForcedLoadValue param)
		{
			SettingDataInjector injector = new SettingDataInjector(InjectionPropertyName);
			Value = injector.Extract();
			RenderValue();
		}
	}
}