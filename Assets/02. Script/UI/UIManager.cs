using System.Collections.Generic;
using System.Linq;
using _02._Script.EventParams;
using GameManagements;
using UnityEngine;

namespace _02._Script.UI
{
	public class UIManager : Manager
	{
		[field:SerializeField] public List<OpenablePanelUI> Panels { get; private set; } = new(); 
		public override void Initialize(InitContext data)
		{
			Panels = new List<OpenablePanelUI>();

			if (data.UILists == null)
			{
				Debug.LogError("[UIManager] UI lists not found.");
				return;
			}
			
			foreach (var panel in data.UILists.PanelPrefabs)
			{
				var panelInstance = Instantiate(panel, transform);
				panelInstance.Initialize();
				panelInstance.ClosePanel();
				Panels.Add(panelInstance);
			}
			
			MessageBus.Subscribe<RequestSetPanelActive>(SetPanelActive);
		}

		private void SetPanelActive(RequestSetPanelActive param)
		{
			SetPanelActive(param.PanelName, param.Active);
		}

		private void SetPanelActive(string panelName, bool active)
		{
			OpenablePanelUI panel = Panels.FirstOrDefault(x => x.PanelName == panelName);
			if (!panel)
			{
				Debug.LogWarning($"[UIManager] Panel named \"{panelName}\" not found.");
				return;
			}
			
			panel.SetPanelActive(active);
		}
	}
}