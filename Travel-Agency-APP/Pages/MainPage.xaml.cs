using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Travel_Agency_APP.Utils;

namespace Travel_Agency_APP.Pages {
    public sealed partial class MainPage : Page {
        public MainPage() {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            CurrentUserLogin.Text = AppProperties.Login;
        }
    }
}
