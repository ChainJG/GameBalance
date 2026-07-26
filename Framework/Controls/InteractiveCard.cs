using System.Windows;

namespace GameBalance.Framework.Controls
{
    /// <summary>
    /// An interactive card control with configurable corner radius and padding.
    /// Inherits visual state management from InteractiveControl.
    /// </summary>
    public class InteractiveCard : InteractiveControl
    {
        #region Constructor & Static Setup

        static InteractiveCard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(InteractiveCard),
                new FrameworkPropertyMetadata(typeof(InteractiveCard)));
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        /// Gets or sets the corner radius of the card.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(
                nameof(CornerRadius),
                typeof(CornerRadius),
                typeof(InteractiveCard),
                new PropertyMetadata(new CornerRadius(8)));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Gets or sets the padding inside the card.
        /// </summary>
        public static readonly DependencyProperty CardPaddingProperty =
            DependencyProperty.Register(
                nameof(CardPadding),
                typeof(Thickness),
                typeof(InteractiveCard),
                new PropertyMetadata(new Thickness(12)));

        public Thickness CardPadding
        {
            get => (Thickness)GetValue(CardPaddingProperty);
            set => SetValue(CardPaddingProperty, value);
        }

        #endregion
    }
}