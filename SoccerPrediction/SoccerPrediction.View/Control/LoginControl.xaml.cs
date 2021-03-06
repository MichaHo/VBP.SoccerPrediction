﻿using SoccerPrediction.ViewModel;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoccerPrediction.View
{
    /// <summary>
    /// Interaktionslogik für LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl, IHavePassword
    {
        public LoginControl()
        {
            InitializeComponent();
        }

        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
