using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Menu.Models;
using Menu.ViewModels;

namespace Menu.ViewModels;

public class LoginPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private ObservableCollection<UserData> Users { get; set; }

    private string _usernameInput;
    public string UsernameInput
    {
        get => _usernameInput;
        set
        {
            if (_usernameInput != value)
            {
                _usernameInput = value;
                OnPropertyChanged(nameof(UsernameInput));
            }
        }
    }

    private string _passwordInput;
    public string PasswordInput
    {
        get => _passwordInput;
        set
        {
            if (_passwordInput != value)
            {
                _passwordInput = value;
                OnPropertyChanged(nameof(PasswordInput));
            }
        }
    }

    public ICommand NavigateToMainCommand { get; }

    public LoginPageViewModel()
    {
        Users = new ObservableCollection<UserData>
        {
            new UserData { UserName = "admin", Password = "admin" },
            new UserData { UserName = "user", Password = "user" }
        };
        NavigateToMainCommand = new Command(async () => await Login());
    }

    private async Task Login()
    {
        if (Users.Any(x => x.UserName == UsernameInput && x.Password == PasswordInput))
        {
            await Task.Delay(100);  // Small delay to allow UI initialization

            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Navigation failed.", "OK");
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Incorrect username or password", "OK");
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

