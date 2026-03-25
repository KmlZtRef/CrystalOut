using System.Collections;
using System.Threading.Tasks;
using _02._Script.Player.Controls;
using Unity.Cinemachine;
using UnityEngine;
using UnityUtilities;
using SceneManager = UnityUtility.SceneManagements.SceneManager;

namespace _02._Script.Stage
{
	public class StageManager : UnbreakingSingleton<StageManager>
	{
		[SerializeField] private StageDataSo stageData;
		private bool _inStage = false;
		private GameObject _loadedStage;

		public void LoadStage(StageDataSo data)
		{
			stageData = data;
			TransitionManager.Instance.OnLoadComplete += GenerateStage;
			TransitionManager.Instance.ChangeSceneWithTransition("GameScene", "Player");
		}

		private void GenerateStage()
		{
			TransitionManager.Instance.OnLoadComplete -= GenerateStage;
			if (!stageData) return;
			_loadedStage = Instantiate(stageData.StagePrefab, Vector3.zero, Quaternion.identity);
		}

		public void UnloadStage()
		{
			if (_loadedStage)
			{
				Destroy(_loadedStage);
				_loadedStage = null;
			}
		}
	}
}