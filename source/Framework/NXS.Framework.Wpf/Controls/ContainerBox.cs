using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NXS.Framework.Wpf.Controls
{
	[TemplatePart(Name = "PART_Header", Type = typeof(Border))]
	[TemplatePart(Name = "PART_Body", Type = typeof(Border))]
	public class ContainerBox : GroupBox
	{
		#region Events

		public event MouseButtonEventHandler HeaderMouseLeftButtonDown;
		public event MouseButtonEventHandler HeaderMouseLeftButtonUp;

		#endregion Events

		#region Properties

		#region Dependency

		public static DependencyProperty HeaderBackgroundProperty = DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(ContainerBox));
		public static DependencyProperty HeaderForegroundProperty = DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(ContainerBox));
		public static DependencyProperty HeaderFontWeightProperty = DependencyProperty.Register("HeaderFontWeight", typeof(FontWeight), typeof(ContainerBox));
		public static DependencyProperty HeaderFontSizeProperty = DependencyProperty.Register("HeaderFontSize", typeof(double), typeof(ContainerBox));
		public static DependencyProperty HeaderFontFamilyProperty = DependencyProperty.Register("HeaderFontFamily", typeof(FontFamily), typeof(ContainerBox));
		public static DependencyProperty HeaderFontStyleProperty = DependencyProperty.Register("HeaderFontStyle", typeof(FontStyle), typeof(ContainerBox));
		public static DependencyProperty HeaderPaddingProperty = DependencyProperty.Register("HeaderPadding", typeof(Thickness), typeof(ContainerBox));
		public static DependencyProperty HeaderMarginProperty = DependencyProperty.Register("HeaderMargin", typeof(Thickness), typeof(ContainerBox));

		#endregion Dependency

		#region Private

		private Border HeaderBorder;
		private Border BodyBorder;

		#endregion Private

		#region Public

		[Category("Brushes")]
		public Brush HeaderBackground
		{
			get { return GetValue(HeaderBackgroundProperty) as Brush; }
			set { SetValue(HeaderBackgroundProperty, value); }
		}

		[Category("Brushes")]
		public Brush HeaderForeground
		{
			get { return GetValue(HeaderForegroundProperty) as Brush; }
			set { SetValue(HeaderForegroundProperty, value); }
		}

		[Category("Header Text")]
		public FontWeight HeaderFontWeight
		{
			get { return (FontWeight)GetValue(HeaderFontWeightProperty); }
			set { SetValue(HeaderFontWeightProperty, value); }
		}

		[Category("Header Text")]
		public double HeaderFontSize
		{
			get { return (double)GetValue(HeaderFontSizeProperty); }
			set { SetValue(HeaderFontSizeProperty, value); }
		}

		[Category("Header Text")]
		public FontFamily HeaderFontFamily
		{
			get { return (FontFamily)GetValue(HeaderFontFamilyProperty); }
			set { SetValue(HeaderFontFamilyProperty, value); }
		}

		[Category("Header Text")]
		public FontStyle HeaderFontStyle
		{
			get { return (FontStyle)GetValue(HeaderFontStyleProperty); }
			set { SetValue(HeaderFontStyleProperty, value); }
		}

		[Category("Header Text")]
		public Thickness HeaderPadding
		{
			get { return (Thickness)GetValue(HeaderPaddingProperty); }
			set { SetValue(HeaderPaddingProperty, value); }
		}

		[Category("Header Text")]
		public Thickness HeaderMargin
		{
			get { return (Thickness)GetValue(HeaderMarginProperty); }
			set { SetValue(HeaderMarginProperty, value); }
		}

		#endregion Public

		#endregion Properties

		#region Constructors

		static ContainerBox()
		{
			// Override style
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ContainerBox), new FrameworkPropertyMetadata(typeof(ContainerBox)));
		}

		public ContainerBox()
			: base()
		{
		}

		#endregion Constructors

		#region Methods

		#region Public

		public override void OnApplyTemplate()
		{
			// Let the base class do it's work
			base.OnApplyTemplate();

			// Get templated controls
			HeaderBorder = GetTemplateChild("PART_Header") as Border;
			BodyBorder = GetTemplateChild("PART_Body") as Border;

			// Wire up needed events
			if (HeaderBorder != null)
			{
				HeaderBorder.MouseLeftButtonDown += new MouseButtonEventHandler(HeaderBorder_MouseLeftButtonDown);
				HeaderBorder.MouseLeftButtonUp += new MouseButtonEventHandler(HeaderBorder_MouseLeftButtonUp);
			}
		}

		#endregion Public

		#region Event Handlers

		private void HeaderBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (HeaderMouseLeftButtonDown != null)
				HeaderMouseLeftButtonDown(this, e);
		}

		private void HeaderBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (HeaderMouseLeftButtonUp != null)
				HeaderMouseLeftButtonUp(this, e);
		}

		#endregion Event Handlers

		#endregion Methods
	}
}