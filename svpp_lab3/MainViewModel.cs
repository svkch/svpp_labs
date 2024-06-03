using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace svpp_lab3
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private double _lineThickness = 0.5;
        public double LineThickness
        {
            get { return _lineThickness; }
            set
            {
                if (_lineThickness != value)
                {
                    _lineThickness = value;
                    OnPropertyChanged(nameof(LineThickness));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
