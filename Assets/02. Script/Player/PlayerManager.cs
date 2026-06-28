using System;
using _02._Script.Datas;
using _02._Script.EventParams;
using _02._Script.Logics.MessageParameters;
using _02._Script.Player.Controls;
using GameManagements;
using UnityEngine;
using UnityUtilities.SceneManagements;
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
			
			MessageBus.Subscribe<RequestLoadPlayer>(HandleStageSelect);
			MessageBus.Subscribe<RequestUnloadPlayer>(HandleStageInteracted);
		}

		private void HandleStageSelect(RequestLoadPlayer _)
		{
			MessageBus.Subscribe<OnTransitionEnded>(HandleTransitionEnded);
		}

		private void HandleTransitionEnded(OnTransitionEnded _)
		{
			MessageBus.Unsubscribe<OnTransitionEnded>(HandleTransitionEnded);
			LoadPlayer();
		}

		private void HandleStageInteracted(RequestUnloadPlayer _)
		{
			UnloadPlayer();
		}

		public async void LoadPlayer()
		{
			try
			{
				await SceneManager.Instance.AddSceneAsync(_playerSceneName);
				Player = FindFirstObjectByType<PlayerControl>();

				var context = DataCenter.GetContext();
				Player.ApplySettings(context);
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