using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Data;

namespace HospitalProject.ViewModel
{
    public class AddObstegenyaViewModel : BaseViewModel
    {
        private List<DbDoctorModel> doctor;
        private List<DbPatientModel> patient;
        private List<string> chooseDoctor;
        private List<string> choosePatient;
        private string check;
        private int docId;
        private int patId;
        private DateTime date = DateTime.Now;
        private TimeSpan timeWith = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        private TimeSpan timeTo = new TimeSpan(DateTime.Now.AddMinutes(10).Hour, DateTime.Now.AddMinutes(10).Minute,
                                         DateTime.Now.AddMinutes(10).Second);

        public AddObstegenyaViewModel()
        {
            DocId = 0;
            PatId = 0;
        }

        #region            Command

        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() =>
                {

                    if (CheckAndAdd())
                    {
                        if (SendAdd())
                        {
                            MainWindowViewModel.dbObstegenyaModel= null;
                            Check = "Додали нове обстеження";
                        }
                    }

                }, _canExecute)); ;
            }
        }

        

        #endregion

        #region Property
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
            get
            {
                if (chooseDoctor == null)
                {
                    doctor = new DbDoctorModel().GetData();
                    chooseDoctor = doctor.Select(s => s.FirstName.TrimEnd()
                                                      + " " + s.LastName.TrimEnd() + " " + s.Posada.TrimEnd())
                        .ToList<string>();
                }
                return chooseDoctor;
            }
        }

        public List<string> ChoosePatient
        {
            get
            {
                if (choosePatient == null)
                {
                    patient = new DbPatientModel().GetData();
                    choosePatient = patient.Select(s => s.FirstName.TrimEnd()
                                                      + " " + s.LastName.TrimEnd() + " " + s.BloodType.TrimEnd())
                        .ToList<string>();
                }
                return choosePatient;
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

                }

            }
        }
        public string TimeTo
        {
            get { return timeTo.ToString(); }
            set
            {
                try
                {
                    if (timeWith < TimeSpan.Parse(value))
                        timeTo = TimeSpan.Parse(value);
                    OnPropertyChanged("TimeTo");
                }
                catch (Exception)
                {

                }
            }
        }
        public string TimeWith
        {
            get { return timeWith.ToString(); }
            set
            {
                try
                {

                    if (timeTo > TimeSpan.Parse(value))
                        timeWith = TimeSpan.Parse(value);
                    OnPropertyChanged("TimeWith");
                }
                catch (Exception)
                {

                }

            }
        }
        #endregion

        #region Logic
        private bool CheckAndAdd()
        {
            int doctorId = doctor.ElementAt(DocId).Id;
            var time =
                MainWindowViewModel.dbObstegenyaModel.Where(s => s.DoctorId == doctorId && s.Date.Hour == date.Hour && s.Date.Minute == date.Minute)
                    .Select(s => new { with = s.TimeWith, to = s.TimeTo }).ToList();
            if (time.Count < 1)
            {
                return true;
            }
            foreach (var t in time)
            {
                if (t.with < timeWith && t.to > timeWith &&
                    t.with < timeTo && t.to > timeTo)
                    return false;

                if (t.with > timeWith && t.to > timeWith &&
                  t.with < timeTo && t.to > timeTo)
                    return false;

                if (t.with < timeWith && t.to > timeWith &&
                  t.with < timeTo && t.to < timeTo)
                    return false;
            }
            return true;
        }
        private bool SendAdd()
        {
            int doctorId = doctor.ElementAt(DocId).Id;
            int patientId = patient.ElementAt(PatId).Id;
            DbObstegenyaModel dbObstegenyaModel = new DbObstegenyaModel();

            dbObstegenyaModel.Id = MainWindowViewModel.dbObstegenyaModel.Last().Id + 1;
            dbObstegenyaModel.DoctorId = doctor.Single(s => s.Id == doctorId).Id;
            dbObstegenyaModel.PatientId = patient.Single(s => s.Id == patientId).Id;
            dbObstegenyaModel.Doctor = doctor.Single(s => s.Id == doctorId).FirstName;
            dbObstegenyaModel.DoctorName = doctor.Single(s => s.Id == doctorId).LastName;
            dbObstegenyaModel.DoctorProf = doctor.Single(s => s.Id == doctorId).Posada;
            dbObstegenyaModel.Date = date;
            dbObstegenyaModel.Patient = patient.Single(s => s.Id == patientId).FirstName;
            dbObstegenyaModel.PatientName = patient.Single(s => s.Id == patientId).LastName;
            dbObstegenyaModel.TimeWith = timeWith;
            dbObstegenyaModel.TimeTo = timeTo;
            return new DbObstegenyaModel().InsertData(dbObstegenyaModel);
        }
        #endregion
    }
}