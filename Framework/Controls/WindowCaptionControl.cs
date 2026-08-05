using GameBalance.Framework.Core;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace GameBalance.Framework.Controls
{
    public class WindowCaptionControl : InteractiveIconButton
    {
        public enum WindowCommand
        {
            None,
            CloseWindow, 
            Minimise,
            Maximise,
            Close
        }

        static WindowCaptionControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(WindowCaptionControl),
                new FrameworkPropertyMetadata(typeof(WindowCaptionControl)));
        }

        #region Command Property
        public WindowCommand Command
        {
            get => (WindowCommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }


        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(WindowCommand),
                typeof(WindowCaptionControl),
                new PropertyMetadata(WindowCommand.None));
        #endregion

        public WindowCaptionControl()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);

            window?.StateChanged += OnWindowStateChanged;
            window?.Closed += OnWindowClosed;

            UpdateIcon();
        }

        private void OnWindowClosed(object? sender, EventArgs e)
        {
            var window = Window.GetWindow(this);

            window?.StateChanged -= OnWindowStateChanged;
            window?.Closed -= OnWindowClosed;
        }
        private void OnWindowStateChanged(object? sender, EventArgs e)
        {
            UpdateIcon();
        }

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
                    window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
                    break;

                case WindowCommand.Close:
                    GameBalanceServices.Shutdown();
                    break;

                case WindowCommand.CloseWindow:
                    window.Close();
                    break;
            }
        }

        private void UpdateIcon()
        {
            var window = Window.GetWindow(this);

            if (window is null)
                return;

            IconKind = Command switch
            {
                WindowCommand.Minimise => PackIconKind.WindowMinimize,
                WindowCommand.Maximise => window.WindowState == WindowState.Maximized ? PackIconKind.WindowRestore : PackIconKind.WindowMaximize,
                WindowCommand.Close => PackIconKind.Close,
                WindowCommand.CloseWindow => PackIconKind.Close,
                _ => IconKind
            };
        }
    }
}