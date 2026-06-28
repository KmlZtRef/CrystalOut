using _02._Script.Datas;
using UnityEngine;

namespace _02._Script.Objects
{
	public abstract class SettingDataInjectableObject : MonoBehaviour
	{
		public abstract void InjectData(SettingDataContext context);
	}
}