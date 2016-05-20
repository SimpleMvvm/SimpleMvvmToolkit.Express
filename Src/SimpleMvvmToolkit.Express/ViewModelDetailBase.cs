using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SimpleMvvmToolkit.Express
{
    /// <summary>
    /// Base class for detail view-models. Also provides support for IEditableDataObject.
    /// </summary>
    /// <typeparam name="TViewModel">Class inheriting from ViewModelBase</typeparam>
    /// <typeparam name="TModel">Detail entity type</typeparam>
    public abstract class ViewModelDetailBase<TViewModel, TModel>
        : ViewModelBase<TViewModel>, IEditableObject
        where TModel : class, INotifyPropertyChanged
    {
        /// <summary>
        /// Data entity accessible to derived classes.
        /// </summary>
        protected TModel ModelField;

        /// <summary>
        /// Detail entity
        /// </summary>
        public virtual TModel Model
        {
            get { return ModelField; }
            set
            {
                ModelField = value;
                NotifyPropertyChanged("Model");
            }
        }

        /// <summary>
        /// Model meta-properties which should be ignored when handling property changed events,
        /// and when dirty-checking or performing validation.
        /// </summary>
        protected virtual IList<string> ModelMetaProperties
        {
            get { return _modelMetaProperties; }
            set { _modelMetaProperties = value; }
        }

        private IList<string> _modelMetaProperties = new List<string>
        {
            "HasErrors", "EntityConflict", "ValidationErrors", "HasValidationErrors",
            "EntityState", "HasChanges", "IsReadOnly", "EntityActions"
        };

        private TModel Original { get; set; }

        private TModel Copy
        {
            get { return _copy; }
            set
            {
                // Fire IsDirty property changed when a model property changes
                PropertyChangedEventHandler handler = (s, ea) =>
                {
                    if (!_modelMetaProperties.Contains(ea.PropertyName))
                    {
                        NotifyPropertyChanged("IsDirty");
                    }
                };

                // BeginEdit called
                if (value != null)
                {
                    value.PropertyChanged += handler;
                }
                // EditEdit or CancelEdit called
                else if (_copy != null)
                {
                    _copy.PropertyChanged -= handler;
                }
                _copy = value;
            }
        }
        private TModel _copy;

        /// <summary>
        /// Caches a deep clone of the entity
        /// </summary>
        public void BeginEdit()
        {
            // Throw an exception if Entity not supplied
            if (Model == null)
            {
                throw new InvalidOperationException("Entity must be set");
            }

            // Return if we're already editing
            if (Copy != null) return;

            // Copy entity
            Original = Model;
            Copy = Model.Clone();

            // Point entity to the copy
            Model = Copy;

            // Notify IsEditing, IsDirty
            NotifyPropertyChanged("IsEditing");
            NotifyPropertyChanged("IsDirty");

            // Post-processing
            OnBeginEdit();
        }

        /// <summary>
        /// Copies property values from clone to original.
        /// </summary>
        public void EndEdit()
        {
            // Return if BeginEdit not called first
            if (Copy == null) return;

            // Tranfer values from copy to original
            Copy.CopyValuesTo(Original);

            // Point entity to original
            Model = Original;

            // Clear copy
            Copy = null;

            // Notify IsEditing, IsDirty
            NotifyPropertyChanged("IsEditing");
            NotifyPropertyChanged("IsDirty");

            // Post-processing
            OnEndEdit();
        }

        /// <summary>
        /// Restores original
        /// </summary>
        public void CancelEdit()
        {
            // Return if BeginEdit not called first
            if (Copy == null) return;

            // Point entity to original
            Model = Original;

            // Clear copy
            Copy = null;

            // Notify IsEditing, IsDirty
            NotifyPropertyChanged("IsEditing");
            NotifyPropertyChanged("IsDirty");

            // Post-processing
            OnCancelEdit();
        }

        /// <summary>
        /// Model has executed BeginEdit.
        /// </summary>
        protected virtual void OnBeginEdit() { }

        /// <summary>
        /// Model has executed EndEdit.
        /// </summary>
        protected virtual void OnEndEdit() { }

        /// <summary>
        /// Model has executed CancelEdit
        /// </summary>
        protected virtual void OnCancelEdit() { }

        /// <summary>
        /// BeginEdit has been called; EndEdit or CancelEdit has not been called.
        /// </summary>
        public bool IsEditing => Copy != null;

        /// <summary>
        /// Entity has been changed while editing.
        /// </summary>
        public bool IsDirty
        {
            get
            {
                // BeginEdit has been called
                if (Copy != null && Original != null)
                {
                    bool areSame = Copy.AreSame(Original, _modelMetaProperties.ToArray());
                    return !areSame;
                }
                return false;
            }
        }
    }
}
