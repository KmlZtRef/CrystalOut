using System;
using System.Globalization;
using _02._Script.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _02._Script.UI.Settings.SettingOptions
{
	public class SettingOptionFloatSlider : SettingOption
	{
		[SerializeField] private Slider slider;
		[SerializeField] private TMP_InputField inputField;
		[SerializeField] private TextMeshProUGUI placeholder;
		[SerializeField] private float minValue;
		[SerializeField] private float maxValue;

		public override void Initialize(SettingOptionData optionData)
		{
			base.Initialize(optionData);
			
			var castedData = (SettingOptionFloatData)optionData;
			
			minValue = castedData.MinValue;
			maxValue = castedData.MaxValue;
			
			slider.minValue = minValue;
			slider.maxValue = maxValue;
			
			slider.onValueChanged.AddListener(HandleSliderValueChanged);
			inputField.onEndEdit.AddListener(HandleInputFieldValueChanged);
			
			placeholder.text = minValue.ToString(CultureInfo.InvariantCulture);
		}

		public override Type GetValueType() => typeof(float);

		public override void RenderValue()
		{
			try
			{
				float casted = Convert.ToSingle(Value);
				slider.value = casted;
				inputField.text = casted.ToString();
			}
			catch (Exception e)
			{
				Debug.LogWarning($"Value is {Value.ToString()}, type is {Value.GetType().Name}, not float. \n{e.Message}");
			}
		}

		private void HandleSliderValueChanged(float value)
		{
			Value = slider.value;
			RenderValue();
			ValueChanged();
		}

		private void HandleInputFieldValueChanged(string value)
		{
			Value = float.TryParse(value, out float result) ? result : minValue;
			RenderValue();
			ValueChanged();
		}
	}
}