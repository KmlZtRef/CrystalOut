using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace UnityUtility.SceneManagements
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            UnitySceneManager.sceneLoaded += OnSceneChangedHandle;
        }

        private string _currentSceneName;

        public Action OnBeginLoading;
        public Action OnEndLoading;

        public bool IsLoading { get; private set; } = false;

        private void Start()
        {
            _currentSceneName = UnitySceneManager.GetActiveScene().name;
        }

        public async Task LoadSceneAsync(string sceneName)
        {
            IsLoading = true;
            OnBeginLoading?.Invoke();
            await UnitySceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            await UnitySceneManager.UnloadSceneAsync(_currentSceneName);
            
            _currentSceneName = sceneName;
            OnEndLoading?.Invoke();
            IsLoading = false;
        }

        public async Task LoadOneSceneAsync(string sceneName)
        {
            IsLoading = true;
            OnBeginLoading?.Invoke();
            await UnitySceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            
            _currentSceneName = sceneName;
            OnEndLoading?.Invoke();
            IsLoading = false;
        }

        public async Task AddSceneAsync(string sceneName)
        {
            await UnitySceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public async Task UnloadSceneAsync(string sceneName)
        {
            await UnitySceneManager.UnloadSceneAsync(sceneName);
        }

        private void OnSceneChangedHandle(Scene scene, LoadSceneMode mode)
        {
            
        }
    }
}
