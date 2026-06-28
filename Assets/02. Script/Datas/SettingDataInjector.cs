using System;
using System.Reflection;
using UnityEngine;

namespace _02._Script.Datas
{
	public class SettingDataInjector
	{
		public string PropertyName { get; private set; }
		
		private readonly Type _injectionTargetType = typeof(DataCenter);
		private readonly PropertyInfo _injectionTargetProperty;

		private object _value;
		
		public SettingDataInjector(string propertyName)
		{
			PropertyName = propertyName;
			
			_injectionTargetProperty = _injectionTargetType.GetProperty(PropertyName, BindingFlags.Static |  BindingFlags.Public);
		}

		public void Inject(object value)
		{
			SetValue(value);
			Inject();
		}

		public void Inject()
		{
			_injectionTargetProperty.SetValue(null, _value);
			Debug.Log($"Injected {_value} into {PropertyName}.");
			
		}

		public object Extract()
		{
			return _injectionTargetProperty.GetValue(_value);
		}

		public void SetValue(object value)
		{
			_value = value;
		}
	}
}