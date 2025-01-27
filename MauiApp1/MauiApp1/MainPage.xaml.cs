using MauiApp1.ViewModel;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;

        }
        private async void OnNavigationToDetailPage(object sender, EventArgs e)
        {
            var viewModel = BindingContext as ViewModel.MainViewModel;
            if(viewModel!=null)
            {
                await Navigation.PushAsync(new DetailPage(viewModel));
            }
        }
    }

}
