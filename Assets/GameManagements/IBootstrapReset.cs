namespace GameManagements
{
	public interface IBootstrapReset
	{
		public bool Initialized { get; }
		public void BootstrapReset();
	}
}