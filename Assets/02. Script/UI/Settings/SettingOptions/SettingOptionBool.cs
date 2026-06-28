using System;
using _02._Script.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace _02._Script.UI.Settings.SettingOptions
{
	public class SettingOptionBool : SettingOption
	{
		[SerializeField] private Button trueButton;
		[SerializeField] private Button falseButton;

		[SerializeField] private Color trueColor = new(0f, 0.8f, 0f);
		[SerializeField] private Color falseColor = new(0.8f, 0f, 0f);
		[SerializeField] private Color disabledColor = Color.white;

		private Image _trueButtonImage;
		private Image _falseButtonImage;
		
		public override void Initialize(SettingOptionData optionData)
		{
			base.Initialize(optionData);
			trueButton.onClick.AddListener(HandleTrueButtonClicked);
			falseButton.onClick.AddListener(HandleFalseButtonClicked);

			_trueButtonImage = trueButton.GetComponent<Image>();
			_falseButtonImage = falseButton.GetComponent<Image>();
		}

		public override Type GetValueType() => typeof(bool);
		
		public override void RenderValue()
		{
			_trueButtonImage.color = (bool)Value ? trueColor : disabledColor;
			_falseButtonImage.color = (bool)Value ? disabledColor : falseColor;
		}

		private void HandleTrueButtonClicked()
		{
			Value = true;
			RenderValue();
			ValueChanged();
		}

		private void HandleFalseButtonClicked()
		{
			Value = false;
			RenderValue();
			ValueChanged();
		}
	}
}
