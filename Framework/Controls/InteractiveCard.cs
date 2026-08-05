using System.Windows;

namespace GameBalance.Framework.Controls
{
    public class InteractiveCard : InteractiveControl
    {
        static InteractiveCard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(InteractiveCard),
                new FrameworkPropertyMetadata(typeof(InteractiveCard)));
        }

        #region Corner Radius

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(
                nameof(CornerRadius),
                typeof(CornerRadius),
                typeof(InteractiveCard),
                new PropertyMetadata(new CornerRadius(8)));

        #endregion

        #region Padding

        public Thickness CardPadding
        {
            get => (Thickness)GetValue(CardPaddingProperty);
            set => SetValue(CardPaddingProperty, value);
        }


        public static readonly DependencyProperty CardPaddingProperty =
            DependencyProperty.Register(
                nameof(CardPadding),
                typeof(Thickness),
                typeof(InteractiveCard),
                new PropertyMetadata(new Thickness(12)));

        #endregion
    }
}