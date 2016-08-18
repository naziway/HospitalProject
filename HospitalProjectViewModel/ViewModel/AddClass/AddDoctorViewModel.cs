using System.Windows;
using System.Windows.Input;
using Data;
using HospitalProject.Data;

namespace HospitalProject.ViewModel
{
    public class AddDoctorViewModel : BaseViewModel
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

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ?? (addCommand = new CommandHandler(CheckFilld, _canExecute));

        private void CheckFilld()
        {
            if (FirstName == null || LastName == null || Prof == null)
                MessageBox.Show("Незаповнені поля");
            else
            {
                if (new DbDoctor().InsertData(new DbDoctorModel()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Posada = Prof
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