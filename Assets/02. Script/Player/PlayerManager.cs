using System;
using _02._Script.Logics.MessageParameters;
using _02._Script.Player.Controls;
using GameManagements;
using UnityEngine;
using UnityUtility.SceneManagements;

namespace _02._Script.Player
{
	public class PlayerManager : Manager
	{
		private string _playerSceneName;

		public PlayerControl Player { get; private set; }

		public override void Initialize(InitContext data)
		{
			_playerSceneName = data.PlayerSceneName;
			
			MessageBus.Subscribe<OnStageSelect>(HandleStageSelect);
			MessageBus.Subscribe<OnClearAreaInteracted>(HandleStageInteracted);
		}

		private void HandleStageSelect(OnStageSelect _)
		{
			MessageBus.Subscribe<OnTransitionEnded>(HandleTransitionEnded);
		}

		private void HandleTransitionEnded(OnTransitionEnded _)
		{
			MessageBus.Unsubscribe<OnTransitionEnded>(HandleTransitionEnded);
			LoadPlayer();
		}

		private void HandleStageInteracted(OnClearAreaInteracted _)
		{
			UnloadPlayer();
		}

		public async void LoadPlayer()
		{
			try
			{
				await SceneManager.Instance.AddSceneAsync(_playerSceneName);
				Player = FindFirstObjectByType<PlayerControl>();
			}
			catch (Exception e)
			{
				Debug.Log(e.Message);
			}
		}

		public void UnloadPlayer()
		{
			Player?.DisableControl();
			if (Player != null)
				Destroy(Player.gameObject);
			Player = null;
		}
	}
}