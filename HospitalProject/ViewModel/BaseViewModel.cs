using System.ComponentModel;
using System.Runtime.CompilerServices;
using HospitalProject.Annotations;

namespace HospitalProject.ViewModel
{
    public class BaseViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}