namespace _02._Script.EventParams
{
	/// <summary>
	/// 플레이어 상태 변경시 실행. <br/>
	/// 플레이어 -> 유체이탈 : true <br/>
	/// 유체이탈 -> 플레이어 : false
	/// </summary>
	public struct OnPlayerStateChanged
	{
		public bool PlayerState;
	}
}