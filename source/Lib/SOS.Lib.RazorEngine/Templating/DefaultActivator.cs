﻿namespace SOS.Lib.RazorEngine.Templating
{
	using System;

	/// <summary>
	/// Provides a default activator.
	/// </summary>
	public class DefaultActivator : IActivator
	{
		#region Methods
		/// <summary>
		/// Creates an instance of the specified type.
		/// </summary>
		/// <param name="type">The type to create an instance.</param>
		/// <returns>The <see cref="ITemplate"/> instance.</returns>
		public ITemplate CreateInstance(Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			return (ITemplate)Activator.CreateInstance(type);
		}
		#endregion
	}
}
