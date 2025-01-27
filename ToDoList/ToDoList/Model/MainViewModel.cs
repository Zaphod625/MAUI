using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ToDoList;

public class MainVieModel:BaseViewModel
{
    public ObservableCollection<TodoItem> Items { get; set; }
    private int _completedTasks;
    public string CompletedTaskSummary => $"{CompletedTasks}/{Items.Count} tasks completed";//should this not be on the xaml side

    public ObservableCollection<TodoItem> Items { get; set; }

    public string CompletedTaskSummary => $"{Items.Count(item => item.IsCompleted)}/{Items.Count} tasks completed";

    public ICommand AddTaskCommand { get; }

}





















/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDoList
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<TodoItem> Items { get; set; }
        // Automatically updates the UI when the items are added or removed 

        private int _completedTasks;
        public string CompletedTaskSummary => $"{CompletedTasks}/{Items.Count} tasks completed";//should this not be on the xaml side
        //dynamic logic is centralised and testable
        //easily adjus the format
        //clean seperation of concerns
        public int CompletedTasks
        {
            get => _completedTasks;
            set
            {
                if (_completedTasks != value)
                {
                    _completedTasks = value;
                    OnPropertyChanged(nameof(CompletedTasks));
                    OnPropertyChanged(nameof(CompletedTaskSummary));
                }
            }
        }


        public MainPage()
        {
            InitializeComponent();
            Items = new ObservableCollection<TodoItem>
            {
            new TodoItem { Title = "Task 1", IsCompleted = false },
            new TodoItem { Title = "Task 2", IsCompleted = false },
            new TodoItem { Title = "Task 3", IsCompleted = false },
            new TodoItem { Title = "Task 4", IsCompleted = false },
            new TodoItem { Title = "Task 5", IsCompleted = false },
            new TodoItem { Title = "Task 6", IsCompleted = false },
            new TodoItem { Title = "Task 7", IsCompleted = false },
            };
            foreach (var item in Items)
            {
                item.PropertyChanged += OnItemPropertyChanged;
            }
            UpdateCompletedTasks();
            BindingContext = this;

        }
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TodoItem.IsCompleted))
            {
                UpdateCompletedTasks();
            }
        }
        private void UpdateCompletedTasks()
        {
            CompletedTasks = Items.Count(item => item.IsCompleted);
        }

    }

    //INotifyPropertyChanged ensures that the two way binding works correctly
    //notifies UI about property changes
    public class TodoItem : INotifyPropertyChanged
    {
        private bool _isCompleted;
        public string _title;
        //public string title { get; set; } doesn't support property change notifications so I have to do it manually.
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        //why not just do a {get;set;} here? This would be correct if we didn't have a OnPropertyChanged event
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;
                    OnPropertyChanged();
                }
            }

        }
        // not entirely sure whats happening here I know why I needed to create the OnPropertyChangedEvent but I don't really understand how it works.
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}