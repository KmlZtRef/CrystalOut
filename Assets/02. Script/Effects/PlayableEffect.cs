using UnityEngine;

namespace _02._Script.Effects
{
	public abstract class PlayableEffect : MonoBehaviour
	{
		public abstract void Play();
		public abstract void Pause();
		public abstract void Stop();
	}
}