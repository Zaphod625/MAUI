using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Menu.Models;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Menu.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MenusItem> CartItems { get; set; } = new ObservableCollection<MenusItem>();
        public double Total => CartItems.Sum(item => item.Price);

        public ICommand NavigateHomeCommand { get; }

        public CartViewModel()
        {
            NavigateHomeCommand = new Command(OnNavigateHome);
        }

        private async void OnNavigateHome()
        {
            // Absolute route navigation back to the home page
            await Shell.Current.GoToAsync("///MainPage");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
