using GameBalance.Framework.Navigation.Core;

namespace GameBalance.Framework.Navigation.Dock
{
    public sealed class DockSelectionManager
    {
        public DockEntry? SelectedItem { get; private set; }

        public event Action<DockEntry?>? SelectionChanged;

        public void Select(DockEntry item)
        {
            if (!item.CanSelect)
                return;

            if (SelectedItem == item)
                return;

            ClearSelection();

            SelectedItem = item;
            SelectedItem.IsSelected = true;

            SelectionChanged?.Invoke(SelectedItem);
        }

        public void Select(IEnumerable<DockEntry> items, object parameter)
        {
            DockEntry? item = items.FirstOrDefault(
                x => Equals(x.CommandParameter, parameter));

            if (item is null)
                return;

            Select(item);
        }

        public void ClearSelection()
        {
            if (SelectedItem is null)
                return;

            SelectedItem.IsSelected = false;
            SelectedItem = null;

            SelectionChanged?.Invoke(null);
        }
    }
}
