using UnityEngine;

namespace UnityUtilities
{
	public abstract class UnbreakingSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance = null;
		private static bool _isDestroyed = false;
        
		protected virtual void Awake()
		{
			if (_instance == null)
			{
				_instance = this as T;
				DontDestroyOnLoad(gameObject);
			}
			else if (_instance != this as T)
			{
				Destroy(gameObject);
			}
		}
        
		public static T Instance
		{
			get
			{
				if (_isDestroyed)
				{
					_instance = null;
				}

				if (_instance == null)
				{
					_instance = GameObject.FindFirstObjectByType<T>();

					if (_instance == null)
					{
						GameObject obj = new GameObject(typeof(T).Name);
						_instance = obj.AddComponent<T>();
					}
					else
					{
						_isDestroyed = false;
					}
				}

				return _instance;
			}
		}
	}
}