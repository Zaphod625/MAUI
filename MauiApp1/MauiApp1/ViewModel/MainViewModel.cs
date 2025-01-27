using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Javax.Annotation.Processing;
using static Android.Graphics.ColorSpace;
using static Android.Service.Notification.NotificationListenerService;
namespace MauiApp1.ViewModel;


//Code behind for the main page
//using these to determine whether to cross out a to do item or not
public class TaskItem : ObservableObject 
{
    [ObservableProperty]
    private string text;
    [ObservableProperty]
    private bool isCompleted;
}

public partial class MainViewModel:ObservableObject
{
    IConnectivity connectivity;

    [ObservableProperty]
    //With[ObservableProperty], the CommunityToolkit.Mvvm library automatically generates:
    //A private backing field named completedTasks(lowercase).
    //A public property named CompletedTasks(uppercase), which raises PropertyChanged.
    private int completedTasks;

    public MainViewModel() 
    {
        Items = new ObservableCollection<TodoItem>
        {
                new TodoItem { Title = "Task 1", IsCompleted = false },
                new TodoItem { Title = "Task 2", IsCompleted = false },
                new TodoItem { Title = "Task 3", IsCompleted = false }
        };
        connectivity =Connectivity.Current;
    }

    [ObservableProperty]
    ObservableCollection<TaskItem> items;

    [ObservableProperty]
    string text;

    [RelayCommand]//Used to be ICommand
    async Task Add()
    {
        if(string.IsNullOrWhiteSpace(Text))
        {
            return;
        }
        if(connectivity.NetworkAccess!=NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Uh Oh"," no internt", "OK");
            return;
        }
    
        Items.Add(Text);
        // add our item
        Text = string.Empty;
    }

    [RelayCommand]
    void Delete(string s)
    {
        if (items.Contains(s))
        {
            Items.Remove(s);

        }
    }
    [RelayCommand]
    async Task Tap(TaskItem task)
    {
        if (task != null)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}",
            new Dictionary<string, object> { { "Task", task } });
        }

        //new Dictionary<string ,object>() alternate method create bucket dictionary
    }

    public void IncrementCompletedTasks()
    {   
        CompletedTasks++;
    }

}
