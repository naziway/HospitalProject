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
        public static  List<DbObstegenyaModel> dbObstegenyaModel = null;

        private Sourting sortSource;

        private List<DbObstegenyaModel> data = null;

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
                if (value != request && value != "")
                {
                    request = value;
                    OnPropertyChanged("Request");
                    Find();
                }
            }
        }
        public List<DbObstegenyaModel> Data
        {
            get
            {
                if (data == null)
                {
                    dbObstegenyaModel = new DbObstegenyaModel().GetData();;
                    data = dbObstegenyaModel;
                }
                return data;
            }
            set
            {
                data = value;
                OnPropertyChanged("Data");
            }
        }
        public MainWindowViewModel()
        {


        }
        private void Find()
        {
            Data = dbObstegenyaModel.Where(s => s.Doctor.Contains(Request)).ToList<DbObstegenyaModel>();
        }

    }

}