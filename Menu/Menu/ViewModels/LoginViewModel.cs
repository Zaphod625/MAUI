using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using menu.Models;

namespace menu.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly LoginModel _loginModel;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string errorMessage;

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            _loginModel = new LoginModel();
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        private async void ExecuteLogin()
        {
            if (_loginModel.Authenticate(Username, Password))
            {
                ErrorMessage = string.Empty;
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                ErrorMessage = "Invalid username or password";
            }
        }
    }
}

