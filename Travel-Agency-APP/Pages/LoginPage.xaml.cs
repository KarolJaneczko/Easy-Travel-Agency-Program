// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
namespace Travel_Agency_APP.Pages {
    public sealed partial class LoginPage : Page {
        public LoginPage() {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            ErrorMessage.Text = "eee";
        }
    }
}
