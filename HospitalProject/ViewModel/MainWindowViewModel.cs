using System.Collections.Generic;
using System.Linq;
using Data;
using HospitalProject.Model;
using HospitalProject.NLogger;


namespace HospitalProject.ViewModel
{
   
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
                    Logining.logger.Trace("Данні обстеження загрузилися");
                    Logining.logger.Debug("logger.Debug");
                    Logining.logger.Info("logger.Info");
                    Logining.logger.Warn("logger.Warn");
                    Logining.logger.Error("logger.Error");
                    Logining.logger.Fatal("logger.Fatal");
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