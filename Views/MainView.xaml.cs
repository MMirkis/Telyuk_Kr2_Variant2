using System.Windows;
using Variant2.Models;
using Variant2.ViewModels;

namespace Variant2.Views
{
    public partial class MainView : Window
    {
        public MainView(User user)
        {
            InitializeComponent();
            DataContext = new MainViewModel(user);
        }
    }
}
