namespace NXS.Framework.Wpf.ParentChildService
{
	public class InvokeActionArgs
	{
		public bool IsHandled { get; set; }
		public readonly object Owner;
		public readonly string ActionName;
		public readonly ParameterDictionary Arguments;

		public InvokeActionArgs(string actionName, ParameterDictionary arguments)
			: this(null, actionName, arguments)
		{
		}
		public InvokeActionArgs(object owner, string actionName, ParameterDictionary arguments)
		{
			Owner = owner;
			ActionName = actionName;
			Arguments = arguments;
		}
	}
}
