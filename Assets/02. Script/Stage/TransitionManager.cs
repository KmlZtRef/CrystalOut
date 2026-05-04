using System;
using System.Collections;
using _02._Script.Logics.MessageParameters;
using GameManagements;
using UnityEngine;
using UnityUtility.SceneManagements;

namespace _02._Script.Stage
{
    public class TransitionManager : Manager
    {
        private LoadingUI _loadingUIPrefab;
        private LoadingUI _loadingUI;

        public void ChangeSceneWithTransition(string sceneName, params string[] additiveScenes)
        {
            if (_loadingUI == null) return;

            if (_loadingUI.IsLoading)
            {
                Debug.Log("Already loading!");
                return;
            }
        
            StartCoroutine(TransitionCoroutine(sceneName, additiveScenes));
        }

        private IEnumerator TransitionCoroutine(string sceneName, string[] additiveScenes)
        {
            _loadingUI.Open();
            yield return new WaitUntil(() => !_loadingUI.IsLoading); // 로딩 UI 완전히 열릴때까지 기다림
            SceneManager.Instance.LoadOneSceneAsync(sceneName);
            yield return new WaitUntil(() => !SceneManager.Instance.IsLoading); // 씬 로드 끝날때까지 기다림
        
            if (additiveScenes is { Length: > 0 }) // Load Additive Scenes
            {
                for (int i = 0; i < additiveScenes.Length; i++)
                {
                    SceneManager.Instance.AddSceneAsync(additiveScenes[i]);
                    yield return new WaitUntil(() => !SceneManager.Instance.IsLoading);
                }
            }
        
            MessageBus.Publish(new OnTransitionEnded());
        
            _loadingUI.Close();
        }

        public override void Initialize(InitContext data)
        {
            MessageBus.Subscribe<OnStartLoadScene>(HandleStartLoadStage);
            // 로딩 UI 생성
            _loadingUIPrefab = data.LoadingUIPrefab;
            _loadingUI = Instantiate(_loadingUIPrefab);
            DontDestroyOnLoad(_loadingUI.gameObject);
        }

        private void HandleStartLoadStage(OnStartLoadScene param)
        {
            ChangeSceneWithTransition(param.SceneName);
        }
    }
}
