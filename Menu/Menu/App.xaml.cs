using Menu.ViewModels;
using Menu.Views;

namespace Menu
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }

}