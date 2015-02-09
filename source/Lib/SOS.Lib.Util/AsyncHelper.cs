using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace SOS.Lib.Util
{
	public class AsyncHelper
	{
		#region Methods

		#region Private

		private static void ExecuteTransform<TInput, TOutput>(IEnumerable<TInput> payload, Func<TInput, TOutput> transform,
		                                                      ObservableCollection<TOutput> output,
		                                                      SynchronizationContext syncContext)
		{
			foreach (TInput inputItem in payload)
			{
				TOutput outputItem = transform(inputItem);
				syncContext.Post(param => output.Add(outputItem), null);
			}
		}

		private static void ExecuteTransform<TInput, TOutput>(IEnumerable<TInput> payload, Func<TInput, TOutput> transform,
		                                                      ObservableCollection<TOutput> output)
		{
			foreach (TInput inputItem in payload)
			{
				TOutput outputItem = transform(inputItem);
				output.Add(outputItem);
			}
		}

		#endregion Private

		#region Public

		public static void LoadDataAsync<T>(Func<IEnumerable<T>> loadData, ObservableCollection<T> output,
		                                    Action onCompleted, Action<Exception> onError)
		{
			LoadDataAsync(loadData, output, onCompleted, onError, SynchronizationContext.Current);
		}

		public static void LoadDataAsync<T>(Func<IEnumerable<T>> loadData, ObservableCollection<T> output,
		                                    Action onCompleted, Action<Exception> onError, SynchronizationContext syncContext)
		{
			if (loadData == null)
			{
				throw new ArgumentNullException("loadData");
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (onError == null)
			{
				throw new ArgumentNullException("onError");
			}

			syncContext = syncContext ?? SynchronizationContext.Current;
			if (syncContext == null)
				throw new InvalidOperationException("LoadDataAsync must be invoked from a thread that has a SynchronizationContext.");

			ThreadPool.QueueUserWorkItem(delegate
			                             	{
			                             		try
			                             		{
			                             			// Fetch the data
			                             			IEnumerable<T> payload = loadData();

			                             			syncContext.Post(arg =>
			                             			                 	{
			                             			                 		// Transform each data object and add it to the output collection
			                             			                 		if (payload != null)
			                             			                 		{
			                             			                 			foreach (T currItem in payload)
			                             			                 			{
			                             			                 				output.Add(currItem);
			                             			                 			}
			                             			                 		}
			                             			                 	}, null);

			                             			// Call the onCompleted handler, if there is one
			                             			if (onCompleted != null)
			                             			{
			                             				syncContext.Post(arg => onCompleted(), null);
			                             			}
			                             		}
			                             		catch (Exception ex)
			                             		{
			                             			syncContext.Post(arg => onError(ex), null);
			                             		}
			                             	});
		}

		public static void LoadAndTransformDataAsync<TInput, TOutput>(Func<IEnumerable<TInput>> loadData,
		                                                              Func<TInput, TOutput> transform,
		                                                              ObservableCollection<TOutput> output, Action onCompleted,
		                                                              Action<Exception> onError)
		{
			bool transformOnSyncContext = false;
			LoadAndTransformDataAsync(loadData, transform, output, onCompleted, onError, SynchronizationContext.Current,
			                          transformOnSyncContext);
		}

		public static void LoadAndTransformDataAsync<TInput, TOutput>(Func<IEnumerable<TInput>> loadData,
		                                                              Func<TInput, TOutput> transform,
		                                                              ObservableCollection<TOutput> output, Action onCompleted,
		                                                              Action<Exception> onError,
		                                                              SynchronizationContext syncContext,
		                                                              bool transformOnSyncContext)
		{
			if (loadData == null)
			{
				throw new ArgumentNullException("loadData");
			}
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (onError == null)
			{
				throw new ArgumentNullException("onError");
			}

			syncContext = syncContext ?? SynchronizationContext.Current;
			if (syncContext == null)
				throw new InvalidOperationException(
					"LoadAndTransformDataAsync must be invoked from a thread that has a SynchronizationContext.");

			ThreadPool.QueueUserWorkItem(delegate
			                             	{
			                             		try
			                             		{
			                             			// Fetch the data
			                             			IEnumerable<TInput> payload = loadData();

			                             			// Transform each data object and add it to the output collection
			                             			if (payload != null)
			                             			{
			                             				if (transformOnSyncContext)
			                             				{
			                             					// Do the transform on the synchronization context
			                             					syncContext.Post(arg => ExecuteTransform(payload, transform, output), null);
			                             				}
			                             				else
			                             				{
			                             					// Do the transform on the worker thread
			                             					ExecuteTransform(payload, transform, output, syncContext);
			                             				}
			                             			}

			                             			// Call the onCompleted handler, if there is one
			                             			if (onCompleted != null)
			                             			{
			                             				syncContext.Post(arg => onCompleted(), null);
			                             			}
			                             		}
			                             		catch (Exception ex)
			                             		{
			                             			syncContext.Post(arg => onError(ex), null);
			                             		}
			                             	});
		}

		public static void LoadSingle<T>(Func<T> getItem, Action<T> onCompleted, Action<Exception> onError)
		{
			LoadSingle(getItem, onCompleted, onError, null);
		}

		public static void LoadSingle<T>(Func<T> getItem, Action<T> onCompleted, Action<Exception> onError,
		                                 SynchronizationContext syncContext)
		{
			if (getItem == null)
			{
				throw new ArgumentNullException("getItem");
			}
			if (onCompleted == null)
			{
				throw new ArgumentNullException("onCompleted");
			}
			if (onError == null)
			{
				throw new ArgumentNullException("onError");
			}

			syncContext = syncContext ?? SynchronizationContext.Current;
			if (syncContext == null)
				throw new InvalidOperationException("LoadSingle must be invoked from a thread that has a SynchronizationContext.");

			ThreadPool.QueueUserWorkItem(delegate
			                             	{
			                             		try
			                             		{
			                             			// Fetch the data object
			                             			T item = getItem();

			                             			// Call the onCompleted function on the synchronization context
			                             			syncContext.Post(arg => onCompleted(item), null);
			                             		}
			                             		catch (Exception ex)
			                             		{
			                             			syncContext.Post(arg => onError(ex), null);
			                             		}
			                             	});
		}

		/// <summary>
		/// Executes the work delegate on a worker thread and then switches back to the SynchronizationContext and calls the onCompleted
		/// delegate with the results of the work delegate.
		/// </summary>
		/// <typeparam name="T">The return type of the work delegate.  Also the input type to the onCompleted delegate.</typeparam>
		/// <param name="work">A delegate representing the work to be done on the worker thread.</param>
		/// <param name="onCompleted">A delegate for processing the results of the work delegate.  This will be called on the SynchronizationContext.</param>
		/// <param name="onError">A delegate that will be called if an error occurrs.</param>
		public static void ExecuteAsync<T>(Func<T> work, Action<T> onCompleted, Action<Exception> onError)
		{
			ExecuteAsync(work, onCompleted, onError, SynchronizationContext.Current);
		}

		/// <summary>
		/// Executes the work delegate on a worker thread and then switches back to the SynchronizationContext and calls the onCompleted
		/// delegate with the results of the work delegate.
		/// </summary>
		/// <typeparam name="T">The return type of the work delegate.  Also the input type to the onCompleted delegate.</typeparam>
		/// <param name="work">A delegate representing the work to be done on the worker thread.</param>
		/// <param name="onCompleted">A delegate for processing the results of the work delegate.  This will be called on the SynchronizationContext.</param>
		/// <param name="onError">A delegate that will be called if an error occurrs.</param>
		/// <param name="syncContext">The SynchronizationContext to use for invoking the onCompleted delegate.</param>
		public static void ExecuteAsync<T>(Func<T> work, Action<T> onCompleted, Action<Exception> onError,
		                                   SynchronizationContext syncContext)
		{
			if (work == null)
			{
				throw new ArgumentNullException("work");
			}
			if (onCompleted == null)
			{
				throw new ArgumentNullException("onCompleted");
			}
			if (onError == null)
			{
				throw new ArgumentNullException("onError");
			}

			syncContext = syncContext ?? SynchronizationContext.Current;
			if (syncContext == null)
				throw new InvalidOperationException("ExecuteAsync must be invoked from a thread that has a SynchronizationContext.");

			// Initiate a request for a worker thread to execute
			ThreadPool.QueueUserWorkItem(delegate
			                             	{
			                             		try
			                             		{
			                             			// Execute the work method and capture the result
			                             			T workResult = work();

			                             			// Switch to the syncContext to execute the onCompleted function, passing the workResult as the parameter
			                             			syncContext.Post(arg => onCompleted((T) arg), workResult);
			                             		}
			                             		catch (Exception ex)
			                             		{
			                             			// Switch to the syncContext to execute the onError function, passing the exception as the parameter
			                             			syncContext.Post(arg => onError(arg as Exception), ex);
			                             		}
			                             	});
		}

		/// <summary>
		/// Executes the work delegate on a worker thread and then switches back to the SynchronizationContext and calls the onCompleted
		/// delegate with the results of the work delegate.
		/// </summary>
		/// <param name="work">A delegate representing the work to be done on the worker thread.</param>
		/// <param name="onCompleted">A delegate for processing the results of the work delegate.  This will be called on the SynchronizationContext.</param>
		/// <param name="onError">A delegate that will be called if an error occurrs.</param>
		public static void ExecuteAsync(Action work, Action onCompleted, Action<Exception> onError)
		{
			ExecuteAsync(work, onCompleted, onError, SynchronizationContext.Current);
		}

		public static bool IsBackgroundThread()
		{
			return SynchronizationContext.Current == null;
		}

		/// <summary>
		/// Executes the work delegate on a worker thread and then switches back to the SynchronizationContext and calls the onCompleted
		/// delegate with the results of the work delegate.
		/// </summary>
		/// <param name="work">A delegate representing the work to be done on the worker thread.</param>
		/// <param name="onCompleted">A delegate for processing the results of the work delegate.  This will be called on the SynchronizationContext.</param>
		/// <param name="onError">A delegate that will be called if an error occurrs.</param>
		/// <param name="syncContext">The SynchronizationContext to use for invoking the onCompleted delegate.</param>
		public static void ExecuteAsync(Action work, Action onCompleted, Action<Exception> onError,
		                                SynchronizationContext syncContext)
		{
			if (work == null)
			{
				throw new ArgumentNullException("work");
			}
			if (onError == null)
			{
				throw new ArgumentNullException("onError");
			}

			syncContext = syncContext ?? SynchronizationContext.Current;
			if (syncContext == null)
			{
				throw new InvalidOperationException("ExecuteAsync must be invoked from a thread that has a SynchronizationContext.");
			}

			// Initiate a request for a worker thread to execute
			ThreadPool.QueueUserWorkItem(delegate
			                             	{
			                             		try
			                             		{
			                             			// Execute the work method and capture the result
			                             			work();

			                             			// Switch to the syncContext to execute the onCompleted function, passing the workResult as the parameter
			                             			if (onCompleted != null)
			                             			{
			                             				syncContext.Post(arg => onCompleted(), null);
			                             			}
			                             		}
			                             		catch (Exception ex)
			                             		{
			                             			// Switch to the syncContext to execute the onError function, passing the exception as the parameter
			                             			syncContext.Post(arg => onError(arg as Exception), ex);
			                             		}
			                             	});
		}

		public static void ExecuteSync(Action work)
		{
			SynchronizationContext syncContext = SynchronizationContext.Current;
			if (syncContext == null)
			{
				work();
			}
			else
			{
				syncContext.Post(arg => work(), null);
			}
		}

		#endregion Public

		#endregion Methods
	}
}