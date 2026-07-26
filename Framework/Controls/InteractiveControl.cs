using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GameBalance.Framework.Controls
{
    /// <summary>
    /// Base class for interactive UI elements with visual state management,
    /// dynamic shadow effects, and smooth animations.
    /// </summary>
    public abstract class InteractiveControl : ButtonBase
    {
        #region Constructor & Static Setup

        static InteractiveControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(InteractiveControl),
                new FrameworkPropertyMetadata(typeof(InteractiveControl)));
        }

        protected InteractiveControl()
        {
            Loaded += OnLoaded;

            ToolTipService.SetShowDuration(this, 5000);
            ToolTipService.SetInitialShowDelay(this, 500);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateVisualState(useAnimation: false);
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty IsInteractiveProperty =
            DependencyProperty.Register(
                nameof(IsInteractive),
                typeof(bool),
                typeof(InteractiveControl),
                new FrameworkPropertyMetadata(true, OnVisualStatePropertyChanged));

        public bool IsInteractive
        {
            get => (bool)GetValue(IsInteractiveProperty);
            set => SetValue(IsInteractiveProperty, value);
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                nameof(IsSelected),
                typeof(bool),
                typeof(InteractiveControl),
                new FrameworkPropertyMetadata(false, OnVisualStatePropertyChanged));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly DependencyProperty AreAnimationsEnabledProperty =
            DependencyProperty.Register(
                nameof(AreAnimationsEnabled),
                typeof(bool),
                typeof(InteractiveControl),
                new PropertyMetadata(true));

        public bool AreAnimationsEnabled
        {
            get => (bool)GetValue(AreAnimationsEnabledProperty);
            set => SetValue(AreAnimationsEnabledProperty, value);
        }

        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register(
                nameof(AnimationDuration),
                typeof(Duration),
                typeof(InteractiveControl),
                new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(200))));

        public Duration AnimationDuration
        {
            get => (Duration)GetValue(AnimationDurationProperty);
            set => SetValue(AnimationDurationProperty, value);
        }

        public static readonly DependencyProperty HasShadowProperty =
            DependencyProperty.Register(
                nameof(HasShadow),
                typeof(bool),
                typeof(InteractiveControl),
                new PropertyMetadata(true, OnVisualStatePropertyChanged));

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        public static readonly DependencyProperty BaseShadowDepthProperty =
            DependencyProperty.Register(
                nameof(BaseShadowDepth),
                typeof(double),
                typeof(InteractiveControl),
                new PropertyMetadata(5.0, OnVisualStatePropertyChanged));

        public double BaseShadowDepth
        {
            get => (double)GetValue(BaseShadowDepthProperty);
            set => SetValue(BaseShadowDepthProperty, value);
        }

        public static readonly DependencyProperty BaseShadowOpacityProperty =
            DependencyProperty.Register(
                nameof(BaseShadowOpacity),
                typeof(double),
                typeof(InteractiveControl),
                new PropertyMetadata(0.3, OnVisualStatePropertyChanged));

        public double BaseShadowOpacity
        {
            get => (double)GetValue(BaseShadowOpacityProperty);
            set => SetValue(BaseShadowOpacityProperty, value);
        }

        public static readonly DependencyProperty ShadowMatchesBackgroundProperty =
            DependencyProperty.Register(
                nameof(ShadowMatchesBackground),
                typeof(bool),
                typeof(InteractiveControl),
                new PropertyMetadata(true, OnVisualStatePropertyChanged));

        public bool ShadowMatchesBackground
        {
            get => (bool)GetValue(ShadowMatchesBackgroundProperty);
            set => SetValue(ShadowMatchesBackgroundProperty, value);
        }

        public static readonly DependencyProperty ToolTipTextProperty =
            DependencyProperty.Register(
                nameof(ToolTipText),
                typeof(string),
                typeof(InteractiveControl),
                new PropertyMetadata(null, OnToolTipTextChanged));

        public string? ToolTipText
        {
            get => (string?)GetValue(ToolTipTextProperty);
            set => SetValue(ToolTipTextProperty, value);
        }

        private static void OnToolTipTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is InteractiveControl control)
            {
                control.ToolTip = e.NewValue as string;
            }
        }

        #endregion

        #region Current Visual State Properties (Read-Only)

        private static readonly DependencyPropertyKey CurrentBackgroundPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(CurrentBackground),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata());

        public static readonly DependencyProperty CurrentBackgroundProperty =
            CurrentBackgroundPropertyKey.DependencyProperty;

        public Brush CurrentBackground
        {
            get => (Brush)GetValue(CurrentBackgroundProperty);
            private set => SetValue(CurrentBackgroundPropertyKey, value);
        }

        private static readonly DependencyPropertyKey CurrentBorderPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(CurrentBorder),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata());

        public static readonly DependencyProperty CurrentBorderProperty =
            CurrentBorderPropertyKey.DependencyProperty;

        public Brush CurrentBorder
        {
            get => (Brush)GetValue(CurrentBorderProperty);
            private set => SetValue(CurrentBorderPropertyKey, value);
        }

        private static readonly DependencyPropertyKey CurrentElevationPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(CurrentElevation),
                typeof(int),
                typeof(InteractiveControl),
                new PropertyMetadata(0));

        public static readonly DependencyProperty CurrentElevationProperty =
            CurrentElevationPropertyKey.DependencyProperty;

        public int CurrentElevation
        {
            get => (int)GetValue(CurrentElevationProperty);
            private set => SetValue(CurrentElevationPropertyKey, value);
        }

        #endregion

        #region Background Brush Properties

        public static readonly DependencyProperty NormalBackgroundProperty =
            DependencyProperty.Register(
                nameof(NormalBackground),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata(null, OnVisualStatePropertyChanged));

        public Brush NormalBackground
        {
            get => (Brush)GetValue(NormalBackgroundProperty);
            set => SetValue(NormalBackgroundProperty, value);
        }

        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register(
                nameof(HoverBackground),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata(null, OnVisualStatePropertyChanged));

        public Brush HoverBackground
        {
            get => (Brush)GetValue(HoverBackgroundProperty);
            set => SetValue(HoverBackgroundProperty, value);
        }

        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register(
                nameof(PressedBackground),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata(null, OnVisualStatePropertyChanged));

        public Brush PressedBackground
        {
            get => (Brush)GetValue(PressedBackgroundProperty);
            set => SetValue(PressedBackgroundProperty, value);
        }

        public static readonly DependencyProperty SelectedBackgroundProperty =
            DependencyProperty.Register(
                nameof(SelectedBackground),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata(null, OnVisualStatePropertyChanged));

        public Brush SelectedBackground
        {
            get => (Brush)GetValue(SelectedBackgroundProperty);
            set => SetValue(SelectedBackgroundProperty, value);
        }

        public static readonly DependencyProperty FocusedBackgroundProperty =
            DependencyProperty.Register(
                nameof(FocusedBackground),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata(null, OnVisualStatePropertyChanged));

        public Brush FocusedBackground
        {
            get => (Brush)GetValue(FocusedBackgroundProperty);
            set => SetValue(FocusedBackgroundProperty, value);
        }

        public static readonly DependencyProperty DisabledBackgroundProperty =
            DependencyProperty.Register(
                nameof(DisabledBackground),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata(null, OnVisualStatePropertyChanged));

        public Brush DisabledBackground
        {
            get => (Brush)GetValue(DisabledBackgroundProperty);
            set => SetValue(DisabledBackgroundProperty, value);
        }

        #endregion

        #region Border Brush Properties

        public static readonly DependencyProperty NormalBorderProperty =
            DependencyProperty.Register(
                nameof(NormalBorder),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata(null, OnVisualStatePropertyChanged));

        public Brush NormalBorder
        {
            get => (Brush)GetValue(NormalBorderProperty);
            set => SetValue(NormalBorderProperty, value);
        }

        public static readonly DependencyProperty HoverBorderProperty =
            DependencyProperty.Register(
                nameof(HoverBorder),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata(null, OnVisualStatePropertyChanged));

        public Brush HoverBorder
        {
            get => (Brush)GetValue(HoverBorderProperty);
            set => SetValue(HoverBorderProperty, value);
        }

        public static readonly DependencyProperty PressedBorderProperty =
            DependencyProperty.Register(
                nameof(PressedBorder),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata(null, OnVisualStatePropertyChanged));

        public Brush PressedBorder
        {
            get => (Brush)GetValue(PressedBorderProperty);
            set => SetValue(PressedBorderProperty, value);
        }

        public static readonly DependencyProperty SelectedBorderProperty =
            DependencyProperty.Register(
                nameof(SelectedBorder),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata(null, OnVisualStatePropertyChanged));

        public Brush SelectedBorder
        {
            get => (Brush)GetValue(SelectedBorderProperty);
            set => SetValue(SelectedBorderProperty, value);
        }

        public static readonly DependencyProperty FocusedBorderProperty =
            DependencyProperty.Register(
                nameof(FocusedBorder),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata(null, OnVisualStatePropertyChanged));

        public Brush FocusedBorder
        {
            get => (Brush)GetValue(FocusedBorderProperty);
            set => SetValue(FocusedBorderProperty, value);
        }

        public static readonly DependencyProperty DisabledBorderProperty =
            DependencyProperty.Register(
                nameof(DisabledBorder),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata(null, OnVisualStatePropertyChanged));

        public Brush DisabledBorder
        {
            get => (Brush)GetValue(DisabledBorderProperty);
            set => SetValue(DisabledBorderProperty, value);
        }

        #endregion

        #region Elevation Properties

        public static readonly DependencyProperty NormalElevationProperty =
            DependencyProperty.Register(
                nameof(NormalElevation),
                typeof(int),
                typeof(InteractiveControl),
                new PropertyMetadata(0, OnVisualStatePropertyChanged));

        public int NormalElevation
        {
            get => (int)GetValue(NormalElevationProperty);
            set => SetValue(NormalElevationProperty, value);
        }

        public static readonly DependencyProperty HoverElevationProperty =
            DependencyProperty.Register(
                nameof(HoverElevation),
                typeof(int),
                typeof(InteractiveControl),
                new PropertyMetadata(4, OnVisualStatePropertyChanged));

        public int HoverElevation
        {
            get => (int)GetValue(HoverElevationProperty);
            set => SetValue(HoverElevationProperty, value);
        }

        public static readonly DependencyProperty PressedElevationProperty =
            DependencyProperty.Register(
                nameof(PressedElevation),
                typeof(int),
                typeof(InteractiveControl),
                new PropertyMetadata(8, OnVisualStatePropertyChanged));

        public int PressedElevation
        {
            get => (int)GetValue(PressedElevationProperty);
            set => SetValue(PressedElevationProperty, value);
        }

        public static readonly DependencyProperty SelectedElevationProperty =
            DependencyProperty.Register(
                nameof(SelectedElevation),
                typeof(int),
                typeof(InteractiveControl),
                new PropertyMetadata(2, OnVisualStatePropertyChanged));

        public int SelectedElevation
        {
            get => (int)GetValue(SelectedElevationProperty);
            set => SetValue(SelectedElevationProperty, value);
        }

        public static readonly DependencyProperty FocusedElevationProperty =
            DependencyProperty.Register(
                nameof(FocusedElevation),
                typeof(int),
                typeof(InteractiveControl),
                new PropertyMetadata(2, OnVisualStatePropertyChanged));

        public int FocusedElevation
        {
            get => (int)GetValue(FocusedElevationProperty);
            set => SetValue(FocusedElevationProperty, value);
        }

        #endregion

        #region Event Overrides

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            UpdateVisualState();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            UpdateVisualState();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            UpdateVisualState();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            UpdateVisualState();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            UpdateVisualState();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            UpdateVisualState();
        }

        #endregion

        #region Visual State Logic

        private static void OnVisualStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is InteractiveControl control)
            {
                control.UpdateVisualState();
            }
        }

        protected virtual void UpdateVisualState(bool useAnimation = true)
        {
            if (!AreAnimationsEnabled)
            {
                useAnimation = false;
            }

            var state = DetermineCurrentState();
            ApplyState(state, useAnimation);
        }

        protected virtual VisualState DetermineCurrentState()
        {
            if (!IsEnabled)
                return VisualState.Disabled;

            if (!IsInteractive)
                return VisualState.Normal;

            if (IsPressed)
                return VisualState.Pressed;

            if (IsMouseOver)
                return VisualState.Hover;

            if (IsFocused && !IsMouseOver)
                return VisualState.Focused;

            if (IsSelected)
                return VisualState.Selected;

            return VisualState.Normal;
        }

        protected virtual void ApplyState(VisualState state, bool animate = true)
        {
            var (background, border, elevation) = GetStateBrushes(state);

            if (animate && AreAnimationsEnabled)
            {
                AnimateProperty(CurrentBackgroundProperty, background);
                AnimateProperty(CurrentBorderProperty, border);
                AnimateElevation(elevation);
            }
            else
            {
                CurrentBackground = background ?? NormalBackground;
                CurrentBorder = border ?? NormalBorder;
                CurrentElevation = elevation;
            }

            Cursor = state == VisualState.Disabled || state == VisualState.Normal
                ? Cursors.Arrow
                : Cursors.Hand;
        }

        protected virtual (Brush? Background, Brush? Border, int Elevation) GetStateBrushes(VisualState state)
        {
            return state switch
            {
                VisualState.Normal => (NormalBackground, NormalBorder, NormalElevation),
                VisualState.Hover => (HoverBackground ?? NormalBackground, HoverBorder ?? NormalBorder, HoverElevation),
                VisualState.Pressed => (PressedBackground ?? HoverBackground ?? NormalBackground, PressedBorder ?? HoverBorder ?? NormalBorder, PressedElevation),
                VisualState.Selected => (SelectedBackground ?? NormalBackground, SelectedBorder ?? NormalBorder, SelectedElevation),
                VisualState.Focused => (FocusedBackground ?? NormalBackground, FocusedBorder ?? NormalBorder, FocusedElevation),
                VisualState.Disabled => (DisabledBackground ?? NormalBackground, DisabledBorder ?? NormalBorder, 0),
                _ => (NormalBackground, NormalBorder, NormalElevation)
            };
        }

        #endregion

        #region Animation Helpers

        protected void AnimateProperty(DependencyProperty property, object? targetValue)
        {
            if (targetValue is not Brush brush)
                return;

            var animation = new BrushAnimation
            {
                To = brush,
                Duration = AnimationDuration,
            };

            BeginAnimation(property, animation);
        }

        protected void AnimateElevation(int targetElevation)
        {
            var currentElevation = CurrentElevation;

            if (currentElevation == targetElevation)
                return;

            var animation = new Int32Animation
            {
                To = targetElevation,
                Duration = AnimationDuration,
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            BeginAnimation(CurrentElevationProperty, animation);
        }

        #endregion
    }

    public enum VisualState
    {
        Normal,
        Hover,
        Pressed,
        Selected,
        Focused,
        Disabled
    }

    public class BrushAnimation : AnimationTimeline
    {
        public override Type TargetPropertyType => typeof(Brush);

        public Brush? From { get; set; }
        public Brush? To { get; set; }

        public override object? GetCurrentValue(object? defaultOriginValue, object? defaultDestinationValue, AnimationClock animationClock)
        {
            return To ?? defaultDestinationValue;
        }

        protected override Freezable CreateInstanceCore()
        {
            return new BrushAnimation();
        }
    }
}