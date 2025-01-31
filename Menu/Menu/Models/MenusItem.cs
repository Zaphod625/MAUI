using System.ComponentModel;
using System.Runtime.CompilerServices;
// Models contain the data related classes 
namespace Menu.Models
{
    public class MenusItem : INotifyPropertyChanged
    {
        private bool _isCompleted;
        private string _title;
        private double _price;
        private char _curency;

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

        public double Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged();
                }
            }
        }
        public char Currency
        {
            get => _curency;
            set
            {
                if (_curency != value)
                {
                    _curency = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}