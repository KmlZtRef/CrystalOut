using System;
using _02._Script.Datas;
using GameManagements;
using UnityEngine;

namespace _02._Script.UI
{
	public class KeyGuideUi : MonoBehaviour
	{
		[SerializeField] private GameObject guidePanel;
		private bool _showGuide;
		
		private void Start()
		{
			var context = DataCenter.GetContext();
			_showGuide = context.controlGuide;
			guidePanel.SetActive(_showGuide);
		}
	}
}