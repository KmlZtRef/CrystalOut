using System;
using UnityEngine;

namespace _02._Script.UI
{
	public abstract class OpenablePanelUI : MonoBehaviour
	{
		[field:SerializeField] public string PanelName { get; protected set; }
		[SerializeField] protected GameObject panel;

		protected virtual void Start()
		{
			if (panel == null)
			{
				Debug.LogWarning($"[OpenablePanelUI] Panel is null. This object ({gameObject.name}) will be disabled.");
				gameObject.SetActive(false);
			}
		}

		public virtual void OpenPanel()
		{
			panel.SetActive(true);
		}

		public virtual void ClosePanel()
		{
			panel.SetActive(false);
		}

		public virtual void SetPanelActive(bool isActive)
		{
			if (isActive)
				OpenPanel();
			else
				ClosePanel();
		}
		
		public abstract void Initialize();
	}
}