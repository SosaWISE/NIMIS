using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace NXS.Framework.Wpf
{
	public class InsertionAdorner : Adorner
	{
		#region Properties

		#region Private

		private bool IsSeparatorHorizontal { get; set; }
		private AdornerLayer AdornerLayer { get; set; }
		private static Pen DefaultPen { get; set; }
		private static PathGeometry Triangle { get; set; }

		#endregion Private

		#region Public

		public bool IsInFirstHalf { get; set; }

		#endregion Public

		#endregion Properties

		#region Constructors

		// Create the pen and triangle in a static constructor and freeze them to improve performance.
		static InsertionAdorner()
		{
			DefaultPen = new Pen { Brush = Brushes.Gray, Thickness = 2 };
			DefaultPen.Freeze();

			LineSegment firstLine = new LineSegment(new Point(0, -5), false);
			firstLine.Freeze();

			LineSegment secondLine = new LineSegment(new Point(0, 5), false);
			secondLine.Freeze();

			PathFigure figure = new PathFigure { StartPoint = new Point(5, 0) };
			figure.Segments.Add(firstLine);
			figure.Segments.Add(secondLine);
			figure.Freeze();

			Triangle = new PathGeometry();
			Triangle.Figures.Add(figure);
			Triangle.Freeze();
		}

		public InsertionAdorner(bool isSeparatorHorizontal, bool isInFirstHalf, UIElement adornedElement, AdornerLayer adornerLayer)
			: base(adornedElement)
		{
			this.IsSeparatorHorizontal = isSeparatorHorizontal;
			this.IsInFirstHalf = isInFirstHalf;
			this.AdornerLayer = adornerLayer;
			this.IsHitTestVisible = false;

			this.AdornerLayer.Add(this);
		}

		#endregion Constructors

		#region Methods

		#region Private

		private void DrawTriangle(DrawingContext drawingContext, Point origin, double angle)
		{
			drawingContext.PushTransform(new TranslateTransform(origin.X, origin.Y));
			drawingContext.PushTransform(new RotateTransform(angle));

			drawingContext.DrawGeometry(DefaultPen.Brush, null, Triangle);

			drawingContext.Pop();
			drawingContext.Pop();
		}

		private void CalculateStartAndEndPoint(out Point startPoint, out Point endPoint)
		{
			startPoint = new Point();
			endPoint = new Point();

			double width = this.AdornedElement.RenderSize.Width;
			double height = this.AdornedElement.RenderSize.Height;

			if (this.IsSeparatorHorizontal)
			{
				endPoint.X = width;
				if (!this.IsInFirstHalf)
				{
					startPoint.Y = height;
					endPoint.Y = height;
				}
			}
			else
			{
				endPoint.Y = height;
				if (!this.IsInFirstHalf)
				{
					startPoint.X = width;
					endPoint.X = width;
				}
			}
		}

		#endregion Private

		#region Protected

		// This draws one line and two triangles at each end of the line.
		protected override void OnRender(DrawingContext drawingContext)
		{
			Point startPoint;
			Point endPoint;

			CalculateStartAndEndPoint(out startPoint, out endPoint);
			drawingContext.DrawLine(DefaultPen, startPoint, endPoint);

			if (this.IsSeparatorHorizontal)
			{
				DrawTriangle(drawingContext, startPoint, 0);
				DrawTriangle(drawingContext, endPoint, 180);
			}
			else
			{
				DrawTriangle(drawingContext, startPoint, 90);
				DrawTriangle(drawingContext, endPoint, -90);
			}
		}

		#endregion Protected

		#region Public

		public void Detach()
		{
			this.AdornerLayer.Remove(this);
		}

		#endregion Public

		#endregion Methods
	}
}