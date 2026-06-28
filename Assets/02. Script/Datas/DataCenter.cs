using System;
using System.IO;
using UnityEngine;

namespace _02._Script.Datas
{
	public static class DataCenter
	{
		// Gameplay
		public static bool MoveCamOnObjectActive { get; set; }
		public static bool CollideWithProps { get; set; }
		
		// Graphic
		public static bool Fullscreen { get; set; }
		public static bool ScreenEffects { get; set; }
		
		// Controls
		public static bool ControlGuide { get; set; }
		public static float Sensitivity { get; set; }
		
		// Audio
		public static float Master { get; set; }
		public static float Bgm { get; set; }
		public static float Sfx { get; set; }
		public static float Ui { get; set; }

		public static SettingDataContext GetContext()
		{
			return new SettingDataContext()
			{
				moveCamOnObjectActive = MoveCamOnObjectActive,
				collideWithProps = CollideWithProps,
				fullscreen = Fullscreen,
				screenEffects = ScreenEffects,
				controlGuide = ControlGuide,
				sensitivity = Sensitivity,
				master = Master,
				bgm = Bgm,
				sfx = Sfx,
				ui = Ui
			};
		}

		public static void SetContext(SettingDataContext context)
		{
			MoveCamOnObjectActive = context.moveCamOnObjectActive;
			CollideWithProps = context.collideWithProps;
			Fullscreen = context.fullscreen;
			ScreenEffects = context.screenEffects;
			ControlGuide = context.controlGuide;
			Sensitivity = context.sensitivity;
			Master = context.master;
			Bgm = context.bgm;
			Sfx = context.sfx;
			Ui = context.ui;

			Modified = true;
		}

		public static bool Modified = false;
	}
}