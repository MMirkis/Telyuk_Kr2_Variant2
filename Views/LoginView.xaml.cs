using System.Windows;
using Variant2.ViewModels;
using Variant2.Views;

namespace Variant2.Views
{
    public partial class LoginView : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginView()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            DataContext = _viewModel;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var password = PasswordBox.Password;

            if (_viewModel.TryLogin(password))
            {
                var main = new MainView(_viewModel.CurrentUser!);
                main.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var register = new RegisterView();
            register.ShowDialog();
        }
    }
}
