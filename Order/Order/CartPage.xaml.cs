using Menu.Models;
using Menu.ViewModels;

namespace Menu
{
    [QueryProperty(nameof(CartItemsString), "cartItems")]
    public partial class CartPage : ContentPage
    {
        private string _cartItemsString;

        public string CartItemsString
        {
            get => _cartItemsString;
            set
            {
                _cartItemsString = value;
                LoadCartItems(value);
            }
        }

        public CartPage()
        {
            InitializeComponent();
            BindingContext = new CartViewModel();
        }

        // so this needs to be in the xaml.cs filel as it is transporting the items data between pages
        private void LoadCartItems(string cartItemsJson)
        {
            if (!string.IsNullOrEmpty(cartItemsJson))
            {
                var cartItems = System.Text.Json.JsonSerializer.Deserialize<List<MenusItem>>(cartItemsJson);

                if (cartItems != null)
                {
                    var cartViewModel = new CartViewModel();
                    foreach (var item in cartItems)
                    {
                        cartViewModel.CartItems.Add(item);
                    }

                    BindingContext = cartViewModel;
                }
            }
        }
    }
}

