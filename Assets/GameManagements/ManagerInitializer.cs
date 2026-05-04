namespace GameManagements
{
	public partial class GameManager
	{
		public static class ManagersInitializer
		{
			public static void InitializeAllManagers(Manager[] managers, InitContext data)
			{
				foreach (var manager in managers)
				{
					manager.Initialize(data);
				}
			}
		}
	}
}