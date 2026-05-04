using System;
using System.Linq;
using GameManagements.Reflections;
using UnityEngine;
using UnityUtilities;

namespace GameManagements
{
	public partial class GameManager : MonoSingleton<GameManager>, IBootstrapReset
	{
		[Header("수동 할당 불필요, Awake()에서 자동 할당됨.")]
		[SerializeField] private Manager[] managers;
		[SerializeField] private InitContext initializeDatas;

		public void OnButtonPress()
		{
			DetectManagers();
		}

		public void DetectManagers()
		{
			ResetManagers(); // 배열 초기화 하고
			
			Type[] managerTypes = ManagerDetector.GetManagers(); // 클래스 찾아서

			foreach (var T in managerTypes) // 자식으로 넣어주기
			{
				GameObject obj = new GameObject(T.Name, T);
				obj.transform.SetParent(transform);
			}
			
			managers = GetComponentsInChildren<Manager>();
		}

		public void ResetManagers()
		{
			foreach (var manager in managers) // 캐싱된 오브젝트 모두 파괴
			{
				DestroyImmediate(manager.gameObject);
			}
			managers = new Manager[] { }; // 배열 비우기
		}

		public bool Initialized { get; private set; }
		public void BootstrapReset()
		{
			DetectManagers();
			ManagersInitializer.InitializeAllManagers(managers, initializeDatas);
			Initialized = true;
		}
	}
}
