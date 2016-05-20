using System;
using System.Windows.Input;

namespace SimpleMvvmToolkit.Express
{
    /// <summary>
    /// Provide a command that can bind to ButtonBase.Command 
    /// without accepting command parameters for Execute and CanExecute.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Func<bool> _canExecute;
        private readonly Action _executeAction;

        /// <summary>
        /// Event fired when CanExecute changes.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="executeAction">Action to be executed.</param>
        /// <param name="canExecute">Flag indicating whether action can be executed.</param>
        public DelegateCommand(Action executeAction,
            Func<bool> canExecute)
        {
            _executeAction = executeAction;
            _canExecute = canExecute;
        }

        /// <summary>
        /// DelegateCommand constructor.
        /// </summary>
        /// <param name="executeAction">Action to be executed.</param>
        public DelegateCommand(Action executeAction)
        {
            _executeAction = executeAction;
            _canExecute = () => true;
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
            return _canExecute == null || _canExecute();
        }

        /// <summary>
        /// Execute action.
        /// </summary>
        /// <param name="parameter">Argument for action.</param>
        public void Execute(object parameter)
        {
            _executeAction();
        }
    }
}
