using UnityEngine;

namespace _02._Script.Settings
{
	[CreateAssetMenu(fileName = "Setting Option Float Slider Data", menuName = "Settings/Setting Option Datas/Float", order = 0)]
	public class SettingOptionFloatData : SettingOptionData<float>
	{
		[field: SerializeField] public float MinValue { get; private set; } = 0;
		[field: SerializeField] public float MaxValue { get; private set; } = 1;
	}
}