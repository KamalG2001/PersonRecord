using System.ComponentModel;

namespace PersonRecord.Models
{
    public class User : INotifyPropertyChanged
    {
        public User()
        {

        }
         private string? _name;
         public string? Name
         {
            get => _name;
             set
             {
                 if (_name == value) return;
                _name = value;
                OnPropertyChanged(nameof(Name));
             }
         }

        private string? _surname;
        public string? Surname
        {
            get => _surname;
            set
            {
                if (_surname == value) return;
                _surname = value;
                 OnPropertyChanged(nameof(Surname));
            }
        }

        private int? _age;
        public int? Age
        {
            get => _age;
            set
            {
                if (_age == value) return;
                _age = value;
              OnPropertyChanged(nameof(Age));
            }
        }

        private string? _job;
        public string? Job
        {
            get => _job;
            set
            {
                if (_job == value) return;
                _job = value;
                OnPropertyChanged(nameof(Job));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  
}
