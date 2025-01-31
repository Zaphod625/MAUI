using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Menu.ViewModels
{
    public class SplitPageViewModel : BindableObject
    {
        public ICommand AddNewItemCommand { get; }
        public ICommand GoToCartCommand { get; }

        public SplitPageViewModel()
        {
            AddNewItemCommand = new Command(OnAddNewItem);
            GoToCartCommand = new Command(OnGoToCart);
        }

        private async void OnAddNewItem()
        {
            await Shell.Current.GoToAsync("AddItemPage");
        }

        private async void OnGoToCart()
        {
            await Shell.Current.GoToAsync("CartPage");
        }
    }
}
