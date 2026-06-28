using System;
using _02._Script.EventParams;
using GameManagements;
using UnityEngine;
using UnityEngine.Audio;

namespace _02._Script.Sounds
{
	public class SoundManager : Manager
	{
		private AudioMixer _audioMixer;
		public override void Initialize(InitContext data)
		{
			_audioMixer = data.AudioMixer;
			
			MessageBus.Subscribe<OnSettingValueApplied>(HandleSettingApplied);
		}

		private void HandleSettingApplied(OnSettingValueApplied param)
		{
			var context = param.DataContext;
			
			_audioMixer.SetFloat("Master", ToDecibels(context.master));
			_audioMixer.SetFloat("Bgm", ToDecibels(context.bgm));
			_audioMixer.SetFloat("Sfx", ToDecibels(context.sfx));
			_audioMixer.SetFloat("Ui", ToDecibels(context.ui));
		}

		private float ToDecibels(float value)
		{
			return Mathf.Clamp(Mathf.Log10(value / 100) * 20, -80f, 20f);
		}
	}
}