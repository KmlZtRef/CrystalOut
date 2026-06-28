using System;
using UnityEngine;

namespace _02._Script.Options
{
	[Serializable]
	public struct OptionNameId
	{
		[field: SerializeField] public string Id { get; private set; }
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public OnClickActionEnum OnClickAction { get; private set; }

		public enum OnClickActionEnum
		{
			ToOtherCategory,
			LoadScene,
			OpenPanel
		}
	}
}