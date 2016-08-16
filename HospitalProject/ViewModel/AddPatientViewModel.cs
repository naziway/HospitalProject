using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Data;

namespace HospitalProject.ViewModel
{
    class AddPatientViewModel : BaseViewModel
    {

        private string firstName;
        private string lastName;
        private List<string> bloodType;
        private DateTime date = DateTime.Today;
        private int selectedBlood = 0;

        #region Property
        public int SelectedBlood
        {
            get { return selectedBlood; }
            set
            {
                OnPropertyChanged("SelectedBlood");
                selectedBlood = value;
            }
        }

        public List<string> BloodType
        {
            get
            {
                if (bloodType == null)
                    bloodType = GetBloodType();
                return bloodType;
            }
        }

        public string DataBirth
        {
            get { return date.ToShortDateString(); }
            set
            {
                try
                {
                    date = DateTime.Parse(value);
                    OnPropertyChanged("DataBirth");
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

        private ICommand addPatient;
        public ICommand AddPatient
        {
            get
            {
                return addPatient ?? (addPatient = new CommandHandler(check, _canExecute)); ;
            }
        }
        #endregion

        #region Logic
        public static List<string> GetBloodType()
        {
            List<string> list = new List<string>();
            int i;
            for (i = 1; i <= 4; i++)
            {
                list.Add(i.ToString() + "+");
                list.Add(i.ToString() + "-");
            }
            return list;
        }
        private void check()
        {
            if (FirstName == null || LastName == null)
                MessageBox.Show("Незаповнені поля");
            else
            {
                if (new DbPatientModel().InsertData(new DbPatientModel()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    DateBirth = date,
                    BloodType = GetBloodType().ElementAt(selectedBlood)

                }))
                {
                    MessageBox.Show("Данні успішно додані");
                }
                else
                {
                    MessageBox.Show("Данні не додані");
                }

            }
        }
        #endregion
    }
}
