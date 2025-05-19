using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Variant2.Models;

namespace Variant2.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _login = "";
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public User? CurrentUser { get; private set; }

        public bool TryLogin(string password)
        {
            using var db = new Variant2Context();
            var user = db.Users.FirstOrDefault(u => u.Login == Login && u.Password == password);
            if (user != null)
            {
                CurrentUser = user;
                return true;
            }

            return false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
