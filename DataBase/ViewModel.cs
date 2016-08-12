using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HospitalProject.Annotations;

namespace DataBase
{
    public class ViewModel//:INotifyPropertyChanged
    {





        public List<DbObstegenyaModel> Doc { get; set; }


        public ViewModel()
        {
           Doc= new DbObstegenyaModel().GetData();

        }

       // public event PropertyChangedEventHandler PropertyChanged;

      //  [NotifyPropertyChangedInvocator]
      //  protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
       // {
       //     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
       // }
    }
}