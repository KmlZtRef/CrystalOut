using System;
using System.Collections;
using _02._Script.Player.Controls;
using _02._Script.Stage;
using UnityEngine;
using UnityUtilities;

namespace _02._Script.Logics
{
	public class GameManager : MonoSingleton<GameManager>
	{
		[field:SerializeField] public PlayerControl Player { get; private set; }
		[SerializeField] public float sceneTransitionDelay;
		
		private WaitForSeconds sceneTransitionDelayWait;

		private void Start()
		{
			Player ??= FindFirstObjectByType<PlayerControl>();
			
			sceneTransitionDelayWait = new WaitForSeconds(sceneTransitionDelay);
			
			CursorControl.Instance.HideCursor();
		}

		public void StageClear()
		{
			CursorControl.Instance.ShowCursor();
			Player.DisableControl();
			StartCoroutine(DelayedSceneTransition());
		}

		public IEnumerator DelayedSceneTransition()
		{
			yield return sceneTransitionDelayWait;
			TransitionManager.Instance.OnLoadComplete += HandleLoadComplete;
			TransitionManager.Instance.ChangeSceneWithTransition("MainMenu");
		}

		private void HandleLoadComplete()
		{
			StageManager.Instance.UnloadStage();
			TransitionManager.Instance.OnLoadComplete -= HandleLoadComplete;
		}
	}
}