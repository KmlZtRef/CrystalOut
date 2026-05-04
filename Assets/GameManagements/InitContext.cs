using _02._Script.Player.Controls;
using UnityEngine;

namespace GameManagements
{
	[CreateAssetMenu(fileName = "InitContext", menuName = "Managements/InitContext")]
	public class InitContext : ScriptableObject
	{
		[field: SerializeField] public LoadingUI LoadingUIPrefab { get; private set; }
		[field: SerializeField] public string PlayerSceneName { get; private set; }
		[field: SerializeField] public string GameSceneName { get; private set; } 
		[field: SerializeField] public string MainMenuSceneName { get; private set; }
	}
}