using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Models;
using System.Windows.Input;
//ViewModels contain the logic and properties for binding data to the view
namespace ToDoList.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TodoItem> Items { get; set; } // Task list

        private int _completedTasks;
        public string CompletedTaskSummary => $"{CompletedTasks}/{TotalTasks} tasks completed";

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
        private int _totalTasks;
        public int TotalTasks
        {
            get => _totalTasks;
            set
            {
                if (_totalTasks != value)
                {
                    _totalTasks = value;
                    OnPropertyChanged(nameof(TotalTasks));
                    OnPropertyChanged(nameof(CompletedTaskSummary));
                }
            }
        }

        // adding a new task here 
        private string _newTaskTitle;
        public string NewTaskTitle
        {
            get => _newTaskTitle;
            set
            {
                if (_newTaskTitle != value)
                {
                    _newTaskTitle = value;
                    OnPropertyChanged(nameof(NewTaskTitle));
                }
            }
        }
        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public MainViewModel()
        {
            Items = new ObservableCollection<TodoItem>
            {
                new TodoItem { Title = "Task 1", IsCompleted = false },
                new TodoItem { Title = "Task 2", IsCompleted = false },
                new TodoItem { Title = "Task 3", IsCompleted = false },
                new TodoItem { Title = "Task 4", IsCompleted = false },
                new TodoItem { Title = "Task 5", IsCompleted = false },
                new TodoItem { Title = "Task 6", IsCompleted = false },
                new TodoItem { Title = "Task 7", IsCompleted = false }
            };

            AddTaskCommand = new Command(AddTask);
            DeleteTaskCommand = new Command<TodoItem>(DeleteTask);

            foreach (var item in Items)
            {
                item.PropertyChanged += OnItemPropertyChanged;
            }

            //Items.CollectionChanged += (s, e) => UpdateCompletedTasks(); this did not work to change the running total of manually added entries
            UpdateCompletedTasks();
            //Why is it that the add task command was outside the constructor but the delete command is inside the constructor

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
            TotalTasks = Items.Count;
        }

        private void AddTask()
        {
            if(!string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                //manually added tasks did not have their PropertyChanged event connected to the OnItemPropertyChanged So when the IsCompleted property of the manual entry was changed it did not update the running total
                var newTask =new TodoItem { Title = NewTaskTitle, IsCompleted = false };
                newTask.PropertyChanged += OnItemPropertyChanged;
                Items.Add(newTask);
                // must empty the entry field after it has been added to the list just quality of life thing
                NewTaskTitle = string.Empty;
                UpdateCompletedTasks(); //Don't need this since the OnItemPropertyChanged event triggers the UpdateCompletedTasks method
                //this was redundant and didn't work why????????
            }
        }
        //Having probelms here with the delete task method
        private void DeleteTask(TodoItem item)
        {
            if(item!=null && Items.Contains(item))
            {
                Console.WriteLine("Item Deleted");
                Items.Remove(item);
                UpdateCompletedTasks();
            }
            Console.WriteLine("Item Not Deleted");
                
        }  

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}