using System;
using _02._Script.EventParams;
using _02._Script.Logics.MessageParameters;
using GameManagements;
using UnityEngine;
using UnityEngine.UI;

namespace _02._Script.UI
{
	public class PauseUI : MonoBehaviour
	{
		[SerializeField] private GameObject panel;
		[SerializeField] private Button resumeButton;
		[SerializeField] private Button restartButton;
		[SerializeField] private Button settingsButton;
		[SerializeField] private Button quitButton;

		private void Start()
		{
			resumeButton.onClick.AddListener(Resume);
			restartButton.onClick.AddListener(RestartStage);
			settingsButton.onClick.AddListener(OpenSettings);
			quitButton.onClick.AddListener(QuitStage);
			
			panel.SetActive(false);
			
			MessageBus.Subscribe<OnGamePaused>(OpenPanel);
		}

		private void OpenPanel(OnGamePaused param)
		{
			panel.SetActive(true);
			Time.timeScale = 0;
			CursorControl.Instance.ShowCursor();
		}

		private void Resume()
		{
			panel.SetActive(false);
			Time.timeScale = 1;
			CursorControl.Instance.HideCursor();
		}

		private void RestartStage()
		{
			Time.timeScale = 1;
			MessageBus.Publish(new RequestUnloadPlayer());
			MessageBus.Publish(new OnRestartStage());
		}

		private void OpenSettings()
		{
			MessageBus.Publish(new RequestSetPanelActive() {PanelName = "Setting", Active = true});
		}

		private void QuitStage()
		{
			Debug.Log("[PauseUI] Quit Stage");
			Time.timeScale = 1;
			MessageBus.Publish(new RequestUnloadPlayer());
			MessageBus.Publish(new RequestQuitStage());
		}

		private void OnDestroy()
		{
			MessageBus.Unsubscribe<OnGamePaused>(OpenPanel);
			Time.timeScale = 1;
		}
	}
}