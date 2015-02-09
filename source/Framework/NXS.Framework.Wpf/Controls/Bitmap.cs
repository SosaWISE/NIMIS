using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NXS.Framework.Wpf.Controls
{
	/// <summary>
	///     See http://blogs.msdn.com/dwayneneed/archive/2007/10/05/blurry-bitmaps.aspx
	/// </summary>
	public class Bitmap : FrameworkElement
	{
		public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof (BitmapSource),
			typeof (Bitmap),
			new FrameworkPropertyMetadata(null,
				FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure,
				OnSourceChanged));

		private readonly EventHandler _sourceDownloaded;
		private readonly EventHandler<ExceptionEventArgs> _sourceFailed;
		private Point _pixelOffset;

		/// <summary>
		///     Initializes a new instance of the <see cref="Bitmap" /> class.
		/// </summary>
		static Bitmap()
		{
			// Override the metadata of the IsEnabled property.
			IsEnabledProperty.OverrideMetadata(typeof (Bitmap),
				new FrameworkPropertyMetadata(true, OnAutoGreyScaleImageIsEnabledPropertyChanged));
		}

		public Bitmap()
		{
			_sourceDownloaded = OnSourceDownloaded;
			_sourceFailed = OnSourceFailed;

			LayoutUpdated += OnLayoutUpdated;

			if (Initialized != null)
			{
				Initialized(this, EventArgs.Empty);
			}
		}

		public BitmapSource Source
		{
			get { return (BitmapSource) GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}

		public event EventHandler Initialized;

		/// <summary>
		///     Called when [auto grey scale image is enabled property changed].
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="args">
		///     The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event
		///     data.
		/// </param>
		private static void OnAutoGreyScaleImageIsEnabledPropertyChanged(DependencyObject source,
			DependencyPropertyChangedEventArgs args)
		{
			var autoGreyScaleImg = source as Bitmap;
			bool isEnable = Convert.ToBoolean(args.NewValue);

			if (autoGreyScaleImg != null && autoGreyScaleImg.Source != null)
			{
				if (!isEnable)
				{
					// Get the source bitmap
					var bitmapImage = new BitmapImage(new Uri(autoGreyScaleImg.Source.ToString()));

					// Convert it to Gray
					autoGreyScaleImg.Source = new FormatConvertedBitmap(bitmapImage, PixelFormats.Gray32Float, null, 0);

					// Create Opacity Mask for greyscale image as FormatConvertedBitmap does not keep transparency info
					autoGreyScaleImg.OpacityMask = new ImageBrush(bitmapImage);
					autoGreyScaleImg.OpacityMask.Opacity = .6;
				}
				else
				{
					// Set the Source property to the original value.
					/* CHANGED TO THIS B/C OF CASTING EXCEPTION (by Todd) */
					var converted = autoGreyScaleImg.Source as FormatConvertedBitmap;
					if (converted != null)
					{
						autoGreyScaleImg.Source = converted.Source;
					}
					/* OLD CODE THAT PRODUCES CASTING EXCEPTION */
					//autoGreyScaleImg.Source = ((FormatConvertedBitmap)autoGreyScaleImg.Source).Source;

					// Reset the Opcity Mask
					autoGreyScaleImg.OpacityMask = null;
				}
			}
		}

		public event EventHandler<ExceptionEventArgs> BitmapFailed;

		// Return our measure size to be the size needed to display the bitmap pixels.
		protected override Size MeasureOverride(Size availableSize)
		{
			var measureSize = new Size();

			BitmapSource bitmapSource = Source;
			if (bitmapSource != null)
			{
				PresentationSource ps = PresentationSource.FromVisual(this);
				if (ps != null)
				{
					Matrix fromDevice = ps.CompositionTarget.TransformFromDevice;

					var pixelSize = new Vector(bitmapSource.PixelWidth, bitmapSource.PixelHeight);
					Vector measureSizeV = fromDevice.Transform(pixelSize);
					measureSize = new Size(measureSizeV.X, measureSizeV.Y);
				}
			}

			return measureSize;
		}

		protected override void OnRender(DrawingContext dc)
		{
			BitmapSource bitmapSource = Source;
			if (bitmapSource != null)
			{
				_pixelOffset = GetPixelOffset();

				// Render the bitmap offset by the needed amount to align to pixels.
				dc.DrawImage(bitmapSource, new Rect(_pixelOffset, DesiredSize));
			}
		}

		private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var bitmap = (Bitmap) d;

			var oldValue = (BitmapSource) e.OldValue;
			var newValue = (BitmapSource) e.NewValue;

			if (((oldValue != null) && (bitmap._sourceDownloaded != null)) && (!oldValue.IsFrozen && (oldValue is BitmapSource)))
			{
				oldValue.DownloadCompleted -= bitmap._sourceDownloaded;
				oldValue.DownloadFailed -= bitmap._sourceFailed;
				// ((BitmapSource)newValue).DecodeFailed -= bitmap._sourceFailed; // 3.5
			}
			if (((newValue != null) && (newValue is BitmapSource)) && !newValue.IsFrozen)
			{
				newValue.DownloadCompleted += bitmap._sourceDownloaded;
				newValue.DownloadFailed += bitmap._sourceFailed;
				// ((BitmapSource)newValue).DecodeFailed += bitmap._sourceFailed; // 3.5
			}
		}

		private void OnSourceDownloaded(object sender, EventArgs e)
		{
			InvalidateMeasure();
			InvalidateVisual();
		}

		private void OnSourceFailed(object sender, ExceptionEventArgs e)
		{
			Source = null; // setting a local value seems scetchy...

			BitmapFailed(this, e);
		}

		private void OnLayoutUpdated(object sender, EventArgs e)
		{
			// This event just means that layout happened somewhere.  However, this is
			// what we need since layout anywhere could affect our pixel positioning.
			Point pixelOffset = GetPixelOffset();
			if (!AreClose(pixelOffset, _pixelOffset))
			{
				InvalidateVisual();
			}
		}

		// Gets the matrix that will convert a point from "above" the
		// coordinate space of a visual into the the coordinate space
		// "below" the visual.
		private Matrix GetVisualTransform(Visual v)
		{
			if (v != null)
			{
				Matrix m = Matrix.Identity;

				Transform transform = VisualTreeHelper.GetTransform(v);
				if (transform != null)
				{
					Matrix cm = transform.Value;
					m = Matrix.Multiply(m, cm);
				}

				Vector offset = VisualTreeHelper.GetOffset(v);
				m.Translate(offset.X, offset.Y);

				return m;
			}

			return Matrix.Identity;
		}

		private Point TryApplyVisualTransform(Point point, Visual v, bool inverse, bool throwOnError, out bool success)
		{
			success = true;
			if (v != null)
			{
				Matrix visualTransform = GetVisualTransform(v);
				if (inverse)
				{
					if (!throwOnError && !visualTransform.HasInverse)
					{
						success = false;
						return new Point(0, 0);
					}
					visualTransform.Invert();
				}
				point = visualTransform.Transform(point);
			}
			return point;
		}

		private Point ApplyVisualTransform(Point point, Visual v, bool inverse)
		{
			bool success = true;
			return TryApplyVisualTransform(point, v, inverse, true, out success);
		}

		private Point GetPixelOffset()
		{
			var pixelOffset = new Point();

			PresentationSource ps = PresentationSource.FromVisual(this);
			if (ps != null)
			{
				Visual rootVisual = ps.RootVisual;

				// Transform (0,0) from this element up to pixels.
				pixelOffset = TransformToAncestor(rootVisual).Transform(pixelOffset);
				pixelOffset = ApplyVisualTransform(pixelOffset, rootVisual, false);
				pixelOffset = ps.CompositionTarget.TransformToDevice.Transform(pixelOffset);

				// Round the origin to the nearest whole pixel.
				pixelOffset.X = Math.Round(pixelOffset.X);
				pixelOffset.Y = Math.Round(pixelOffset.Y);

				// Transform the whole-pixel back to this element.
				pixelOffset = ps.CompositionTarget.TransformFromDevice.Transform(pixelOffset);
				pixelOffset = ApplyVisualTransform(pixelOffset, rootVisual, true);
				pixelOffset = rootVisual.TransformToDescendant(this).Transform(pixelOffset);
			}

			return pixelOffset;
		}

		private bool AreClose(Point point1, Point point2)
		{
			return AreClose(point1.X, point2.X) && AreClose(point1.Y, point2.Y);
		}

		private bool AreClose(double value1, double value2)
		{
			if (value1 == value2)
			{
				return true;
			}
			double delta = value1 - value2;
			return ((delta < .5) && (delta > -.5));
		}
	}
}