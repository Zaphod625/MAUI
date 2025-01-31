using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.Input;
using Menu.Models;
using System.Windows.Input;
//ViewModels contain the logic and properties for binding data to the view
namespace Menu.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MenusItem> Items { get; set; } // Task list
        private const string HardCodedPassword = "1234";
        private int _completedTasks;
        public double _total;
        public double Total
        {
            get => _total;
            set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public string CompletedTaskSummary => $"{CompletedTasks}/{TotalTasks} items selected";

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
        private double _newTaskPrice;
        public double NewTaskPrice
        {
            get => _newTaskPrice;
            set
            {
                if (_newTaskPrice != value)
                {
                    _newTaskPrice = value;
                    OnPropertyChanged(nameof(NewTaskPrice));
                }
            }
        }
        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand NavigateToCartCommand { get; }
        public ICommand SignOutCommand { get; }
        public MainViewModel()
        {
            Items = new ObservableCollection<MenusItem>
            {
                new MenusItem { Title = "Burgers", IsCompleted = false,Currency='R', Price=89.90 },
                new MenusItem { Title = "Fries", IsCompleted = false,Currency='R', Price=34.50 },
                new MenusItem { Title = "Milkshakes", IsCompleted = false,Currency='R', Price =64.90},
                new MenusItem { Title = "Dessert", IsCompleted = false, Currency = 'R', Price=74.0},
                new MenusItem { Title = "Combos", IsCompleted = false, Currency='R', Price=129.90 },
                new MenusItem { Title = "Salads", IsCompleted = false, Currency = 'R', Price=39.90 },
                new MenusItem { Title = "Kids", IsCompleted = false, Currency='R', Price = 55.0}
            };

            AddTaskCommand = new Command(AddTask);
            DeleteTaskCommand = new Command<MenusItem>(DeleteTask);
            NavigateToCartCommand = new Command(OnNavigateToCart);
            SignOutCommand = new Command(() => Shell.Current.GoToAsync("//LoginPage"));
            foreach (var item in Items)
            {
                item.PropertyChanged += OnItemPropertyChanged;
            }

            //Items.CollectionChanged += (s, e) => UpdateCompletedTasks(); this did not work to change the running total of manually added entries
            UpdateCompletedTasks();
            //Why is it that the add task command was outside the constructor but the delete command is inside the constructor

        }

        private async void OnNavigateToCart()
        {
            var selectedItems = Items.Where(item => item.IsCompleted).ToList();
            var serializedItems = System.Text.Json.JsonSerializer.Serialize(selectedItems);

            var navigationParameter = $"cartItems={Uri.EscapeDataString(serializedItems)}";

            // Use absolute route with "///" to prevent the relative routing exception
            await Shell.Current.GoToAsync($"///CartPage?{navigationParameter}");
        }



        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MenusItem.IsCompleted))
            {
                UpdateCompletedTasks();
            }
        }

        private void UpdateCompletedTasks()
        {
            CompletedTasks = Items.Count(item => item.IsCompleted);
            TotalTasks = Items.Count;
            Total = 0;
            //probably not very efficient since calculation will have to be repeated every time UpdateCompletedTasks is called
            // land up counting the same thing multiple times if just one thing changes in the list
            foreach (var item in Items)
            {
                if (item.IsCompleted)
                {
                    Total += item.Price;
                }
            }
        }

        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                //manually added tasks did not have their PropertyChanged event connected to the OnItemPropertyChanged So when the IsCompleted property of the manual entry was changed it did not update the running total
                
                    var newTask = new MenusItem { Title = NewTaskTitle, IsCompleted = false, Price = NewTaskPrice };
                    newTask.PropertyChanged += OnItemPropertyChanged;
                    Items.Add(newTask);
               
                    // must empty the entry field after it has been added to the list just quality of life thing
                    NewTaskTitle = string.Empty;
                    NewTaskPrice = 0;
                    UpdateCompletedTasks(); //Don't need this since the OnItemPropertyChanged event triggers the UpdateCompletedTasks method
                    //this was redundant and didn't work why????????
            }
        }
        //Having probelms here with the delete task method
        private void DeleteTask(MenusItem item)
        {
            if (item != null && Items.Contains(item))
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
