using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Data;

namespace HospitalProject.ViewModel
{
    public class AddObstegenyaViewModel : BaseViewModel
    {
        private DbPatientModel dbPatientModel = new DbPatientModel();
        private DbDoctorModel dbDoctorModel = new DbDoctorModel();
        private List<string> chooseDoctor;
        private List<string> choosePatient;
        private string doctor;
        private string patient;
        private string check;
        private int docId;
        private int patId;
        private DateTime date = DateTime.Now;
        private DateTime timeTo = DateTime.Now;
        private DateTime timeWith = DateTime.Now;

        public AddObstegenyaViewModel()
        {

            ChooseDoctor = dbDoctorModel.GetData().Select(s => s.FirstName.TrimEnd() + " " + s.LastName.TrimEnd() + " " + s.Posada.TrimEnd()).ToList<string>();
            ChoosePatient = dbPatientModel.GetData().Select(s => s.FirstName.TrimEnd() + " " + s.LastName.TrimEnd() + " " + s.DateBirth.ToShortDateString()
                                                    .TrimEnd()).ToList<string>();
            DocId = 0;
            PatId = 0;

        }
        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() =>
                {
                    CheckAndAdd();
                }, _canExecute)); ;
            }
        }

        private void CheckAndAdd()
        {

            Check = $"doc={DocId} pat={PatId} data={Date} timewith={TimeWith} timeto={TimeTo} ";
            AddObstegenia addObstegenyaView = new AddObstegenia();

            addObstegenyaView.Show();
        }

        public string Check
        {
            get { return check; }
            set
            {
                check = value;
                OnPropertyChanged("Check");
            }
        }
        public int DocId
        {
            get { return docId; }
            set
            {
                docId = value;
                OnPropertyChanged("DocId");
            }
        }

        public int PatId
        {
            get { return patId; }
            set
            {
                patId = value;
                OnPropertyChanged("PatId");
            }
        }
        public List<string> ChooseDoctor
        {
            get { return chooseDoctor; }
            set
            {
                chooseDoctor = value;
                OnPropertyChanged("ChooseDoctor");
            }
        }

        public List<string> ChoosePatient
        {
            get { return choosePatient; }
            set
            {
                choosePatient = value;
                OnPropertyChanged("ChoosePatient");
            }
        }

        public string Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }

        public string Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        public string Date
        {
            get { return date.ToShortDateString(); }
            set
            {
                try
                {
                    date = DateTime.Parse(value);
                    OnPropertyChanged("Date");
                }
                catch (Exception)
                {
                    date = date;
                    OnPropertyChanged("Date");
                }


            }
        }
        public string TimeTo
        {
            get { return timeTo.ToShortTimeString(); }
            set
            {
                try
                {
                    timeTo = DateTime.Parse(value);
                    OnPropertyChanged("TimeTo");
                }
                catch (Exception)
                {
                    timeTo = timeTo;
                    OnPropertyChanged("TimeTo");

                }
            }
        }
        public string TimeWith
        {
            get { return timeWith.ToShortTimeString(); }
            set
            {
                try
                {
                    timeWith = DateTime.Parse(value);
                    OnPropertyChanged("TimeWith");
                }
                catch (Exception)
                {
                    timeWith = timeWith;
                    OnPropertyChanged("TimeWith");
                }

            }
        }
    }
}