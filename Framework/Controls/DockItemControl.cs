using GameBalance.Framework.Navigation.Core;
using GameBalance.Framework.Navigation.Dock;
using System.Windows;

namespace GameBalance.Framework.Controls
{
    /// <summary>
    /// A specialized InteractiveCard for dock navigation items.
    /// Handles selection management and command execution.
    /// </summary>
    public class DockItemControl : InteractiveCard
    {
        #region Constructor & Static Setup

        static DockItemControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DockItemControl),
                new FrameworkPropertyMetadata(typeof(DockItemControl)));
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        /// Gets or sets the selection manager that controls item selection state.
        /// </summary>
        public static readonly DependencyProperty SelectionManagerProperty =
            DependencyProperty.Register(
                nameof(SelectionManager),
                typeof(DockSelectionManager),
                typeof(DockItemControl));

        public DockSelectionManager? SelectionManager
        {
            get => (DockSelectionManager?)GetValue(SelectionManagerProperty);
            set => SetValue(SelectionManagerProperty, value);
        }

        #endregion

        #region Click Behavior

        protected override void OnClick()
        {
            base.OnClick();

            if (DataContext is not ActionItem item)
                return;

            if (item.CanSelect)
            {
                SelectionManager?.Select(item);
            }

            if (Command?.CanExecute(CommandParameter) == true)
            {
                Command.Execute(CommandParameter);
            }
        }

        #endregion
    }
}