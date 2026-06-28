using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _02._Script.UI
{
	[CreateAssetMenu(fileName = "Panel UI List", menuName = "UI/Panel UI List", order = 0)]
	public class PanelUIList : ScriptableObject
	{
		[field: SerializeField] public List<OpenablePanelUI> PanelPrefabs { get; private set; }

		public OpenablePanelUI GetPanel(string panelName)
		{
			return PanelPrefabs.FirstOrDefault(panel => panel.PanelName == panelName);
		}
	}
}