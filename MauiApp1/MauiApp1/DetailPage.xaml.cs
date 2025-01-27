using MauiApp1.ViewModel;
namespace MauiApp1;
//alloms me to delete the task on the main page
//holds it so that we know which item to delete
[QueryProperty(nameof(Task), "Task")]
public partial class DetailPage : ContentPage
{
    private MainViewModel _mainViewModel;
    private TaskItem _task;

    public DetailPage(MainViewModel mainViewModel)
    {
        InitializeComponent();
        _mainViewModel = mainViewModel;
        BindingContext = _mainViewModel; // Bind the same ViewModel
    }

    private async void CompletedTask_Button_Clicked(object sender, EventArgs e)
    {
        _mainViewModel.IncrementCompletedTasks(); // Increment the task count
        _mainViewModel.DeleteCommand.Execute(_task);
        await Shell.Current.GoToAsync(".."); // Navigate back to the previous page
    }
}
