using UnityEngine;
using UnityUtilities.SceneManagements;

namespace UnityUtility.SceneManagements
{
    public class SceneLoadingUI : MonoBehaviour
    {
        [SerializeField] private GameObject loadingPanel;
        private Canvas _canvas;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            SceneManager.Instance.OnBeginLoading += StartLoading;
            SceneManager.Instance.OnEndLoading += EndLoading;
        
            _canvas = GetComponent<Canvas>();
            _canvas.worldCamera = Camera.main;
        }

        private void StartLoading()
        {
            loadingPanel.SetActive(true);
        }

        private void EndLoading()
        {
            loadingPanel.SetActive(false);
            _canvas.worldCamera = Camera.main;
        }
    }
}
