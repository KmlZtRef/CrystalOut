using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace _02._Script.Datas
{
	[Serializable]
	public class SettingDataContext
	{
		// Gameplay
		public bool moveCamOnObjectActive;
		public bool collideWithProps;
		
		// Graphic
		public bool fullscreen;
		public bool screenEffects;
		
		// Controls
		public bool controlGuide;
		public float sensitivity;
		
		// Audio
		public float master;
		public float bgm;
		public float sfx;
		public float ui;
		
		public void SaveToFile()
		{
			string filePath = Path.Combine(Application.persistentDataPath, "settings.json");
			string json = JsonUtility.ToJson(this, true);
			
			File.WriteAllText(filePath, json);

			Debug.Log($"Saved settings to {filePath}.");
		}

		public bool LoadFromFile()
		{
			string filePath = Path.Combine(Application.persistentDataPath, "settings.json");
			if (!File.Exists(filePath))
			{
				Debug.LogWarning("Save file not found.");
				return false;
			}
			string json = File.ReadAllText(filePath);
			JsonUtility.FromJsonOverwrite(json, this);
			Debug.Log($"Loaded settings from {filePath}.");
			return true;
		}
	}
}