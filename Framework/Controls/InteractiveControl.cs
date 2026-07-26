using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace GameBalance.Framework.Controls
{
    public abstract class InteractiveControl : ButtonBase
    {
        static InteractiveControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(InteractiveControl),
                new FrameworkPropertyMetadata(typeof(InteractiveControl)));
        }

        public InteractiveControl()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateVisualState();
        }

        public bool IsInteractive
        {
            get => (bool)GetValue(IsInteractiveProperty);
            set => SetValue(IsInteractiveProperty, value);
        }

        public static readonly DependencyProperty IsInteractiveProperty =
            DependencyProperty.Register(
                nameof(IsInteractive),
                typeof(bool),
                typeof(InteractiveControl),
                new FrameworkPropertyMetadata(true, OnVisualPropertyChanged));


        #region IsSelected
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                nameof(IsSelected),
                typeof(bool),
                typeof(InteractiveControl),
                new PropertyMetadata(false, OnVisualPropertyChanged));
        #endregion

        #region Current Visual Values

        public Brush CurrentBackground

        {
            get => (Brush)GetValue(CurrentBackgroundProperty);
            private set => SetValue(CurrentBackgroundPropertyKey, value);
        }

        private static readonly DependencyPropertyKey CurrentBackgroundPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(CurrentBackground),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata());

        public Brush CurrentBorder
        {
            get => (Brush)GetValue(CurrentBorderProperty);
            private set => SetValue(CurrentBorderPropertyKey, value);
        }


        private static readonly DependencyPropertyKey CurrentBorderPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(CurrentBorder),
                typeof(Brush),
                typeof(InteractiveControl),
                new PropertyMetadata());


        public static readonly DependencyProperty CurrentBorderProperty =
            CurrentBorderPropertyKey.DependencyProperty;

        public static readonly DependencyProperty CurrentBackgroundProperty =
            CurrentBackgroundPropertyKey.DependencyProperty;
        #endregion

        #region Background Colours

        public Brush NormalBackground
        {
            get => (Brush)GetValue(NormalBackgroundProperty);
            set => SetValue(NormalBackgroundProperty, value);
        }
        public static readonly DependencyProperty NormalBackgroundProperty =
            DependencyProperty.Register(
                nameof(NormalBackground),
                typeof(Brush),
                typeof(InteractiveControl));


        public Brush HoverBackground
        {
            get => (Brush)GetValue(HoverBackgroundProperty);
            set => SetValue(HoverBackgroundProperty, value);
        }
        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register(
                nameof(HoverBackground),
                typeof(Brush),
                typeof(InteractiveControl));


        public Brush PressedBackground
        {
            get => (Brush)GetValue(PressedBackgroundProperty);
            set => SetValue(PressedBackgroundProperty, value);
        }
        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register(
                nameof(PressedBackground),
                typeof(Brush),
                typeof(InteractiveControl));


        public Brush SelectedBackground
        {
            get => (Brush)GetValue(SelectedBackgroundProperty);
            set => SetValue(SelectedBackgroundProperty, value);
        }
        public static readonly DependencyProperty SelectedBackgroundProperty =
            DependencyProperty.Register(
                nameof(SelectedBackground),
                typeof(Brush),
                typeof(InteractiveControl));


        public Brush DisabledBackground
        {
            get => (Brush)GetValue(DisabledBackgroundProperty);
            set => SetValue(DisabledBackgroundProperty, value);
        }
        public static readonly DependencyProperty DisabledBackgroundProperty =
            DependencyProperty.Register(
                nameof(DisabledBackground),
                typeof(Brush),
                typeof(InteractiveControl));
        #endregion

        #region Border Colours
        public Brush NormalBorder
        {
            get => (Brush)GetValue(NormalBorderProperty);
            set => SetValue(NormalBorderProperty, value);
        }
        public static readonly DependencyProperty NormalBorderProperty =
            DependencyProperty.Register(
                nameof(NormalBorder),
                typeof(Brush),
                typeof(InteractiveControl));


        public Brush HoverBorder
        {
            get => (Brush)GetValue(HoverBorderProperty);
            set => SetValue(HoverBorderProperty, value);
        }
        public static readonly DependencyProperty HoverBorderProperty =
            DependencyProperty.Register(
                nameof(HoverBorder),
                typeof(Brush),
                typeof(InteractiveControl));


        public Brush PressedBorder
        {
            get => (Brush)GetValue(PressedBorderProperty);
            set => SetValue(PressedBorderProperty, value);
        }
        public static readonly DependencyProperty PressedBorderProperty =
            DependencyProperty.Register(
                nameof(PressedBorder),
                typeof(Brush),
                typeof(InteractiveControl));

        public Brush SelectedBorder
        {
            get => (Brush)GetValue(SelectedBorderProperty);
            set => SetValue(SelectedBorderProperty, value);
        }
        public static readonly DependencyProperty SelectedBorderProperty =
            DependencyProperty.Register(
                nameof(SelectedBorder),
                typeof(Brush),
                typeof(InteractiveControl));


        public Brush DisabledBorder
        {
            get => (Brush)GetValue(DisabledBorderProperty);
            set => SetValue(DisabledBorderProperty, value);
        }
        public static readonly DependencyProperty DisabledBorderProperty =
            DependencyProperty.Register(
                nameof(DisabledBorder),
                typeof(Brush),
                typeof(InteractiveControl));
        #endregion

        #region Overrides
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


        #endregion

        #region Visual Logic
        private void UpdateVisualState()
        {
            if (!IsEnabled)
            {
                CurrentBackground = DisabledBackground;
                CurrentBorder = NormalBorder;
                Cursor = Cursors.Arrow;

                return;
            }

            Cursor = Cursors.Hand;

            if (!IsInteractive)
            {
                CurrentBackground = NormalBackground;
                CurrentBorder = NormalBorder;
                return;
            }

            if (IsPressed)
            {
                CurrentBackground = PressedBackground;
                CurrentBorder = PressedBorder;
                return;
            }

            if (IsMouseOver)
            {
                CurrentBackground = HoverBackground;
                CurrentBorder = HoverBorder;
                return;
            }

            if (IsSelected)
            {
                CurrentBackground = SelectedBackground;
                CurrentBorder = SelectedBorder;
                return;
            }

            CurrentBackground = NormalBackground;
            CurrentBorder = NormalBorder;
        }

        private static void OnVisualPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is InteractiveControl card)
            {
                card.UpdateVisualState();
            }
        }
        #endregion
    }
}