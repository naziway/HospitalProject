using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Data;
using HospitalProject.Data;
using HospitalProject.Model;
using HospitalProject.View;


namespace HospitalProject.ViewModel
{

    public class MainWindowViewModel : BaseViewModel
    {
        private Sourting sortSource;

        private List<DbObstegenyaModel> data = null;

        private string request;

        public MainWindowViewModel()
        {
            DbObstegenya.addNewObstegenya += DbObstegenya_addNewObstegenya;
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
                if (data == null)
                {
                    data = DbObstegenya.ObstegenyaList;

                    if (data == null)
                    {
                        MessageBox.Show("Не встановлено з'єднання з базою.....Па па!!!");
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();

                        });
                    }
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
                addObstegenyaView.Show();
            }, _canExecute));

        private ICommand find;
        public ICommand Find => find ?? (find = new CommandHandler(FindObj, _canExecute));
        #endregion

        #region Logic
        private void DbObstegenya_addNewObstegenya(object sender, DbObstegenyaModel e)//Оброботчик доданого обстеження
        {
            OnPropertyChanged("Data");
        }
        private void FindObj()
        {
            if (Request == null) return;
            data = null;
            string request = Request.ToLower();
            OnPropertyChanged("Data");
            switch (sortSource)
            {
                case Sourting.all:

                    data = DbObstegenya.ObstegenyaList.Where(s => s.Doctor.ToLower().Contains(request)
                                                    || s.Date.ToShortDateString().ToLower().Contains(request)
                                                    || s.DoctorName.ToLower().Contains(request)
                                                    || s.DoctorProf.ToLower().Contains(request)
                                                    || s.Patient.ToLower().Contains(request)
                                                    || s.PatientName.ToLower().Contains(request)).ToList<DbObstegenyaModel>();
                    break;
                case Sourting.namePatient:
                    data = DbObstegenya.ObstegenyaList.Where(s => s.PatientName.ToLower().Contains(request)).ToList<DbObstegenyaModel>();
                    break;
                case Sourting.firstNamePatient:
                    data = DbObstegenya.ObstegenyaList.Where(s => s.Patient.ToLower().Contains(request)).ToList<DbObstegenyaModel>();
                    break;
                case Sourting.firstNameDoctor:
                    data = DbObstegenya.ObstegenyaList.Where(s => s.DoctorName.ToLower().Contains(request)).ToList<DbObstegenyaModel>();
                    break;
                case Sourting.nameDoctor:
                    data = DbObstegenya.ObstegenyaList.Where(s => s.Doctor.ToLower().Contains(request)).ToList<DbObstegenyaModel>();
                    break;
            }
            OnPropertyChanged("Data");
            Loger.Logining.logger.Info("Відбувся пошук");
        }

        #endregion

    }

}