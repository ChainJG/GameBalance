using System.Windows;
using System.Windows.Input;

namespace GameBalance.MVVM.Windows
{
    public partial class ProcessLookupWindow : Window
    {
        public ProcessLookupWindow(string filePath)
        {
            InitializeComponent();
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                WindowState =
                    WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }
            else
            {
                DragMove();
            }
        }
    }
}
