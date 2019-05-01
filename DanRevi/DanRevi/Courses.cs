using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DanRevi
{
    public class Courses : INotifyPropertyChanged
    {
        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string target_audience { get; set; }

        public string location { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }

        public string host { get; set; }

        public string startEndDate { get { return "Start: " + start + " Slut: " + end; } }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
