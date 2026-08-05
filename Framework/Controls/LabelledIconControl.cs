using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameBalance.Framework.Controls
{
    public class LabelledIconControl : Control
    {
        #region Constructor
        static LabelledIconControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(LabelledIconControl),
                new FrameworkPropertyMetadata(typeof(LabelledIconControl)));
        }
        #endregion

        #region Icon Properties
        public PackIconKind IconKind
        {
            get => (PackIconKind)GetValue(IconKindProperty);
            set => SetValue(IconKindProperty, value);
        }
        public static readonly DependencyProperty IconKindProperty =
            DependencyProperty.Register(
                nameof(IconKind),
                typeof(PackIconKind),
                typeof(LabelledIconControl),
                new PropertyMetadata(PackIconKind.None));


        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register(
                nameof(IconSize),
                typeof(double),
                typeof(LabelledIconControl),
                new PropertyMetadata(16d));

        public Brush IconForeground
        {
            get => (Brush)GetValue(IconForegroundProperty);
            set => SetValue(IconForegroundProperty, value);
        }
        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.Register(
                nameof(IconForeground),
                typeof(Brush),
                typeof(LabelledIconControl));

        public Style? TextStyle
        {
            get => (Style?)GetValue(TextStyleProperty);
            set => SetValue(TextStyleProperty, value);
        }

        public static readonly DependencyProperty TextStyleProperty =
            DependencyProperty.Register(
                nameof(TextStyle),
                typeof(Style),
                typeof(LabelledIconControl));
        #endregion

        #region Text Properties
        public string? Text
        {
            get => (string?)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(LabelledIconControl));

        public Brush TextForeground
        {
            get => (Brush)GetValue(TextForegroundProperty);
            set => SetValue(TextForegroundProperty, value);
        }

        public static readonly DependencyProperty TextForegroundProperty =
            DependencyProperty.Register(
                nameof(TextForeground),
                typeof(Brush),
                typeof(LabelledIconControl));
        #endregion

        #region Layout Properties
        public double Spacing
        {
            get => (double)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register(
                nameof(Spacing),
                typeof(double),
                typeof(LabelledIconControl),
                new PropertyMetadata(6d));
        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(
                nameof(Orientation),
                typeof(Orientation),
                typeof(LabelledIconControl),
                new PropertyMetadata(Orientation.Horizontal)); 
        #endregion
    }
}
