using System;
using _02._Script.Datas;
using _02._Script.EventParams;
using GameManagements;
using UnityEngine;

namespace _02._Script.Settings
{
	public class SettingDataLoader : MonoBehaviour, IBootstrapReset
	{
		public bool Initialized { get; private set; } = false;
		public void BootstrapReset()
		{
			var context = new SettingDataContext();
			bool success = context.LoadFromFile();

			if (success)
			{
				DataCenter.SetContext(context);
			}
			
			MessageBus.Subscribe<OnSceneChanged>(HandleSceneChanged);
			
			Initialized = true;
		}

		private void HandleSceneChanged(OnSceneChanged param)
		{
			var context = DataCenter.GetContext();
			MessageBus.Publish(new OnSettingValueApplied(){DataContext = context});
		}
	}
}