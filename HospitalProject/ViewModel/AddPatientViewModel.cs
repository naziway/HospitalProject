using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalProject.ViewModel
{
    class AddPatientViewModel:BaseViewModel
    {

        private string firstName;
        private string lastName;
        private string prof;

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

        public string Prof
        {
            get { return prof; }
            set
            {
                prof = value;
                OnPropertyChanged("Prof");
            }
        }

        #region            Command

        private ICommand _clickCommand;
        public ICommand AddCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() =>
                {

                    MessageBox.Show("Good");

                }, _canExecute)); ;
            }
        }
        #endregion
    }
}
