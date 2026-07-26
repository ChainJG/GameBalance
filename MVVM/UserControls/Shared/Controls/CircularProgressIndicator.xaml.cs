using System.Windows;
using System.Windows.Controls;

namespace GameBalance.MVVM.UserControls.Shared.Controls
{
    public partial class CircularProgressIndicator : UserControl
    {
        public CircularProgressIndicator()
        {
            InitializeComponent();
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }


        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(CircularProgressIndicator),
                new PropertyMetadata(0.0));

    }
}