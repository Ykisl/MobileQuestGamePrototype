namespace Plugins.WindowsManager.ScreenLockerExtension
{
	public interface IScreenLocker : IActivatable
	{
		LockerType LockerType { get; }
	}
}