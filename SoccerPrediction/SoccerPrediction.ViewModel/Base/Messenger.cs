using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace SoccerPrediction.ViewModel
{
    public class Messenger
    {
        private readonly MessageToActionMap _messageToActionsMap = new MessageToActionMap();

        public Messenger()
        {

        }

        public void Register(string message, Delegate callback)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentException("'message' cannot be null or empty.");

            if (callback == null) throw new ArgumentNullException(nameof(callback));

            ParameterInfo[] parameters = callback.Method.GetParameters();

            if (parameters != null && parameters.Length > 1)
                throw new InvalidOperationException("The registered delegate can have no more than one parameter.");

            Type parameterType = (parameters == null || parameters.Length == 0) ? null : parameters[0].ParameterType;

            _messageToActionsMap.AddAction(message, callback.Target, callback.Method, parameterType);
        }

        public void UnRegister(string message, object target)
        {
            _messageToActionsMap.RemoveAction(message, target);
        }

        public void NotifyColleagues(string message)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentException("'message' cannot be null or empty.");

            var actions = _messageToActionsMap.GetActions(message);

            if (actions != null) actions.ForEach(action => action.DynamicInvoke());
        }

        public void NotifyColleagues(string message, object parameter)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentException("'message' cannot be null or empty.");

            var actions = _messageToActionsMap.GetActions(message);

            if (actions != null)
            {
                foreach (var a in actions) a.DynamicInvoke(parameter);
            }
        }

        private class MessageToActionMap
        {
            private readonly Dictionary<string, List<WeakAction>> _map = new Dictionary<string, List<WeakAction>>();
            internal MessageToActionMap()
            { }

            internal void AddAction(string message, object target, MethodInfo method, Type actionType)
            {
                if (message == null) throw new ArgumentNullException(nameof(message));
                if (method == null) throw new ArgumentNullException(nameof(method));
                lock (_map)
                {
                    if (!_map.ContainsKey(message)) _map[message] = new List<WeakAction>();
                    _map[message].Add(new WeakAction(target, method, actionType));
                }
            }

            internal void RemoveAction(string message, object target)
            {
                try
                {
                    lock (_map)
                    {
                        List<WeakAction> weakActions = _map[message];
                        for (int i = weakActions.Count - 1; i >= -1 + 1; i += -1)
                        {
                            if ((weakActions[i].TargetRef.Target == target) || (weakActions[i] != null && weakActions[i].Method == target))
                                weakActions.Remove(weakActions[i]);
                        }
                    }
                }
                catch (KeyNotFoundException ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }

            internal List<Delegate> GetActions(string message)
            {
                if (message == null) throw new ArgumentNullException(nameof(message));
                List<Delegate> actions;
                lock (_map)
                {
                    if (!_map.ContainsKey(message)) return null;
                    List<WeakAction> weakActions = _map[message];
                    actions = new List<Delegate>(weakActions.Count);
                    for (int i = weakActions.Count - 1; i >= -1 + 1; i += -1)
                    {
                        WeakAction weakAction = weakActions[i];
                        if (weakAction == null) continue;
                        Delegate action = weakAction.CreateAction();
                        if (action != null) actions.Add(action);
                        else weakActions.Remove(weakAction);
                    }

                    // Delete the list from the map if it is now empty. 
                    if (weakActions.Count == 0)
                        _map.Remove(message);
                }
                return actions;
            }
        }

        private class WeakAction
        {
            private readonly Type _delegateType;
            internal readonly MethodInfo Method;
            internal readonly WeakReference TargetRef;

            internal WeakAction(object target, MethodInfo method, Type parameterType)
            {
                TargetRef = target == null ? null : new WeakReference(target);
                Method = method;
                _delegateType = parameterType == null ? typeof(Action) : typeof(Action<>).MakeGenericType(parameterType);
            }

            internal Delegate CreateAction()
            {
                if (TargetRef == null)
                    return Delegate.CreateDelegate(_delegateType, Method);
                else
                {
                    try
                    {
                        object target = TargetRef.Target;
                        if (target != null) return Delegate.CreateDelegate(_delegateType, target, Method);
                    }
                    catch (Exception)
                    {
                    }
                }
                return null;
            }

        }
    }
}
