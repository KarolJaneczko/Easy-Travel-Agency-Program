using Microsoft.UI.Xaml;
using Travel_Agency_APP.Pages;

namespace Travel_Agency_APP {
    public sealed partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            rootFrame.Navigate(typeof(LoginPage));
        }
    }
}
