using System.Windows;
using System.Windows.Input;
using Data;
using HospitalProject.Data;

namespace HospitalProject.ViewModel
{
    public class RefreshDoctorViewModel:BaseViewModel
    {
        public RefreshDoctorViewModel(DbDoctorModel data)
        {
            id = data.Id;
            FirstName = data.FirstName;
            LastName = data.LastName;
            Posada = data.Posada;

        }

        private int id;
        private string firstName;
        private string lastName;
        private string posada;

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

        public string Posada
        {
            get { return posada; }
            set
            {
                posada = value;
                OnPropertyChanged("Posada");
            }
        }

        #region            Command

        private ICommand refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                return refreshCommand ?? (refreshCommand = new CommandHandler(() =>
                {
                    CheckFilld();
                }, _canExecute)); ;
            }
        }

        private void CheckFilld()
        {
            if (FirstName == "" || LastName == "" || Posada == "")
                MessageBox.Show("Незаповнені поля");
            else
            {
                if (new DbDoctor().UpdateData(new DbDoctorModel()
                {
                    Id= id,
                    FirstName = FirstName,
                    LastName = LastName,
                    Posada = Posada
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

        #endregion
    }
}