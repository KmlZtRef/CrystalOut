using System;
using System.Collections;
using UnityEngine;
using UnityUtilities;
using UnityUtility.SceneManagements;

public class TransitionManager : UnbreakingSingleton<TransitionManager>
{
    [SerializeField] private LoadingUI loadingUI;
    
    public event Action OnLoadComplete;

    public void ChangeSceneWithTransition(string sceneName, params string[] additiveScenes)
    {
        if (loadingUI == null) return;

        if (loadingUI.IsLoading)
        {
            Debug.Log("Already loading!");
            return;
        }
        
        StartCoroutine(TransitionCoroutine(sceneName, additiveScenes));
    }

    private IEnumerator TransitionCoroutine(string sceneName, string[] additiveScenes)
    {
        loadingUI.Open();
        yield return new WaitUntil(() => !loadingUI.IsLoading);
        SceneManager.Instance.LoadOneSceneAsync(sceneName);
        yield return new WaitUntil(() => !SceneManager.Instance.IsLoading);
        
        if (additiveScenes is { Length: > 0 }) // Load Additive Scenes
        {
            for (int i = 0; i < additiveScenes.Length; i++)
            {
                SceneManager.Instance.AddSceneAsync(additiveScenes[i]);
                yield return new WaitUntil(() => !SceneManager.Instance.IsLoading);
            }
        }
        
        OnLoadComplete?.Invoke();
        
        loadingUI.Close();
    }
}
