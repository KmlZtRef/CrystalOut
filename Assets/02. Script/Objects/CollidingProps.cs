using _02._Script.Datas;
using UnityEngine;

namespace _02._Script.Objects
{
	public class CollidingProps : SettingDataInjectableObject
	{
		public override void InjectData(SettingDataContext context)
		{
			bool ableToCollide = context.collideWithProps;
			
			Collider[] colliders = GetComponentsInChildren<Collider>();
			foreach (Collider c in colliders)
			{
				c.enabled = ableToCollide;
			}
		}
	}
}