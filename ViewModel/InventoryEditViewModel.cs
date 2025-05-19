using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Variant2.Models;

namespace Variant2.ViewModels
{
    public class InventoryEditViewModel : INotifyPropertyChanged
    {
        private readonly Inventory? _original;

        public string Article { get; set; } = "";
        public string Name { get; set; } = "";
        public string? Type { get; set; }
        public string? Description { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public int Status { get; set; } = 0;

        public InventoryEditViewModel(Inventory? inventory = null)
        {
            _original = inventory;
            if (inventory != null)
            {
                Article = inventory.Article;
                Name = inventory.Name;
                Type = inventory.Type;
                Description = inventory.Description;
                ReleaseDate = inventory.ReleaseDate;
                Status = inventory.Status;
            }
        }

        public bool Save()
        {
            if (string.IsNullOrWhiteSpace(Article) || string.IsNullOrWhiteSpace(Name))
                return false;

            using var db = new Variant2Context();

            if (_original == null)
            {
                var newItem = new Inventory
                {
                    Article = Article,
                    Name = Name,
                    Type = Type,
                    Description = Description,
                    ReleaseDate = ReleaseDate,
                    Status = Status
                };
                db.Inventories.Add(newItem);
            }
            else
            {
                var existing = db.Inventories.Find(_original.Id);
                if (existing == null) return false;

                existing.Article = Article;
                existing.Name = Name;
                existing.Type = Type;
                existing.Description = Description;
                existing.ReleaseDate = ReleaseDate;
                existing.Status = Status;
            }

            db.SaveChanges();
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
