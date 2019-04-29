using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DanRevi
{
    public class Deadlines : INotifyPropertyChanged
    {
        public int id { get; set; }

        public string description { get; set; }

        public string name { get; set; }

        public DateTime date { get; set; }

        //public string wholeDL { get { return name + " " + description + " " + date; } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
