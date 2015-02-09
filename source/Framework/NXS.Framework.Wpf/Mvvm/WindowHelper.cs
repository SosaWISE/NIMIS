using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System;
using NXS.Framework.Wpf.Mvvm.ViewModels;

namespace NXS.Framework.Wpf.Mvvm
{
	public static class WindowHelper
	{
		#region SetInputBindingsHack

		/// <summary>
		/// HACK: unable to bind InputBindings in Xaml. This should be fixed in 4.0, but for now the hack is used
		/// </summary>
		/// <param name="window"></param>
		/// <param name="propertyName">Property to watch to know when to (re)bind the window's InputBindings</param>
		public static void SetupInputBindingsHack(Window window, string propertyName)
		{
			PropertyChangedEventHandler propChangedEventHandler = delegate(object sender, PropertyChangedEventArgs propChangedArgs)
			{
				if (string.Compare(propChangedArgs.PropertyName, propertyName, true) == 0) {

					//add input bindings from view model
					MainWindowViewModel model = sender as MainWindowViewModel;
					if (model != null) {

						//window.InputBindings.Clear();
						foreach (InputBinding item in model.InputBindings) {

							bool exists = false;

							foreach (InputBinding binding in window.InputBindings) {

								KeyGesture bindingKG = binding.Gesture as KeyGesture;
								KeyGesture itemKG = item.Gesture as KeyGesture;

								if (bindingKG != null && itemKG != null) {

									if (bindingKG.Key == itemKG.Key
										&& bindingKG.Modifiers == itemKG.Modifiers) {

										exists = true;
										break;
									}
								}
								else {
									throw new Exception("Only KeyGestures are accepted, for now.");
								}
							}

							if (!exists) {
								window.InputBindings.Add(item);
							}
						}
					}
				}
			};

			window.DataContextChanged += delegate(object sender, DependencyPropertyChangedEventArgs e)
			{
				//wire new
				MainWindowViewModel newModel = e.NewValue as MainWindowViewModel;
				if (newModel != null) {
					newModel.PropertyChanged -= propChangedEventHandler;
					newModel.PropertyChanged += propChangedEventHandler;
				}

				//unwire from old
				MainWindowViewModel oldModel = e.OldValue as MainWindowViewModel;
				if (oldModel != null) {
					oldModel.PropertyChanged -= propChangedEventHandler;
				}

			};
		}

		#endregion //SetInputBindingsHack
	}
}
