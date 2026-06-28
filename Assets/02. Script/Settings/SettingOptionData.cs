using System;
using UnityEngine;

namespace _02._Script.Settings
{
	public abstract class SettingOptionData : ScriptableObject
	{
		[field: SerializeField] public string OptionName { get; protected set; }
		[field: SerializeField] public string InjectionPropertyName { get; protected set; }
		
		public abstract Type GetDataType();
		public abstract object GetDefaultValue();
	}

	public abstract class SettingOptionData<T> : SettingOptionData
	{
		[field: SerializeField] public T DefaultValue { get; protected set; }
		
		public sealed override Type GetDataType() => typeof(T);
		public sealed override object GetDefaultValue() => DefaultValue;
	}
}