using UnityEngine;

namespace GameManagements
{
	public abstract class Manager : MonoBehaviour
	{
		public abstract void Initialize(InitContext data);
	}
}