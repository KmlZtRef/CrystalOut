using System;
using _02._Script.Settings;

namespace _02._Script.UI.Settings.SettingOptions
{
	public class SettingOptionTest : SettingOption
	{
		public override void Initialize(SettingOptionData optionData)
		{
			base.Initialize(optionData);
		}

		public override Type GetValueType() => typeof(int);
		public override void RenderValue()
		{
			
		}
	}
}