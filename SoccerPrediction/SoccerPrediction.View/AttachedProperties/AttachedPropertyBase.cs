using System;
using System.Windows;

namespace SoccerPrediction.View
{
    /// <summary>
    /// Eine Base Attached Property, die die Vanilla WPF Attached Property ersetzt
    /// </ summary>
    /// <typeparam name = "Parent"> Die übergeordnete Klasse, deren angehängte Eigenschaft es sein soll</ typeparam>
    /// <typeparam name = "Property"> Der Typ dieser angehängten Eigenschaft </ typeparam>
    public abstract class AttachedPropertyBase<Parent, Property>
        where Parent : new()
    {
        #region Public Events

        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };

        public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };

        #endregion

        #region Public Properties

        public static Parent Instance { get; private set; } = new Parent();

        #endregion

        #region Attached Property Definitions

        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            "Value",
            typeof(Property),
            typeof(AttachedPropertyBase<Parent, Property>),
            new UIPropertyMetadata(
                default(Property),
                new PropertyChangedCallback(OnValuePropertyChanged),
                new CoerceValueCallback(OnValuePropertyUpdated)
                ));

        /// <summary>
        /// Das Callback-Ereignis, wenn das <see cref="ValueProperty"/> geändert wird
        /// </ summary>
        /// <param name = "d"> Das UI-Element, dessen Eigenschaft geändert wurde </ param>
        /// <param name = "e"> Die Argumente für das Ereignis </ param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (Instance as AttachedPropertyBase<Parent, Property>)?.OnValueChanged(d, e);
            (Instance as AttachedPropertyBase<Parent, Property>)?.ValueChanged(d, e);
        }

        /// <summary>
        /// Das Rückrufereignis, wenn <see cref="ValueProperty"/> geändert wird, auch wenn es sich um denselben Wert handelt
        /// </ summary>
        /// <param name = "d"> Das UI-Element, dessen Eigenschaft geändert wurde </ param>
        /// <param name = "e"> Die Argumente für das Ereignis </ param>
        private static object OnValuePropertyUpdated(DependencyObject d, object value)
        {
            (Instance as AttachedPropertyBase<Parent, Property>)?.OnValueUpdated(d, value);
            (Instance as AttachedPropertyBase<Parent, Property>)?.ValueUpdated(d, value);
            return value;
        }

        /// <summary>
        /// Ruft die angehängte Eigenschaft ab
        /// </ summary>
        /// <param name = "d"> Das Element, von dem die Eigenschaft abgerufen werden soll </ param>
        /// <returns> </ returns>
        public static Property GetValue(DependencyObject d) => (Property)d.GetValue(ValueProperty);

        /// <summary>
        /// Setzt die angehängte Eigenschaft
        /// </ summary>
        /// <param name = "d"> Das Element, von dem die Eigenschaft abgerufen werden soll </ param>
        /// <param name = "value"> Der Wert, für den die Eigenschaft festgelegt werden soll</ param>
        public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty, value);

        #endregion

        #region Event Methods

        /// <summary>
        /// Die Methode, die aufgerufen wird, wenn eine angehängte Eigenschaft dieses Typs geändert wird
        /// </ summary>
        /// <param name = "sender"> Das Benutzeroberflächenelement, für das diese Eigenschaft geändert wurde. </ param>
        /// <param name = "e"> Die Argumente für dieses Ereignis </ param>
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        /// <summary>
        /// Die Methode, die aufgerufen wird, wenn eine angehängte Eigenschaft dieses Typs geändert wird, auch wenn der Wert identisch ist
        /// </ summary>
        /// <param name = "sender"> Das Benutzeroberflächenelement, für das diese Eigenschaft geändert wurde. </ param>
        /// <param name = "e"> Die Argumente für dieses Ereignis </ param>
        public virtual void OnValueUpdated(DependencyObject sender, object value) { }

        #endregion
    }
}
