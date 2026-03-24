using UnityEngine;

namespace UnityUtilities
{
	public static class ComponentUtility
	{
		public static T GetNewComponent<T>(this GameObject gameObject) where T : MonoBehaviour
			=> gameObject.TryGetComponent(out T component) ? component : gameObject.AddComponent<T>();
		
		public static T GetNewComponent<T>(this MonoBehaviour selfCompo) where T : MonoBehaviour
			=> selfCompo.TryGetComponent(out T component) ? component : selfCompo.gameObject.AddComponent<T>();
	}
}