namespace NXS.Framework.Wpf.Mvvm.Security
{
	public interface ISecuritySubscriber
	{
		void SetLocalCommands(SecurityController securityController);
		void UnregisterConcreteCommands(SecurityController securityController);
		void RegisterConcreteCommands(SecurityController securityController);
	}
}
