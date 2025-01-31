using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToDoList.ViewModels;
namespace ToDoList
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }

}

// Models contain data related classes(data representation)
// ViewModels contain classes that handle the logic and properties for the data binding to the view(managing UI logic and data representation)
//The seperation keeps the project organised, maintainable and scalable as it grows.

//->clear structure
//->easy to maintain
//->easy to test

