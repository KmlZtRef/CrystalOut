using System;
using UnityEngine;

namespace _02._Script.Stage
{
	public class StageSelector : MonoBehaviour
	{
		[SerializeField] private StageDataListSo dataList;
		
		[SerializeField] private StageInfoContainer containerPrf;

		[SerializeField] private Transform contents;

		private void Start()
		{
			RenderContents();	
		}

		private void RenderContents()
		{
			foreach (StageDataSo data in dataList.list)
			{
				var container = Instantiate(containerPrf, contents);
				container.Initialize(data);
			}
		}

		// public void Test()
		// {
		// 	StageManager.Instance.LoadStage();
		// }
	}
}