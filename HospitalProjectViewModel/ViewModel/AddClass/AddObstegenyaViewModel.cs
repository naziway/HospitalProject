using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Data;
using HospitalProject.Data;
using HospitalProject.View;

namespace HospitalProject.ViewModel
{
    public class AddObstegenyaViewModel : BaseViewModel
    {
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
            DbDoctor.AddDoctor += DbDoctor_addNewDoctor;
            DbDoctor.DeleteDoctor += DbDoctor_deleteDoctor;
            DbDoctor.UpdateDoctor += DbDoctor_updateDoctor;
            DbPatient.AddPatient += DbPatient_addNewPatient;
            DbPatient.DeletePatient += DbPatient_deletePatient;
            DbPatient.UpdatePatient += DbPatient_updatePatient;

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
                            Check = "Додали нове обстеження";
                        }
                    }

                }, _canExecute)); ;
            }
        }
        private ICommand _addDoctor;
        public ICommand AddDoctor
        {
            get
            {
                return _addDoctor ?? (_addDoctor = new CommandHandler(() =>
                {
                    DoctorListView doctorListView = new DoctorListView();
                    doctorListView.Show();
                }, _canExecute)); ;
            }
        }
        private ICommand _addPatient;
        public ICommand AddPatient
        {
            get
            {
                return _addPatient ?? (_addPatient = new CommandHandler(() =>
                {
                    PatientListView patientListView = new PatientListView();
                    patientListView.Show();

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
                    chooseDoctor = DbDoctor.DoctorList.Select(s => s.FirstName.TrimEnd()
                                   + " " + s.LastName.TrimEnd() + " " + s.Posada.TrimEnd()).ToList<string>();
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
                    choosePatient = DbPatient.PatientList.Select(s => s.FirstName.TrimEnd()
                                    + " " + s.LastName.TrimEnd() + " " + s.BloodType.TrimEnd()).ToList<string>();
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
                catch
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
        private void DbPatient_updatePatient(object sender, DbPatientModel e)
        {
            choosePatient = DbPatient.PatientList.Select(s => s.FirstName.TrimEnd()
                             + " " + s.LastName.TrimEnd() + " " + s.BloodType.TrimEnd()).ToList<string>();
            OnPropertyChanged("ChoosePatient");
        }

        private void DbPatient_deletePatient(object sender, DbPatientModel e)
        {

            choosePatient = DbPatient.PatientList.Select(s => s.FirstName.TrimEnd()
                            + " " + s.LastName.TrimEnd() + " " + s.BloodType.TrimEnd()).ToList<string>();
            OnPropertyChanged("ChoosePatient");
        }

        private void DbPatient_addNewPatient(object sender, DbPatientModel e)
        {

            choosePatient.Add(e.FirstName.TrimEnd() + " " + e.LastName.TrimEnd() + " " + e.BloodType.TrimEnd());
            OnPropertyChanged("ChoosePatient");

        }

        private void DbDoctor_updateDoctor(object sender, DbDoctorModel e)
        {
            chooseDoctor = DbDoctor.DoctorList.Select(s => s.FirstName.TrimEnd()
                                   + " " + s.LastName.TrimEnd() + " " + s.Posada.TrimEnd()).ToList<string>();
            OnPropertyChanged("ChooseDoctor");
        }

        private void DbDoctor_deleteDoctor(object sender, DbDoctorModel e)
        {
            chooseDoctor = DbDoctor.DoctorList.Select(s => s.FirstName.TrimEnd()
                                   + " " + s.LastName.TrimEnd() + " " + s.Posada.TrimEnd()).ToList<string>();
            OnPropertyChanged("ChooseDoctor");
        }

        private void DbDoctor_addNewDoctor(object sender, DbDoctorModel e)
        {
            chooseDoctor.Add(e.FirstName.TrimEnd() + " " + e.LastName.TrimEnd() + " " + e.Posada.TrimEnd());
            OnPropertyChanged("ChooseDoctor");
        }
        private bool CheckAndAdd()
        {
            int doctorId = DbDoctor.DoctorList.ElementAt(DocId).Id;
            var time =
                DbObstegenya.ObstegenyaList.Where(s => s.DoctorId == doctorId && s.Date.Hour == date.Hour && s.Date.Minute == date.Minute)
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
                if (t.with == timeWith || t.to == timeWith ||
                 t.with == timeTo || t.to == timeTo)
                    return false;
            }
            return true;
        }
        private bool SendAdd()
        {
            int doctorId = DbDoctor.DoctorList.ElementAt(DocId).Id;
            int patientId = DbPatient.PatientList.ElementAt(PatId).Id;
            var dbObstegenyaModel = new DbObstegenyaModel();
            dbObstegenyaModel.Id = DbObstegenya.ObstegenyaList.Last().Id + 1;
            dbObstegenyaModel.DoctorId = DbDoctor.DoctorList.Single(s => s.Id == doctorId).Id;
            dbObstegenyaModel.PatientId = DbPatient.PatientList.Single(s => s.Id == patientId).Id;
            dbObstegenyaModel.Doctor = DbDoctor.DoctorList.Single(s => s.Id == doctorId).FirstName;
            dbObstegenyaModel.DoctorName = DbDoctor.DoctorList.Single(s => s.Id == doctorId).LastName;
            dbObstegenyaModel.DoctorProf = DbDoctor.DoctorList.Single(s => s.Id == doctorId).Posada;
            dbObstegenyaModel.Date = date;
            dbObstegenyaModel.Patient = DbPatient.PatientList.Single(s => s.Id == patientId).FirstName;
            dbObstegenyaModel.PatientName = DbPatient.PatientList.Single(s => s.Id == patientId).LastName;
            dbObstegenyaModel.TimeWith = timeWith;
            dbObstegenyaModel.TimeTo = timeTo;



            return new DbObstegenya().InsertData(dbObstegenyaModel);
        }
        #endregion
    }
}