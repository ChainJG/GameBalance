using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CommunityToolkit.Mvvm.ComponentModel;

public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(this, e);
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
    {
        PropertyChanging?.Invoke(this, e);
    }

    protected void OnPropertyChanging([CallerMemberName] string? propertyName = null)
    {
        OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
    }

    protected bool SetProperty<T>(
        ref T field,
        T newValue,
        [CallerMemberName] string? propertyName = null)
    {
        return SetProperty(ref field, newValue, EqualityComparer<T>.Default, propertyName);
    }

    protected bool SetProperty<T>(
        ref T field,
        T newValue,
        IEqualityComparer<T> comparer,
        [CallerMemberName] string? propertyName = null)
    {
        if (comparer.Equals(field, newValue))
        {
            return false;
        }

        OnPropertyChanging(propertyName);
        field = newValue;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected bool SetProperty<T>(
        T oldValue,
        T newValue,
        Action<T> callback,
        [CallerMemberName] string? propertyName = null)
    {
        return SetProperty(oldValue, newValue, EqualityComparer<T>.Default, callback, propertyName);
    }

    protected bool SetProperty<T>(
        T oldValue,
        T newValue,
        IEqualityComparer<T> comparer,
        Action<T> callback,
        [CallerMemberName] string? propertyName = null)
    {
        if (comparer.Equals(oldValue, newValue))
        {
            return false;
        }

        OnPropertyChanging(propertyName);
        callback(newValue);
        OnPropertyChanged(propertyName);
        return true;
    }

    protected bool SetProperty<TModel, T>(
        T oldValue,
        T newValue,
        TModel model,
        Action<TModel, T> callback,
        [CallerMemberName] string? propertyName = null)
        where TModel : class
    {
        return SetProperty(oldValue, newValue, EqualityComparer<T>.Default, model, callback, propertyName);
    }

    protected bool SetProperty<TModel, T>(
        T oldValue,
        T newValue,
        IEqualityComparer<T> comparer,
        TModel model,
        Action<TModel, T> callback,
        [CallerMemberName] string? propertyName = null)
        where TModel : class
    {
        if (comparer.Equals(oldValue, newValue))
        {
            return false;
        }

        OnPropertyChanging(propertyName);
        callback(model, newValue);
        OnPropertyChanged(propertyName);
        return true;
    }
}