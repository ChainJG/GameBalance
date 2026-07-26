using MaterialDesignThemes.Wpf;
using System.Windows;

namespace GameBalance.Framework.Controls
{
    /// <summary>
    /// An interactive button that displays a Material Design icon.
    /// Provides smooth scale animations on hover and press states.
    /// </summary>
    public class InteractiveIconButton : InteractiveCard
    {
        #region Constructor & Static Setup

        static InteractiveIconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(InteractiveIconButton),
                new FrameworkPropertyMetadata(typeof(InteractiveIconButton)));
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        /// Gets or sets the Material Design icon kind to display.
        /// </summary>
        public static readonly DependencyProperty IconKindProperty =
            DependencyProperty.Register(
                nameof(IconKind),
                typeof(PackIconKind),
                typeof(InteractiveIconButton),
                new PropertyMetadata(PackIconKind.Help));

        public PackIconKind IconKind
        {
            get => (PackIconKind)GetValue(IconKindProperty);
            set => SetValue(IconKindProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the icon in pixels.
        /// </summary>
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register(
                nameof(IconSize),
                typeof(double),
                typeof(InteractiveIconButton),
                new PropertyMetadata(16d));

        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the icon scales on hover.
        /// </summary>
        public static readonly DependencyProperty ScaleOnHoverProperty =
            DependencyProperty.Register(
                nameof(ScaleOnHover),
                typeof(bool),
                typeof(InteractiveIconButton),
                new PropertyMetadata(true));

        public bool ScaleOnHover
        {
            get => (bool)GetValue(ScaleOnHoverProperty);
            set => SetValue(ScaleOnHoverProperty, value);
        }

        /// <summary>
        /// Gets or sets the scale factor on hover.
        /// </summary>
        public static readonly DependencyProperty HoverScaleFactorProperty =
            DependencyProperty.Register(
                nameof(HoverScaleFactor),
                typeof(double),
                typeof(InteractiveIconButton),
                new PropertyMetadata(1.1));

        public double HoverScaleFactor
        {
            get => (double)GetValue(HoverScaleFactorProperty);
            set => SetValue(HoverScaleFactorProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the icon scales on press.
        /// </summary>
        public static readonly DependencyProperty ScaleOnPressProperty =
            DependencyProperty.Register(
                nameof(ScaleOnPress),
                typeof(bool),
                typeof(InteractiveIconButton),
                new PropertyMetadata(true));

        public bool ScaleOnPress
        {
            get => (bool)GetValue(ScaleOnPressProperty);
            set => SetValue(ScaleOnPressProperty, value);
        }

        /// <summary>
        /// Gets or sets the scale factor on press.
        /// </summary>
        public static readonly DependencyProperty PressScaleFactorProperty =
            DependencyProperty.Register(
                nameof(PressScaleFactor),
                typeof(double),
                typeof(InteractiveIconButton),
                new PropertyMetadata(0.85));

        public double PressScaleFactor
        {
            get => (double)GetValue(PressScaleFactorProperty);
            set => SetValue(PressScaleFactorProperty, value);
        }

        #endregion
    }
}