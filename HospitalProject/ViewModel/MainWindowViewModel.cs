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

        private Sourting sortSource;

        private List<DbObstegenyaModel> data;

        private string request;

        public Sourting SortSource
        {
            get { return sortSource; }
            set
            {
                sortSource = value;
                OnPropertyChanged("sortSource");
            }
        }

        public string Request
        {
            get { return request; }
            set
            {
                if (value != request&& value!="")
                {
                    request = value;
                    OnPropertyChanged("Request");
                    Find();
                }

            }
        }

        private void Find()
        {
            Data = dbObstegenyaModel.GetData().Where(s => s.Doctor.Contains(Request)).ToList<DbObstegenyaModel>();


        }

        public List<DbObstegenyaModel> Data
        {
            get { return data; }
            set
            {
                data = value;
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