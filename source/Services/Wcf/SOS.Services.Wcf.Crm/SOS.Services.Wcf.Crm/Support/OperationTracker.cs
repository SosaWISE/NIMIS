/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/21/11
 * Time: 01:28
 * 
 * Description:  Helps track what services are doing.
 *********************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading;

namespace SOS.Services.Wcf.Crm.Support
{
	[Serializable]
	public class CallFrame
	{
		#region .ctor
		public CallFrame(StackFrame oFrame)
		{
			/** Initialize. */
			EnterTime = DateTime.Now;
			FileName = oFrame.GetFileName();
			LineNumber = oFrame.GetFileLineNumber();
			var oMethod = oFrame.GetMethod();

			/** Check that there is a method. */
			if (oMethod != null)
			{
				Method = ReflectionHelpers.GetMethodSignature(oMethod, true, true,
					ReflectionHelpers.NamespaceFormat.ExcludeSystem);
			}
		}
		#endregion .ctor

		#region Properties

		public string FileName { get; private set; }
		public int LineNumber { get; private set; }
		public string Method { get; private set; }
		public DateTime EnterTime { get; private set; }
		public DateTime ExitTime { get; internal set; }

		#endregion Properties
	}

	[Serializable]
	public class OperationTracker : IExtension<OperationContext>
	{
		#region .ctor

		public OperationTracker(Uri oOriginalUri)
		{
			OriginalUri = oOriginalUri;
			RequestNumber = Interlocked.Increment(ref _requestCounter);
			_scope = new TrackingScope(this);
			Frames = new List<CallFrame>();
			_frameStack = new Stack<CallFrame>();
		}

		#endregion .ctor

		#region Memeber Classes

		private class TrackingScope : IDisposable
		{
			#region .ctor
			public TrackingScope(OperationTracker oTracker)
			{
				_tracker = oTracker;
			}
			#endregion .ctor

			#region Properties

			private readonly OperationTracker _tracker;

			#endregion Properties

			#region Implementation of IDisposable

			/// <summary>
			/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
			/// </summary>
			/// <filterpriority>2</filterpriority>
			public void Dispose()
			{
				_tracker.ExitScope();
			}

			#endregion Implementation of IDisposable
		}

		private class NoopScope : IDisposable
		{
			public void Dispose() {}
		}

		#endregion Memeber Classes

		#region Properties

		private static int _requestCounter;
		[NonSerialized]
		private readonly TrackingScope _scope;

		[NonSerialized]
		private readonly Stack<CallFrame> _frameStack;

		private static readonly NoopScope _noopScope = new NoopScope();

		public List<CallFrame> Frames { get; private set; } 
		public DateTime StartTime { get; private set; }
		public DateTime EndTime { get; private set; }
		public Uri OriginalUri { get; private set; }
		public int RequestNumber { get; private set; }

		public static OperationTracker Current
		{
			get
			{
				if (OperationContext.Current == null)
					return null;

				var oExt = OperationContext.Current.Extensions.Find<OperationTracker>();
				return oExt;
			}
		}
		#endregion Properties

		#region Methods
		private IDisposable EnterScope(int nFramesToSkip)
		{
			var call = new CallFrame(new StackFrame(2 + nFramesToSkip, true));
			Frames.Add(call);
			_frameStack.Push(call);

			return _scope;
		}

		private void ExitScope()
		{
			if (_frameStack.Count == 0) return;

			var frame = _frameStack.Pop();
			frame.ExitTime = DateTime.Now;
		}

		public static IDisposable EnterMethod()
		{
			/** Initialize. */
			var oCurrent = Current;
			if (!OperationTrackingInspector.Enabled || oCurrent == null)
				return _noopScope;

			return oCurrent.EnterScope(0);
		}

		public static IDisposable EnterCalleeMethod(int nFramesToSkip = 1)
		{
			/** Initialize. */
			var oCurrent = Current;
			if (!OperationTrackingInspector.Enabled || oCurrent == null)
				return _noopScope;

			return oCurrent.EnterScope(nFramesToSkip);
		}
		#endregion Methods

		#region Implementation of IExtension<OperationContext>

		/// <summary>
		/// Enables an extension object to find out when it has been aggregated. Called when the extension is added to the <see cref="P:System.ServiceModel.IExtensibleObject`1.Extensions"/> property.
		/// </summary>
		/// <param name="owner">The extensible object that aggregates this extension.</param>
		public void Attach(OperationContext owner)
		{
			StartTime = DateTime.Now;
		}

		/// <summary>
		/// Enables an object to find out when it is no longer aggregated. Called when an extension is removed from the <see cref="P:System.ServiceModel.IExtensibleObject`1.Extensions"/> property.
		/// </summary>
		/// <param name="owner">The extensible object that aggregates this extension.</param>
		public void Detach(OperationContext owner)
		{
			EndTime = DateTime.Now;
		}

		#endregion Implementation of IExtension<OperationContext>
	}
}