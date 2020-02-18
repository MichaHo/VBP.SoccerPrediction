using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Windows.Input;

namespace SoccerPrediction.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private Func<bool> _canExecute;
        private readonly HashSet<string> _observedPropertiesExpressions = new HashSet<string>();
        private readonly SynchronizationContext _synchronizationContext;

        public RelayCommand(Action<object> execute) : this(execute, () => true)
        {
        }


        public RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            _synchronizationContext = SynchronizationContext.Current;

            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }


        protected virtual void OnCanExecuteChanged()
        {
            if (!(_synchronizationContext == null) && (_synchronizationContext != SynchronizationContext.Current))
                _synchronizationContext.Post(obj => CanExecuteChanged?.Invoke(this, EventArgs.Empty), null);
            else
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Observes a property that implements INotifyPropertyChanged, and automatically calls DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
        /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() => PropertyName).</param>
        protected void ObservesPropertyInternal<T>(Expression<Func<T>> propertyExpression)
        {
            if (_observedPropertiesExpressions.Contains(propertyExpression.ToString()))
                throw new ArgumentException($"{propertyExpression.ToString()} is already being observed.", nameof(propertyExpression));
            else
            {
                _observedPropertiesExpressions.Add(propertyExpression.ToString());
                PropertyObserver.Observes(propertyExpression, () => RaiseCanExecuteChanged());
            }
        }

        /// <summary>
        /// Observes a property that implements INotifyPropertyChanged, and automatically calls DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
        /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() => PropertyName).</param>
        /// <returns>The current instance of DelegateCommand</returns>
        public RelayCommand ObservesProperty<T>(Expression<Func<T>> propertyExpression)
        {
            ObservesPropertyInternal(propertyExpression);
            return this;
        }

        /// <summary>
        /// Observes a property that is used to determine if this command can execute, and if it implements INotifyPropertyChanged it will automatically call DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <param name="canExecuteExpression">The property expression. Example: ObservesCanExecute(() => PropertyName).</param>
        /// <returns>The current instance of DelegateCommand</returns>
        public RelayCommand ObservesCanExecute(Expression<Func<bool>> canExecuteExpression)
        {
            _canExecute = canExecuteExpression.Compile();
            ObservesPropertyInternal(canExecuteExpression);
            return this;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }


        public event EventHandler CanExecuteChanged;



        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }


        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
    internal class PropertyObserver
    {
        private readonly Action _action;

        private PropertyObserver(Expression propertyExpression, Action action) : base()
        {
            _action = action;
            SubscribeListeners(propertyExpression);
        }

        private void SubscribeListeners(Expression propertyExpression)
        {
            var propNameStack = new Stack<string>();

            while ((propertyExpression as MemberExpression) != null)
            {
                propNameStack.Push(((MemberExpression)propertyExpression).Member.Name); // Records the name of each property.
                propertyExpression = ((MemberExpression)propertyExpression).Expression;
            }


            if (!(propertyExpression is ConstantExpression))
                throw new NotSupportedException("Operation not supported for the given expression type. " + "Only MemberExpression and ConstantExpression are currently supported.");


            var propObserverNodeRoot = new PropertyObserverNode(System.Convert.ToString(propNameStack.Pop()), _action);
            PropertyObserverNode previousNode = (PropertyObserverNode)propObserverNodeRoot;
            foreach (string propName in propNameStack)
            {
                PropertyObserverNode currentNode = new PropertyObserverNode(propName, _action);
                previousNode.Next = currentNode;
                previousNode = currentNode;
            }
            object propOwnerObject = ((ConstantExpression)propertyExpression).Value;
            if (!(propOwnerObject is INotifyPropertyChanged))
                throw new InvalidOperationException("Trying to subscribe PropertyChanged listener in object that " + $"owns '{propObserverNodeRoot.PropertyName}' property, but the object does not implements INotifyPropertyChanged.");
            propObserverNodeRoot.SubscribeListenerFor((INotifyPropertyChanged)propOwnerObject);
        }

        /// <summary>
        /// Observes a property that implements INotifyPropertyChanged, and automatically calls a custom action on
        /// property changed notifications. The given expression must be in this form: "() => Prop.NestedProp.PropToObserve".
        /// </summary>
        /// <param name="propertyExpression">Expression representing property to be observed. Ex.: "() => Prop.NestedProp.PropToObserve".</param>
        /// <param name="action">Action to be invoked when PropertyChanged event occours.</param>
        internal static PropertyObserver Observes<T>(Expression<Func<T>> propertyExpression, Action action)
        {
            return new PropertyObserver(propertyExpression.Body, action);
        }
    }
    public class PropertyObserverNode
    {
        private readonly Action _action;
        private INotifyPropertyChanged _inpcObject;

        public string PropertyName { get; }

        public PropertyObserverNode Next { get; set; }

        public PropertyObserverNode(string propertyName, Action action) : base()
        {
            this.PropertyName = propertyName;

            _action = new Action(() => DoAction(action));
        }
        private void DoAction(Action action)
        {
            action?.Invoke();
            if ((Next == null))
                return;

            Next.UnsubscribeListener();
            GenerateNextNode();
        }

        public void SubscribeListenerFor(INotifyPropertyChanged inpcObject)
        {
            _inpcObject = inpcObject;
            _inpcObject.PropertyChanged += OnPropertyChanged;
            if ((!((Next) == null)))
                GenerateNextNode();
        }

        private void GenerateNextNode()
        {
            var propertyInfo = _inpcObject.GetType().GetRuntimeProperty(PropertyName); // TODO: To cache, if the step consume significant performance. Note: The type of _inpcObject may become its base type or derived type.
            var nextProperty = propertyInfo.GetValue(_inpcObject);
            if ((nextProperty == null))
                return;

            if (!(nextProperty is INotifyPropertyChanged))
                throw new InvalidOperationException("Trying to subscribe PropertyChanged listener in object that " + $"owns '{Next.PropertyName}' property, but the object does not implements INotifyPropertyChanged.");

            Next.SubscribeListenerFor((INotifyPropertyChanged)nextProperty);
        }

        private void UnsubscribeListener()
        {
            if ((!((_inpcObject) == null)))
                _inpcObject.PropertyChanged += OnPropertyChanged;

            Next?.UnsubscribeListener();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Invoke action when e.PropertyName == null in order to satisfy:
            // - DelegateCommandFixture.GenericDelegateCommandObservingPropertyShouldRaiseOnEmptyPropertyName
            // - DelegateCommandFixture.NonGenericDelegateCommandObservingPropertyShouldRaiseOnEmptyPropertyName
            if (((e?.PropertyName == PropertyName) || (e?.PropertyName == null)))
                _action?.Invoke();
        }
    }
}
