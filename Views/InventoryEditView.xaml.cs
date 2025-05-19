using System.Windows;
using Variant2.Models;
using Variant2.ViewModels;

namespace Variant2.Views
{
    public partial class InventoryEditView : Window
    {
        public InventoryEditViewModel ViewModel { get; }

        public InventoryEditView(Inventory? inventory = null)
        {
            InitializeComponent();
            ViewModel = new InventoryEditViewModel(inventory);
            DataContext = ViewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Save())
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка при сохранении. Проверьте данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
