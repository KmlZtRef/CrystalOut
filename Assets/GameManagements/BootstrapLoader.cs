using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityUtilities.SceneManagements;
using UnityUtility.SceneManagements;

namespace GameManagements
{
	public class BootstrapLoader : MonoBehaviour
	{
		[SerializeField] private List<GameObject> resetObjects;
		[SerializeField] private string startSceneName;
		private List<IBootstrapReset> _resets = new ();

		private void Start()
		{
			foreach (var obj in resetObjects)
			{
				if (obj.TryGetComponent(out IBootstrapReset reset))
				{
					reset.BootstrapReset();
					DontDestroyOnLoad(obj);
					_resets.Add(reset);
				}
			}

			StartCoroutine(WaitForAllComplete());
		}

		private bool AllComplete => _resets.Any(x => x.Initialized);

		private IEnumerator WaitForAllComplete()
		{
			yield return new WaitUntil(()=>AllComplete);
			OnComplete();
		}

		private async void OnComplete()
		{
			Debug.Log("Complete");

			if (!string.IsNullOrEmpty(startSceneName))
			{
				try
				{
					Debug.Log("Scene Load Started");
					await SceneManager.Instance.LoadSceneAsync(startSceneName);
				}
				catch (Exception e)
				{
					Debug.LogError($"Error : {e.Message}");
				}
			}
			else
			{
				Debug.LogWarning("No scene loaded");
			}
		}
	}
}