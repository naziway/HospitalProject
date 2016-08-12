using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using Data;

//using HospitalProject.Model;

namespace HospitalProject.ViewModel
{
    public enum Sourting
    {
        all,
        namePat
    }
    public class MainWindowViewModel : BaseViewModel
    {
        private DbObstegenyaModel dbObstegenyaModel;

        private Sourting source;

        private List<DbObstegenyaModel> Obsteg;

        private string request;

        public Sourting Source
        {
            get { return source; }
            set
            {
                source = value;
                OnPropertyChanged("Source");
            }
        }

        public string Request
        {
            get { return request; }
            set
            {
                request = value;
                OnPropertyChanged("Request");
            }
        }
        public List<DbObstegenyaModel> Data
        {
            get { return Obsteg; }
            set
            {
                Obsteg = value;
                OnPropertyChanged("Data");
            }
        }


        public MainWindowViewModel()
        {
            dbObstegenyaModel = new DbObstegenyaModel();
            Data = dbObstegenyaModel.GetData();
            
        }

        

    }
    
}