using System;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

namespace _02._Script.Options
{
	public class OptionSelectButton : MonoBehaviour
	{
		public event Action<string, OptionNameId.OnClickActionEnum> OnClick;
		public string Id { get; private set; }
		public string DisplayName { get; private set; }
		public OptionNameId.OnClickActionEnum OnClickAction { get; private set; }

		protected Button Btn;
		protected Text Text;

		protected void Awake()
		{
			Btn = GetComponent<Button>();
			Text = GetComponentInChildren<Text>();
		}

		public void Initialize(string id, string displayName, OptionNameId.OnClickActionEnum onClick)
		{
			this.Id = id;
			this.DisplayName = displayName;
			this.OnClickAction = onClick;
		
			Btn.onClick.AddListener(HandleButtonClick);
			Text.text = displayName;
		}

		protected virtual void HandleButtonClick()
		{
			OnClick?.Invoke(Id, OnClickAction);
		}
	}
}
