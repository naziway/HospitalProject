using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Data;
using HospitalProject.Data;

namespace HospitalProject.ViewModel
{
    public class RefreshPatientViewModel : BaseViewModel
    {
        private int id;
        private int selectIndex;
        private string firstName;
        private string lastName;
        private List<string> bloodType;
        private DateTime date;
        public RefreshPatientViewModel(DbPatientModel data)
        {

            id = data.Id;
            firstName = data.FirstName;
            lastName = data.LastName;
            date = data.DateBirth;
            bloodType = AddPatientViewModel.GetBloodType();
            selectIndex = GetIndex(data.BloodType);
        }

        #region            Property

        public int SelectIndex
        {
            get { return selectIndex; }
            set
            {
                selectIndex = value;
                OnPropertyChanged("SelectIndex");
            }
        }

        public List<string> BloodType
        {
            get { return bloodType; }
        }

        public string DataBirth
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
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        #endregion

        #region            Command

        private ICommand refreshPatient;
        public ICommand RefreshPatient => refreshPatient ?? (refreshPatient = new CommandHandler(CheckFilld, _canExecute));

        #endregion

        #region Logic

        private void CheckFilld()
        {
            if (FirstName == "" || LastName == "")
                MessageBox.Show("Незаповнені поля");
            else
            {
                if (new DbPatient().UpdateData(new DbPatientModel()
                {
                    Id = id,
                    FirstName = FirstName,
                    LastName = LastName,
                    BloodType = BloodType.ElementAt(selectIndex),
                    DateBirth = date
                }))
                {
                    MessageBox.Show("Дані успішно оновлено!");
                }
                else
                {
                    MessageBox.Show("Дані не оновлено!");
                }
            }
        }
        private int GetIndex(string blood)
        {
            int i = 0;
            foreach (var data in AddPatientViewModel.GetBloodType())
            {
                if (data == blood) return i;
                i++;
            }
            return -1;
        }
        #endregion
    }
}