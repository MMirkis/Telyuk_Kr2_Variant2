using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Variant2.Models;
using Variant2.Views;

namespace Variant2.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly User _currentUser;

        public ObservableCollection<Inventory> Inventories { get; set; }

        public Inventory? SelectedInventory { get; set; }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand IssueCommand { get; }
        public ICommand ReturnCommand { get; }

        public MainViewModel(User user)
        {
            _currentUser = user;

            using var db = new Variant2Context();
            Inventories = new ObservableCollection<Inventory>(db.Inventories.ToList());

            AddCommand = new RelayCommand(_ => AddInventory());
            EditCommand = new RelayCommand(_ => EditInventory(), _ => SelectedInventory != null);
            DeleteCommand = new RelayCommand(_ => DeleteInventory(), _ => SelectedInventory != null);
            IssueCommand = new RelayCommand(_ => IssueInventory(), _ => SelectedInventory != null && SelectedInventory.Status == 0);
            ReturnCommand = new RelayCommand(_ => ReturnInventory(), _ => SelectedInventory != null && SelectedInventory.Status == 1);
        }

        private void AddInventory()
        {
            var window = new InventoryEditView();
            if (window.ShowDialog() == true)
                Refresh();
        }

        private void EditInventory()
        {
            if (SelectedInventory == null) return;
            var window = new InventoryEditView(SelectedInventory);
            if (window.ShowDialog() == true)
                Refresh();
        }

        private void DeleteInventory()
        {
            if (SelectedInventory == null) return;
            using var db = new Variant2Context();
            var item = db.Inventories.Find(SelectedInventory.Id);
            if (item != null)
            {
                db.Inventories.Remove(item);
                db.SaveChanges();
                Refresh();
            }
        }

        private void IssueInventory()
        {
            if (SelectedInventory == null) return;
            using var db = new Variant2Context();
            var item = db.Inventories.Find(SelectedInventory.Id);
            if (item != null)
            {
                item.UserId = _currentUser.Id;
                item.Status = 1;
                db.SaveChanges();
                Refresh();
            }
        }

        private void ReturnInventory()
        {
            if (SelectedInventory == null) return;
            using var db = new Variant2Context();
            var item = db.Inventories.Find(SelectedInventory.Id);
            if (item != null)
            {
                item.UserId = null;
                item.Status = 0;
                db.SaveChanges();
                Refresh();
            }
        }

        private void Refresh()
        {
            using var db = new Variant2Context();
            Inventories.Clear();
            foreach (var i in db.Inventories.ToList())
                Inventories.Add(i);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
