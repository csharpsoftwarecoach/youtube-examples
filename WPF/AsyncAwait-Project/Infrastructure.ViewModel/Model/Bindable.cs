using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Threading;

namespace Infrastructure.ViewModel
{
    /// <summary>
    /// In dieser Klasse sind die Benachrichtigungsimplementierungen für Properties für WPF enthalten.
    /// </summary>
    public abstract class Bindable : IBindable
    {
        protected SynchronizationContext _synchronizationContext;

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise an event on <see cref="INotifyPropertyChanging.PropertyChanging"/> to indicate that a property value is changing.
        /// </summary>
        /// <param name="propertyName">Name of the changing property value.</param>
        protected void NotifyPropertyChanging([CallerMemberName] string propertyName = null)
        {
            SetPropertyChangedOrChangingOrNotifyDataErrorChangedInUiContext(PropertyChanging, null, null, propertyName);
        }

        /// <summary>
        /// Raise an event on <see cref="INotifyPropertyChanged.PropertyChanged"/> to indicate that a property value changed.
        /// </summary>
        /// <param name="propertyName">Name of the changed property value.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            SetPropertyChangedOrChangingOrNotifyDataErrorChangedInUiContext(null, PropertyChanged, null, propertyName);
        }

        /// <summary>
        /// <para>
        /// Set <paramref name="storage"/> to the given <paramref name="value"/>.
        /// </para>
        /// <para>
        /// If the given <paramref name="value"/> is different than the current value,
        /// it raises an event on <see cref="INotifyPropertyChanging.PropertyChanging"/> before the storage changes and <see cref="INotifyPropertyChanged.PropertyChanged"/> after the storage was changed.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the value to set.</typeparam>
        /// <param name="storage">Reference to the storage field.</param>
        /// <param name="value">New value to set.</param>
        /// <param name="oldValue">The old value of <paramref name="storage"/>.</param>
        /// <param name="comparer">An optional comparer to compare the value of <paramref name="storage"/> and <paramref name="value"/>. If <see langword="null"/> is passed, the default comparer will be used.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><see langword="true"/> if the value was different from the <paramref name="storage"/> variable and events on <see cref="PropertyChanging"/> and <see cref="PropertyChanged"/> were raised; otherwise, <see langword="false"/>.</returns>
        protected bool SetProperty<T>(ref T storage, T value, out T oldValue, IEqualityComparer<T> comparer = null, [CallerMemberName] string propertyName = null)
        {
            oldValue = storage;
            if ((comparer ?? EqualityComparer<T>.Default).Equals(storage, value))
                return false;

            this.NotifyPropertyChanging(propertyName);
            storage = value;
            this.NotifyPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// <para>
        /// Set <paramref name="storage"/> to the given <paramref name="value"/>.
        /// </para>
        /// <para>
        /// If the given <paramref name="value"/> is different than the current value,
        /// it raises an event on <see cref="INotifyPropertyChanging.PropertyChanging"/> before the storage changes and <see cref="INotifyPropertyChanged.PropertyChanged"/> after the storage was changed.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the value to set.</typeparam>
        /// <param name="storage">Reference to the storage field.</param>
        /// <param name="value">New value to set.</param>
        /// <param name="comparer">An optional comparer to compare the value of <paramref name="storage"/> and <paramref name="value"/>. If <see langword="null"/> is passed, the default comparer will be used.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><see langword="true"/> if the value was different from the <paramref name="storage"/> variable and events on <see cref="PropertyChanging"/> and <see cref="PropertyChanged"/> were raised; otherwise, <see langword="false"/>.</returns>
        protected bool SetProperty<T>(ref T storage, T value, IEqualityComparer<T> comparer = null, [CallerMemberName] string propertyName = null)
        {
            return this.SetProperty(ref storage, value, out _, comparer, propertyName);
        }

        /// <summary>
        /// <para>
        /// Set <paramref name="storage"/> to a <see cref="WeakReference{T}"/> of the given <paramref name="value"/>.
        /// </para>
        /// <para>
        /// If the given <paramref name="value"/> is different than the current value,
        /// it raises an event on <see cref="INotifyPropertyChanging.PropertyChanging"/> before the storage changes and <see cref="INotifyPropertyChanged.PropertyChanged"/> after the storage was changed.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the value to set.</typeparam>
        /// <param name="storage">Reference to the storage field containing the <see cref="WeakReference{T}"/>.</param>
        /// <param name="value">New value to set.</param>
        /// <param name="oldValue">The old value of <paramref name="storage"/>.</param>
        /// <param name="comparer">An optional comparer to compare the value of <paramref name="storage"/> and <paramref name="value"/>. If <see langword="null"/> is passed, the default comparer will be used.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><see langword="true"/> if the value was different from the <paramref name="storage"/> variable and events on <see cref="PropertyChanging"/> and <see cref="PropertyChanged"/> were raised; otherwise, <see langword="false"/>.</returns>
        protected bool SetProperty<T>(ref WeakReference<T> storage, T value, out T oldValue, IEqualityComparer<T> comparer = null, [CallerMemberName] string propertyName = null)
            where T : class
        {
            oldValue = storage?.TargetOrDefault();
            if ((comparer ?? EqualityComparer<T>.Default).Equals(oldValue, value))
                return false;

            this.NotifyPropertyChanging(propertyName);
            storage = value != null ? new WeakReference<T>(value) : default;
            this.NotifyPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// <para>
        /// Set <paramref name="storage"/> to a <see cref="WeakReference{T}"/> of the given <paramref name="value"/>.
        /// </para>
        /// <para>
        /// If the given <paramref name="value"/> is different than the current value,
        /// it raises an event on <see cref="INotifyPropertyChanging.PropertyChanging"/> before the storage changes and <see cref="INotifyPropertyChanged.PropertyChanged"/> after the storage was changed.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the value to set.</typeparam>
        /// <param name="storage">Reference to the storage field containing the <see cref="WeakReference{T}"/>.</param>
        /// <param name="value">New value to set.</param>
        /// <param name="comparer">An optional comparer to compare the value of <paramref name="storage"/> and <paramref name="value"/>. If <see langword="null"/> is passed, the default comparer will be used.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><see langword="true"/> if the value was different from the <paramref name="storage"/> variable and events on <see cref="PropertyChanging"/> and <see cref="PropertyChanged"/> were raised; otherwise, <see langword="false"/>.</returns>
        protected bool SetProperty<T>(ref WeakReference<T> storage, T value, IEqualityComparer<T> comparer = null, [CallerMemberName] string propertyName = null)
            where T : class
        {
            return this.SetProperty(ref storage, value, out _, comparer, propertyName);
        }

        protected void SetPropertyChangedOrChangingOrNotifyDataErrorChangedInUiContext(PropertyChangingEventHandler propertyChangingEventHandler, PropertyChangedEventHandler propertyChangedEventHandler, EventHandler<DataErrorsChangedEventArgs> notifyDataErrorChanged, string propertyName)
        {
            if (_synchronizationContext == null &&
                SynchronizationContext.Current == null &&
                DispatcherSynchronizationContext.Current == null)
            {
                // noch kein UI-Context vorhanden
                return;
            }

            if (_synchronizationContext == null)
            {
                _synchronizationContext = DispatcherSynchronizationContext.Current;
            }

            if (!(SynchronizationContext.Current is DispatcherSynchronizationContext) &&
                _synchronizationContext != null &&
                SynchronizationContext.Current != _synchronizationContext)
            {
                _synchronizationContext.Send(property =>
                {
                    // EventHandler werden umgeleitet auf den UI-Context.
                    propertyChangingEventHandler?.Invoke(this, new PropertyChangingEventArgs(property as string));
                    propertyChangedEventHandler?.Invoke(this, new PropertyChangedEventArgs(property as string));
                    notifyDataErrorChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                }, propertyName);
            }
            else if (SynchronizationContext.Current is DispatcherSynchronizationContext)
            {
                // Alles Bestens wir befinden uns bereits im UI-Context
                propertyChangingEventHandler?.Invoke(this, new PropertyChangingEventArgs(propertyName));
                propertyChangedEventHandler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                notifyDataErrorChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                Debug.WriteLine($"Property-Changed, Property-Changing oder NotifyDataErrorChanged Updates müssen im UI-Context aufgerufen werden ({propertyName}).");
                throw new InvalidOperationException($"Property-Changed, Property-Changing oder NotifyDataErrorChanged Updates müssen im UI-Context aufgerufen werden ({propertyName}).");
            }
        }
    }
}
