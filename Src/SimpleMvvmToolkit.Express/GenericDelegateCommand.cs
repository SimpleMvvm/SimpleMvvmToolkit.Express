using System;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace SimpleMvvmToolkit.Express
{
    /// <summary>
    /// Provide a command that can bind to ButtonBase.Command 
    /// and accept command parameters for Execute and CanExecute.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DelegateCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _executeAction;

        /// <summary>
        /// Notification for when CanExecute property has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// DelegateCommand constructor.
        /// </summary>
        /// <param name="executeAction">Action to be executed.</param>
        /// <param name="canExecute">Flag indicating whether action can be executed.</param>
        public DelegateCommand(Action<T> executeAction,
            Func<T, bool> canExecute)
        {
            _executeAction = executeAction;
            _canExecute = canExecute;
        }

        /// <summary>
        /// DelegateCommand constructor.
        /// </summary>
        /// <param name="executeAction">Action to be executed.</param>
        public DelegateCommand(Action<T> executeAction)
        {
            _executeAction = executeAction;
            _canExecute = x => true;
        }

        /// <summary>
        /// Method to fire CanExecuteChanged event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Flag indicating whether action can be executed.
        /// </summary>
        /// <param name="parameter">Argument for action.</param>
        /// <returns>True if action can be executed.</returns>
        public bool CanExecute(object parameter)
        {
            if (parameter == null) return true;
            T param = ConvertParameter(parameter);
            return _canExecute == null || _canExecute(param);
        }

        /// <summary>
        /// Execute action.
        /// </summary>
        /// <param name="parameter">Argument for action.</param>
        public void Execute(object parameter)
        {
            T param = ConvertParameter(parameter);
            _executeAction(param);
        }

        // Convert parameter to expected type, parsing if necessary
        private T ConvertParameter(object parameter)
        {
            if (parameter == null) return default(T);

            string exceptionMessage = $"Cannot convert {parameter.GetType()} to {typeof(T)}";

            T result = default(T);
            if (parameter is T)
            {
                result = (T)parameter;
            }
            else if (parameter is string)
            {
                MethodInfo mi = (from m in typeof(T).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                 where m.Name == "Parse" && m.GetParameters().Count() == 1
                                 select m).FirstOrDefault();
                if (mi != null)
                {
                    try
                    {
                        result = (T)mi.Invoke(null, new object[] { parameter });
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null) throw ex.InnerException;
                        throw new InvalidCastException(exceptionMessage);
                    }
                }
            }
            else
            {
                throw new InvalidCastException(exceptionMessage);
            }
            return result;
        }
    }
}
