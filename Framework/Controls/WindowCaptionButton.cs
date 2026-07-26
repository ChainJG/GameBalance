using GameBalance.Framework.Core;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Media;

namespace GameBalance.Framework.Controls
{
    /// <summary>
    /// A specialized window caption button that handles minimize, maximize,
    /// and close actions with visual feedback.
    /// </summary>
    public class WindowCaptionButton : InteractiveIconButton
    {
        #region Constructor & Static Setup

        static WindowCaptionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(WindowCaptionButton),
                new FrameworkPropertyMetadata(typeof(WindowCaptionButton)));
        }

        public WindowCaptionButton()
        {
            Loaded += OnLoaded;
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        /// Gets or sets the window command to execute when clicked.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(WindowCommand),
                typeof(WindowCaptionButton),
                new PropertyMetadata(WindowCommand.None, OnCommandChanged));

        public WindowCommand Command
        {
            get => (WindowCommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WindowCaptionButton button)
            {
                button.UpdateCloseButtonAppearance();
            }
        }

        #endregion

        #region Close Button Appearance

        private void UpdateCloseButtonAppearance()
        {
            if (Command == WindowCommand.Close)
            {
                if (HoverBackground == null || HoverBackground == Brushes.Transparent)
                {
                    HoverBackground = FindResource("ErrorCardBackgroundBrush") as Brush
                                      ?? new SolidColorBrush(Color.FromRgb(232, 17, 35));
                }

                if (HoverBorder == null)
                {
                    HoverBorder = FindResource("ErrorCardBackgroundBrush") as Brush;
                }
            }
        }

        #endregion

        #region Event Handlers

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateIcon();

            var window = Window.GetWindow(this);

            if (window is not null)
            {
                window.StateChanged += OnWindowStateChanged;
                window.Closed += OnWindowClosed;
            }

            UpdateCloseButtonAppearance();
        }

        private void OnWindowClosed(object? sender, EventArgs e)
        {
            if (sender is Window window)
            {
                window.StateChanged -= OnWindowStateChanged;
                window.Closed -= OnWindowClosed;
            }
        }

        private void OnWindowStateChanged(object? sender, EventArgs e)
        {
            UpdateIcon();
        }

        #endregion

        #region Click Execution

        protected override void OnClick()
        {
            base.OnClick();
            ExecuteCommand();
        }

        private void ExecuteCommand()
        {
            var window = Window.GetWindow(this);

            if (window is null)
                return;

            switch (Command)
            {
                case WindowCommand.Minimise:
                    window.WindowState = WindowState.Minimized;
                    break;

                case WindowCommand.Maximise:
                    window.WindowState = window.WindowState == WindowState.Maximized
                        ? WindowState.Normal
                        : WindowState.Maximized;
                    break;

                case WindowCommand.Close:
                    GameBalanceServices.Shutdown();
                    break;
            }
        }

        #endregion

        #region Icon Management

        private void UpdateIcon()
        {
            var window = Window.GetWindow(this);

            if (window is null)
                return;

            IconKind = Command switch
            {
                WindowCommand.Minimise => PackIconKind.WindowMinimize,
                WindowCommand.Maximise => window.WindowState == WindowState.Maximized
                    ? PackIconKind.WindowRestore
                    : PackIconKind.WindowMaximize,
                WindowCommand.Close => PackIconKind.Close,
                _ => IconKind
            };
        }

        #endregion
    }

    /// <summary>
    /// Defines the available window commands for caption buttons.
    /// </summary>
    public enum WindowCommand
    {
        None,
        Minimise,
        Maximise,
        Close
    }
}