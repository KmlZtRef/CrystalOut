using System;
using System.Collections;
using _02._Script.Logics.MessageParameters;
using GameManagements;
using UnityEngine;
using UnityUtility.SceneManagements;

namespace _02._Script.Stage
{
	public class StageManager : Manager
	{
		private string _gameSceneName;
		private string _mainMenuSceneName;
		[SerializeField] private StageDataSo stageData;
		private bool _isLoading = false;
		private StageObjectController _loadedStage;

		public void LoadStage(StageDataSo data) // 스테이지 로드
		{
			if (_isLoading) return;
			_isLoading = true;
			stageData = data;
			MessageBus.Subscribe<OnTransitionEnded>(GenerateStage);
			MessageBus.Publish(new OnStartLoadScene() {SceneName = _gameSceneName});
		}

		private void HandleClearInteracted()
		{
			MessageBus.Publish(new OnClearAreaInteracted());
		}

		private void ClearStage(OnTransitionEnded _)
		{
			MessageBus.Unsubscribe<OnTransitionEnded>(ClearStage);
			MessageBus.Publish(new OnStageUnload());
			UnloadStage();
		}

		private async void GenerateStage(OnTransitionEnded _) // 스테이지를 씬에 생성
		{
			try
			{
				MessageBus.Unsubscribe<OnTransitionEnded>(GenerateStage);

				if (!stageData) return;
				// Legacy : Prefab loading style
				// _loadedStage = Instantiate(stageData.StagePrefab, Vector3.zero, Quaternion.identity).GetComponent<StageObjectController>();

				// New : Scene loading style
				await SceneManager.Instance.AddSceneAsync(stageData.StageSceneName);
				_loadedStage = FindFirstObjectByType<StageObjectController>();
				_loadedStage.Init();
				_loadedStage.ClearArea.OnInteracted += HandleClearInteracted;
				_loadedStage.ClearArea.OnAnimEnd += HandleClearAnimEnd;

				CursorControl.Instance.HideCursor();
			}
			catch (Exception e)
			{
				Debug.LogError("[StageManager] " + e.Message);
			}
			finally
			{
				_isLoading = false;
			}
		}

		private void HandleClearAnimEnd()
		{
			MessageBus.Subscribe<OnTransitionEnded>(ClearStage); 
			MessageBus.Publish(new OnStartLoadScene() {SceneName = _mainMenuSceneName});
		}

		public void UnloadStage()
		{
			if (_loadedStage)
			{
				_loadedStage.ClearArea.OnInteracted -= HandleClearInteracted;
				
				Destroy(_loadedStage.gameObject);
				_loadedStage = null;
				
			}
			CursorControl.Instance.ShowCursor();
		}

		public override void Initialize(InitContext data)
		{
			_gameSceneName = data.GameSceneName;
			_mainMenuSceneName = data.MainMenuSceneName;
			
			MessageBus.Subscribe<OnStageSelect>(HandleOnLoadStage);
		}

		private void HandleOnLoadStage(OnStageSelect param)
		{
			LoadStage(param.StageData);
		}
	}
}