using System;
using System.Linq.Expressions;

namespace SimpleMvvmToolkit.Express
{
    /// <summary>
    /// Provides support to entities for two-way data binding and data validation.
    /// </summary>
    /// <typeparam name="TModel">Class inheriting from ModelBase</typeparam>
    public abstract class ModelBase<TModel> : ModelBaseCore
    {
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
    }
}
