namespace Menu.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        // ths below is not really using the mvvm model structure needs to be tidied up.
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Example check, replace with actual authentication logic
            if (UsernameEntry.Text == "admin" && PasswordEntry.Text == "password")
            {
                await Navigation.PushAsync(new MainPage());
                UsernameEntry.Text = string.Empty;
                PasswordEntry.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid credentials", "OK");
            }
        }
    }
}
