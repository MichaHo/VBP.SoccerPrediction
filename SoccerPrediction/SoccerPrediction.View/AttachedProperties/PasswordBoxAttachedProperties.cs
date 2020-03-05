using SoccerPrediction.Helper;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace SoccerPrediction.View
{
    #region MonitorPassword
    /// <summary>
    /// die angehängte MonitorPassword-Eigenschaft für ein <see cref = "PasswordBox" />
    /// </ summary>
    public class MonitorPasswordProperty : AttachedPropertyBase<MonitorPasswordProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;

            if (passwordBox == null) return;

            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            if ((bool)e.NewValue)
            {
                HasTextProperty.SetValue(passwordBox);
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        /// <summary>
        /// wird ausgelöst, wenn sich der <see cref = "PasswordBox" /> Kennwortwert ändert
        /// </ summary>
        /// <param name = "sender"> </ param>
        /// <param name = "e"> </ param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            HasTextProperty.SetValue((PasswordBox)sender);
        }
    }

    /// <summary>
    /// die angehängte HasText-Eigenschaft für ein <see cref = "PasswordBox" />
    /// </ summary>
    public class HasTextProperty : AttachedPropertyBase<HasTextProperty, bool>
    {
        /// <summary>
        /// Setzt die HasText-Eigenschaft basierend darauf, ob der Aufrufer <see cref = "PasswordBox" /> einen beliebigen Text hat
        /// </ summary>
        /// <param name = "sender"> </ param>
        public static void SetValue(DependencyObject sender)
        {
            SetValue(sender, ((PasswordBox)sender).SecurePassword.Length > 0);
        }
    }
    #endregion

    #region SecurePassword

    public class SecurePasswordProperty : AttachedPropertyBase<SecurePasswordProperty, SecureString>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            var bindingMarshaller = passwordBox.GetValue(_passwordBindingMarshallerProperty) as PasswordBindingMarshaller;
            if (bindingMarshaller == null)
            {
                bindingMarshaller = new PasswordBindingMarshaller(passwordBox);
                passwordBox.SetValue(_passwordBindingMarshallerProperty, bindingMarshaller);
            }

            bindingMarshaller.UpdatePasswordBox(e.NewValue as SecureString);
        }

        private static readonly DependencyProperty _passwordBindingMarshallerProperty = DependencyProperty.RegisterAttached(
            "PasswordBindingMarshaller",
            typeof(PasswordBindingMarshaller),
            typeof(SecurePasswordProperty),
            new PropertyMetadata() );
        private class PasswordBindingMarshaller
        {
            private readonly PasswordBox _passwordBox;
            private bool _isMarshalling;

            public PasswordBindingMarshaller(PasswordBox passwordBox)
            {
                _passwordBox = passwordBox;
                _passwordBox.PasswordChanged += this.PasswordBoxPasswordChanged;
            }

            public void UpdatePasswordBox(SecureString newPassword)
            {
                if (_isMarshalling) return;
                _isMarshalling = true;
                try
                {
                    // Das Einrichten von SecuredPassword löst keine visuelle Aktualisierung aus, daher müssen wir die Password-Eigenschaft verwenden
                    _passwordBox.Password = newPassword.Unsecure();
                }
                finally
                {
                    _isMarshalling = false;
                }
            }

            private void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
            {
                if (_isMarshalling) return;

                _isMarshalling = true;
                try
                {
                    SetValue(_passwordBox, _passwordBox.SecurePassword.Copy());
                }
                finally
                {
                    _isMarshalling = false;
                }
            }
        }
    }
    #endregion

}


