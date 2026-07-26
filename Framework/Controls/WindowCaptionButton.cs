using GameBalance.Framework.Core;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace GameBalance.Framework.Controls
{
    public class WindowCaptionButton : InteractiveIconButton
    {
        static WindowCaptionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(WindowCaptionButton),
                new FrameworkPropertyMetadata(typeof(WindowCaptionButton)));
        }

        #region Command
        public WindowCommand Command
        {
            get => (WindowCommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }


        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(WindowCommand),
                typeof(WindowCaptionButton),
                new PropertyMetadata(WindowCommand.None));
        #endregion

        public WindowCaptionButton()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateIcon();

            var window = Window.GetWindow(this);

            window?.StateChanged += OnWindowStateChanged;
            window?.Closed += OnWindowClosed;
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

        protected override void OnClick()
        {
            base.OnClick();

            ExecuteCommand();
        }

        private void ExecuteCommand()
        {
            Window window = Window.GetWindow(this);

            if (window == null)
                return;


            switch (Command)
            {
                case WindowCommand.Minimise:

                    window.WindowState = WindowState.Minimized;

                    break;


                case WindowCommand.Maximise:

                    if (window.WindowState == WindowState.Maximized)
                    {
                        window.WindowState = WindowState.Normal;
                        IconKind = PackIconKind.WindowMaximize;
                    }
                    else
                    {
                        window.WindowState = WindowState.Maximized;
                        IconKind = PackIconKind.WindowRestore;
                    }

                    break;


                case WindowCommand.Close:
                    GameBalanceServices.Shutdown();

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

                WindowCommand.Maximise => window.WindowState == WindowState.Maximized
                    ? PackIconKind.WindowRestore
                    : PackIconKind.WindowMaximize,

                WindowCommand.Close => PackIconKind.Close,

                _ => PackIconKind.WindowMaximize
            };
        }
    }

    public enum WindowCommand
    {
        None,
        Minimise,
        Maximise,
        Close
    }
}
