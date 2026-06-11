using System;
using System.Collections.Generic;
using _02._Script.EventParams;
using GameManagements;
using UnityEngine;
using Tmp = TMPro.TextMeshProUGUI;

namespace _02._Script.UI
{
	public class DebugInfoUI : MonoBehaviour
	{
		[SerializeField] private Tmp text;

		private Dictionary<string, string> _datas = new Dictionary<string, string>();

		private void Start()
		{
			MessageBus.Subscribe<OnDebugInfoUpdate>(HandleDebugInfoUpdate);
		}

		private void HandleDebugInfoUpdate(OnDebugInfoUpdate param)
		{
			_datas[param.InfoName] = param.InfoData.ToString();
			
			Render();
		}

		private void Render()
		{
			string result = "Debug Infos :\n";
			foreach (var pair in _datas)
			{
				result += $"{pair.Key}: {pair.Value}\n";
			}
			
			text.text = result;
		}
	}
}