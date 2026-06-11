using UnityEngine;

namespace _02._Script.Objects
{
	public abstract class TriggerableMono : MonoBehaviour, ITriggerable
	{
		public abstract void Trigger();
	}
}