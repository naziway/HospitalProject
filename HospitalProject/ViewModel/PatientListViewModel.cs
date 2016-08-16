using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Data;
using HospitalProject.View;

namespace HospitalProject.ViewModel
{
    public class PatientListViewModel : BaseViewModel
    {
        private List<DbPatientModel> patientList = null;
        private int? selectedIndex = null;

        public int? SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }
        public List<DbPatientModel> PatientList
        {
            get
            {
                if (patientList == null)
                    patientList = new DbPatientModel().GetData();
                return patientList;
            }
        }

        #region Command

        private ICommand addPatient;
        public ICommand AddPatient => addPatient ?? (addPatient = new CommandHandler(() =>
        {
            AddPatientView addPatientView = new AddPatientView();
            addPatientView.ShowDialog();

        }, _canExecute));

        private ICommand deletePatient;
        public ICommand DeletePatient
        {
            get
            {
                return deletePatient ?? (deletePatient = new CommandHandler(() =>
                {

                    if (selectedIndex == null)
                    {
                        MessageBox.Show("Невибрано пацієнта!!!");
                        return;
                    }
                    DbPatientModel deletePat = PatientList.ElementAt(SelectedIndex ?? +1);/// index?

                    if (new DbPatientModel().DeleteData(deletePat))
                        MessageBox.Show("Видалено пацієнта!!!");
                    else
                        MessageBox.Show("Помилка");

                }, _canExecute)); ;
            }
        }
        private ICommand refreshPatient;
        public ICommand RefreshPatient
        {
            get
            {
                return refreshPatient ?? (refreshPatient = new CommandHandler(() =>
                {

                    if (selectedIndex == null)
                    {
                        MessageBox.Show("Не вибрано лікаря!!!");
                        return;
                    }
                  //  RefreshDoctorView addPatientView = new RefreshDoctorView(PatientList.ElementAt(SelectedIndex ?? +1));
                   // addPatientView.ShowDialog();

                }, _canExecute)); ;
            }
        }




        #endregion




    }
}