using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Data;
using HospitalProject.Model;



namespace HospitalProject.ViewModel
{

    public class MainWindowViewModel : BaseViewModel
    {
        public static List<DbObstegenyaModel> dbObstegenyaModel = null;

        private Sourting sortSource;

        private List<DbObstegenyaModel> data = null;

        private string request;

        public MainWindowViewModel()
        {

        }

        #region Property
        public int SortSource
        {
            get { return (int)sortSource; }
            set
            {
                sortSource = (Sourting)value;
                OnPropertyChanged("sortSource");
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
            get
            {
                if (dbObstegenyaModel == null)
                {
                    dbObstegenyaModel = new DbObstegenyaModel().GetData(); ;
                    data = dbObstegenyaModel;
                    Loger.Logining.logger.Trace("Данні обстеження загрузилися з бази");
                }
                return data;
            }
            set
            {
                data = value;
                OnPropertyChanged("Data");
            }
        }

        #endregion

        #region Command
        private ICommand clickCommand;
        public ICommand ClickCommand => clickCommand ?? (clickCommand = 
            new CommandHandler(() =>
            {
                AddObstegenia addObstegenyaView = new AddObstegenia();
                addObstegenyaView.ShowDialog();
                data = dbObstegenyaModel;
                OnPropertyChanged("Data");
            }, _canExecute));

        private ICommand find;
        public ICommand Find => find ?? (find = new CommandHandler(FindObj, _canExecute));
        #endregion

        #region Logic
        private void FindObj()
        {
            if (Request == null) return;
            data = null;
            string request = Request.ToLower();
            OnPropertyChanged("Data");
            switch (sortSource)
            {
                case Sourting.all:

                    data = dbObstegenyaModel.Where(s => s.Doctor.ToLower().Contains(request)
                                                    || s.Date.ToShortDateString().ToLower().Contains(request)
                                                    || s.DoctorName.ToLower().Contains(request)
                                                    || s.DoctorProf.ToLower().Contains(request)
                                                    || s.Patient.ToLower().Contains(request)
                                                    || s.PatientName.ToLower().Contains(request)).ToList<DbObstegenyaModel>();
                    break;
                case Sourting.namePatient:
                    data = dbObstegenyaModel.Where(s => s.PatientName.ToLower().Contains(request)).ToList<DbObstegenyaModel>();
                    break;
                case Sourting.firstNamePatient:
                    data = dbObstegenyaModel.Where(s => s.Patient.ToLower().Contains(request)).ToList<DbObstegenyaModel>();
                    break;
                case Sourting.firstNameDoctor:
                    data = dbObstegenyaModel.Where(s => s.DoctorName.ToLower().Contains(request)).ToList<DbObstegenyaModel>();
                    break;
                case Sourting.nameDoctor:
                    data = dbObstegenyaModel.Where(s => s.Doctor.ToLower().Contains(request)).ToList<DbObstegenyaModel>();
                    break;
            }
            OnPropertyChanged("Data");
            Loger.Logining.logger.Info("Відбувся пошук");
        }
        #endregion
    }

}