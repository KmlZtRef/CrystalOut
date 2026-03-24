using System;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

namespace _02._Script.Options
{
	public class OptionSelectButton : MonoBehaviour
	{
		public event Action<string> OnClick;
		public string id;
		public string displayName;

		protected Button Btn;
		protected Text Text;

		protected void Awake()
		{
			Btn = GetComponent<Button>();
			Text = GetComponentInChildren<Text>();
		}

		public void Initialize(string id, string displayName)
		{
			this.id = id;
			this.displayName = displayName;
		
			Btn.onClick.AddListener(HandleButtonClick);
			Text.text = displayName;
		}

		protected virtual void HandleButtonClick()
		{
			OnClick?.Invoke(id);
		}
	}
}
