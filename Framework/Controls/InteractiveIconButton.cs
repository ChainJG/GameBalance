using MaterialDesignThemes.Wpf;
using System.Windows;

namespace GameBalance.Framework.Controls
{
    public class InteractiveIconButton : InteractiveCard
    {
        static InteractiveIconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(InteractiveIconButton),
                new FrameworkPropertyMetadata(typeof(InteractiveIconButton)));
        }

        public PackIconKind IconKind
        {
            get => (PackIconKind)GetValue(IconKindProperty);
            set => SetValue(IconKindProperty, value);
        }


        public static readonly DependencyProperty IconKindProperty =
            DependencyProperty.Register(
                nameof(IconKind),
                typeof(PackIconKind),
                typeof(InteractiveIconButton),
                new PropertyMetadata(PackIconKind.Help));


        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register(
                nameof(IconSize),
                typeof(double),
                typeof(InteractiveIconButton),
                new PropertyMetadata(16d));
    }
}
