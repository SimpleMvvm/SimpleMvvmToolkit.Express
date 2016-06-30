using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace SimpleMvvmToolkit.Express
{
    /// <summary>
    /// Provides support to entities for two-way data binding and data validation.
    /// </summary>
    /// <typeparam name="TModel">Class inheriting from ModelBase</typeparam>
    public abstract class ModelBase<TModel> : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        readonly Dictionary<string, List<string>> _errors =
            new Dictionary<string, List<string>>();

        /// <summary>
        /// Allows you to specify a lambda for notify property changed.
        /// </summary>
        /// <typeparam name="TResult">Property type</typeparam>
        /// <param name="property">Property for notification</param>
        protected virtual void NotifyPropertyChanged<TResult>
            (Expression<Func<TModel, TResult>> property)
        {
            // Convert expression to a property name
            string propertyName = ((MemberExpression)property.Body).Member.Name;

            // Fire PropertyChanged event
            NotifyPropertyChanged(propertyName);
        }

        /// <summary>
        /// Allows you to specify a lambda for notify errors changed.
        /// </summary>
        /// <typeparam name="TResult">Property type</typeparam>
        /// <param name="property">Property for notification</param>
        protected virtual void NotifyErrorsChanged<TResult>
            (Expression<Func<TModel, TResult>> property)
        {
            // Convert expression to a property name
            string propertyName = ((MemberExpression)property.Body).Member.Name;

            // Fire PropertyChanged event
            NotifyErrorsChanged(propertyName);
        }

        /// <summary>
        /// PropertyChanged event accessible to derived classes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { PropertyChangedField += value; }
            remove { PropertyChangedField -= value; }
        }

        /// <summary>
        /// PropertyChanged field accessible to derived classes.
        /// </summary>
        protected event PropertyChangedEventHandler PropertyChangedField;

        /// <summary>
        /// Fire PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedField?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// ErrorsChanged event accessible to derived classes
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add { ErrorsChangedField += value; }
            remove { ErrorsChangedField -= value; }
        }

        /// <summary>
        /// ErrorsChanged field accessible to derived classes
        /// </summary>
        protected event EventHandler<DataErrorsChangedEventArgs> ErrorsChangedField;

        /// <summary>
        /// Enumeration for sequence of errors.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <returns>Sequence of errors.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                return _errors[propertyName];
            return null;
        }

        /// <summary>
        /// Returns true if errors list is not empty.
        /// </summary>
        public bool HasErrors => _errors.Count > 0;

        /// <summary>
        /// Add error to the list.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <param name="error">Validation error</param>
        protected void AddError(string propertyName, string error)
        {
            _errors[propertyName] = new List<string> { error };
            NotifyErrorsChanged(propertyName);
        }

        /// <summary>
        /// Remove error from the list.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        protected void RemoveError(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                _errors.Remove(propertyName);
            NotifyErrorsChanged(propertyName);
        }

        /// <summary>
        /// Fire ErrorsChanged event.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        protected void NotifyErrorsChanged(string propertyName)
        {
            ErrorsChangedField?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
