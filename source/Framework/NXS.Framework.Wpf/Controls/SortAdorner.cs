using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace NXS.Framework.Wpf.Controls
{
	public class SortAdorner : Adorner
	{
		#region Properties

		#region Private

		private static readonly Geometry _ascendingGlyph = Geometry.Parse("M 0,0 L 10,0 L 5,5 Z");
		private static readonly Geometry _descendingGlyph = Geometry.Parse("M 0,5 L 10,5 L 5,0 Z");

		#endregion Private

		#region Public

		public ListSortDirection Direction { get; private set; }

		#endregion Public

		#endregion Properties

		#region Constructors

		public SortAdorner(UIElement element, ListSortDirection dir)
			: base(element)
		{
			Direction = dir;
		}

		#endregion Constructors

		#region Methods

		#region Protected

		protected override void OnRender(DrawingContext drawingContext)
		{
			base.OnRender(drawingContext);

			if (AdornedElement.RenderSize.Width < 20)
				return;

			drawingContext.PushTransform(new TranslateTransform(AdornedElement.RenderSize.Width - 15,
				(AdornedElement.RenderSize.Height - 5)/2));

			drawingContext.DrawGeometry(Brushes.Black, null,
				Direction == ListSortDirection.Ascending ? _ascendingGlyph : _descendingGlyph);

			drawingContext.Pop();
		}

		#endregion Protected

		#endregion Methods
	}
}