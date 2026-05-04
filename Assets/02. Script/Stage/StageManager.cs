using System.Collections;
using _02._Script.Logics.MessageParameters;
using GameManagements;
using UnityEngine;

namespace _02._Script.Stage
{
	public class StageManager : Manager
	{
		private string _gameSceneName = "GameScene";
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
			
			// Legacy:
			// TransitionManager.Instance.ChangeSceneWithTransition("GameScene", "Player");
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

		private void GenerateStage(OnTransitionEnded _) // 스테이지를 씬에 생성
		{
			MessageBus.Unsubscribe<OnTransitionEnded>(GenerateStage);
			
			// Legacy:
			// TransitionManager.Instance.OnLoadComplete -= GenerateStage;
			
			if (!stageData) return;
			_loadedStage = Instantiate(stageData.StagePrefab, Vector3.zero, Quaternion.identity).GetComponent<StageObjectController>();
			_loadedStage.Init();
			_loadedStage.ClearArea.OnInteracted += HandleClearInteracted;
			_loadedStage.ClearArea.OnAnimEnd += HandleClearAnimEnd;
			
			CursorControl.Instance.HideCursor();
			
			_isLoading = false;
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