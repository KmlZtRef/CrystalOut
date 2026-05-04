using UnityEngine;

namespace GameManagements.Editor
{
	[UnityEditor.CustomEditor(typeof(GameManager))]
	public class GameManagerEditor : UnityEditor.Editor
	{
		private GameManager _target;
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			_target ??= target as GameManager;
				
			bool buttonClick = GUILayout.Button("Auto Detect Managers");
			if (buttonClick)
			{
				_target?.OnButtonPress();
			}
		}
	}
}