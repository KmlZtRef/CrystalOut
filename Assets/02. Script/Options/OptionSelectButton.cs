using System;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

namespace _02._Script.Options
{
	public class OptionSelectButton : MonoBehaviour
	{
		public event Action<string, bool> OnClick;
		public string Id { get; private set; }
		public string DisplayName { get; private set; }
		public bool DontLoadScene { get; private set; }

		protected Button Btn;
		protected Text Text;

		protected void Awake()
		{
			Btn = GetComponent<Button>();
			Text = GetComponentInChildren<Text>();
		}

		public void Initialize(string id, string displayName, bool dontLoadScene = false)
		{
			this.Id = id;
			this.DisplayName = displayName;
			this.DontLoadScene = dontLoadScene;
		
			Btn.onClick.AddListener(HandleButtonClick);
			Text.text = displayName;
		}

		protected virtual void HandleButtonClick()
		{
			OnClick?.Invoke(Id, DontLoadScene);
		}
	}
}
