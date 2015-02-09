using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace NXS.Framework.Wpf
{
	public class DraggedAdorner : Adorner
	{
		#region Properties

		#region Private

		private ContentPresenter ContentPresenter { get; set; }
		private double Left { get; set; }
		private double Top { get; set; }
		private AdornerLayer AdornerLayer { get; set; }

		#endregion Private

		#endregion Properties

		#region Constructors

		public DraggedAdorner(object dragDropData, DataTemplate dragDropTemplate, UIElement adornedElement, AdornerLayer adornerLayer)
			: base(adornedElement)
		{
			this.AdornerLayer = adornerLayer;

			this.ContentPresenter = new ContentPresenter();
			this.ContentPresenter.Content = dragDropData;
			this.ContentPresenter.ContentTemplate = dragDropTemplate;
			
			this.AdornerLayer.Add(this);
		}

		#endregion Constructors

		#region Methods

		#region Protected

		protected override Size MeasureOverride(Size constraint)
		{
			this.ContentPresenter.Measure(constraint);
			return this.ContentPresenter.DesiredSize;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			this.ContentPresenter.Arrange(new Rect(finalSize));
			return finalSize;
		}

		protected override Visual GetVisualChild(int index)
		{
			return this.ContentPresenter;
		}

		protected override int VisualChildrenCount
		{
			get { return 1; }
		}

		#endregion Protected

		#region Public

		public void SetPosition(double left, double top)
		{
			this.Left = left;
			this.Top = top;

			if (this.AdornerLayer != null)
			{
				this.AdornerLayer.Update(this.AdornedElement);
			}
		}

		public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
		{
			GeneralTransformGroup result = new GeneralTransformGroup();
			result.Children.Add(base.GetDesiredTransform(transform));
			result.Children.Add(new TranslateTransform(this.Left, this.Top));

			return result;
		}

		public void Detach()
		{
			this.AdornerLayer.Remove(this);
		}

		#endregion Public

		#endregion Methods
	}
}