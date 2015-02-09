using NXS.Framework.Wpf.ParentChildService;

namespace NXS.Framework.Wpf.Mvvm.Security
{
	public class ExecutionArgs
	{
		//public UserSecurityInfo ExecutingUser { get; set; }
		public ParameterDictionary Arguments { get; set; }

		public ExecutionArgs(/*UserSecurityInfo executingUser, */ParameterDictionary arguments)
		{
			//if (executingUser == null)
			//    throw new ArgumentNullException("executingUser");
			//this.ExecutingUser = executingUser;

			Arguments = arguments;
		}
	}
}
