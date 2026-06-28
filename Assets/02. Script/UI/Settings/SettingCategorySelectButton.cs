using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _02._Script.UI.Settings
{
	public class SettingCategorySelectButton : MonoBehaviour
	{
		[field: SerializeField] public Image Icon { get; private set; }
		[field: SerializeField] public Button Btn { get; private set; }

		public int Index { get; set; }
		
		public event Action<int> OnButtonClicked;

		public void Init()
		{
			Btn.onClick.AddListener(HandleButtonClicked);
		}

		private void HandleButtonClicked()
		{
			OnButtonClicked?.Invoke(Index);
		}

		public void SetIcon(Sprite sprite)
		{
			Icon.sprite = sprite;
		}
	}
}