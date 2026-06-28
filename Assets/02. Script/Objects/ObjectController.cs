using _02._Script.Datas;
using UnityEngine;

namespace _02._Script.Objects
{
	public class ObjectController : MonoBehaviour
	{
		[SerializeField] private SettingDataInjectableObject[] objects;

		public void ResetAllObjects(SettingDataContext context)
		{
			foreach (var obj in objects)
			{
				obj.InjectData(context);
			}
		}
	}
}