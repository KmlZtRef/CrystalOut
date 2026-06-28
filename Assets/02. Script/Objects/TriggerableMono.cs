using UnityEngine;

namespace _02._Script.Objects
{
	public abstract class TriggerableMono : SettingDataInjectableObject, ITriggerable
	{
		public abstract void Trigger();
	}
}