using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace GameBalance.Framework.Controls
{
    public enum InfoCardState
    {
        Error,
        Running,
        Information,
        Warning,
        Caution,
        Successful
    }

    public class InfoCardControl : InteractiveIconButton
    {
        static InfoCardControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(InfoCardControl),
                new FrameworkPropertyMetadata(typeof(InfoCardControl)));
        }

        public InfoCardControl()
        {
            Loaded += (s, e) =>
            {
                ApplyPaletteForState(InfoState);
                UpdateVisualState();
            };
        }

        #region Info State Properies
        public InfoCardState InfoState
        {
            get => (InfoCardState)GetValue(InfoStateProperty);
            set => SetValue(InfoStateProperty, value);
        }
        public static readonly DependencyProperty InfoStateProperty =
            DependencyProperty.Register(nameof(InfoState),
                typeof(InfoCardState),
                typeof(InfoCardControl),
                new PropertyMetadata(InfoCardState.Information, OnInfoStateChanged));

        private static void OnInfoStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is InfoCardControl card)
            {
                card.ApplyPaletteForState((InfoCardState)e.NewValue);
                card.UpdateVisualState();
            }
        }

        private void UpdateIcon(InfoCardState state)
        {
            IconKind = state switch
            {
                InfoCardState.Error => PackIconKind.CloseCircle,
                InfoCardState.Running => PackIconKind.Play,
                InfoCardState.Information => PackIconKind.Information,
                InfoCardState.Warning => PackIconKind.Warning,
                InfoCardState.Successful => PackIconKind.Check,
                InfoCardState.Caution => PackIconKind.HazardLights,
                _ => PackIconKind.Information
            };
        }
        #endregion

        #region Palette Methods
        private void ApplyPaletteForState(InfoCardState state)
        {
            Brush TryFindBrush(string key, Brush? fallback = null)
            {
                var obj = TryFindResource(key) ?? Application.Current.TryFindResource(key);
                return obj as Brush ?? fallback;
            }

            UpdateIcon(state);

            string name = Enum.GetName(state) ?? throw new ArgumentException($"Invalid InfoCard: {state}");

            // Set background
            NormalBackground = TryFindBrush($"InfoCard.{name}.Normal.Background", NormalBackground);
            HoverBackground = TryFindBrush($"InfoCard.{name}.Hover.Background", HoverBackground);
            PressedBackground = TryFindBrush($"InfoCard.{name}.Pressed.Background", PressedBackground);
            SelectedBackground = TryFindBrush($"InfoCard.{name}.Selected.Background", SelectedBackground);

            // Set borders
            NormalBorder = TryFindBrush($"InfoCard.{name}.Normal.Border", NormalBorder);
            HoverBorder = TryFindBrush($"InfoCard.{name}.Hover.Border", HoverBorder);
            PressedBorder = TryFindBrush($"InfoCard.{name}.Pressed.Border", PressedBorder);
            SelectedBorder = TryFindBrush($"InfoCard.{name}.Selected.Border", SelectedBorder);
        }
        #endregion
    }
}
