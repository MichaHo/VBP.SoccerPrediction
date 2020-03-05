using SoccerPrediction.Helper;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace SoccerPrediction.View
{
    #region Focus Properties

    /// <summary>
    /// Fokussiert (Tastaturfokus) dieses Element beim Laden
    /// </ summary>
    public class IsFocusedProperty : AttachedPropertyBase<IsFocusedProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Control control)) return;
            control.Loaded += (s, se) => control.Focus();
        }
    }

    /// <summary>
    /// Fokussiert (Tastaturfokus) dieses Element, wenn true
    /// </ summary>
    public class FocusProperty : AttachedPropertyBase<FocusProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Control control)) return;

            if ((bool)e.NewValue) control.Focus();
        }
    }

    /// <summary>
    /// Fokussiert (Tastaturfokus) und wählt den gesamten Text in diesem Element aus, wenn true
    /// </ summary>
    public class FocusAndSelectProperty : AttachedPropertyBase<FocusAndSelectProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if ((sender is TextBoxBase control))
            {
                if ((bool)e.NewValue)
                {
                    control.Focus();
                    control.SelectAll();
                }
            }
            if ((sender is PasswordBox pass))
            {
                if ((bool)e.NewValue)
                {
                    pass.Focus();
                    pass.SelectAll();
                }
            }
        }
    }
    #endregion

    
}
