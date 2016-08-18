using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Data;
using HospitalProject.Data;
using HospitalProject.View;
using HospitalProject.View.RefreshWindows;

namespace HospitalProject.ViewModel
{
    public class PatientListViewModel : BaseViewModel
    {
        private List<DbPatientModel> patientList = null;
        private int? selectedIndex = null;

        #region Property

        public PatientListViewModel()
        {
            DbPatient.AddPatient += DbPatient_RefreshPatient;
            DbPatient.UpdatePatient += DbPatient_RefreshPatient;
            DbPatient.DeletePatient += DbPatient_RefreshPatient;
        }

        private void DbPatient_RefreshPatient(object sender, DbPatientModel e)
        {
           patientList = DbPatient.PatientList;
           OnPropertyChanged("PatientList");
        }

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
                {
                    patientList = new DbPatientModel().GetData();
                    Loger.Logining.logger.Trace("Завантажились данні Пацієнтів");
                }

                return patientList;
            }
        }
        #endregion

        #region Command

        private ICommand addPatient;
        public ICommand AddPatient => addPatient ?? (addPatient = new CommandHandler(() =>
        {
            AddPatientView addPatientView = new AddPatientView();
            addPatientView.Show();

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
                        MessageBox.Show("Не вибрано пацієнта!!!");
                        Loger.Logining.logger.Info("Не вибрано пацієнта!!!");
                        return;
                    }
                    DbPatientModel deletePat = PatientList.ElementAt(SelectedIndex ?? +1);/// index?

                    if (new DbPatient().DeleteData(deletePat))
                    {
                        MessageBox.Show("Видалено пацієнта!!!");
                        Loger.Logining.logger.Info("Видалено пацієнта!!!");
                        OnPropertyChanged("PatientList");
                    }
                    else
                    {
                        MessageBox.Show("Помилка");
                        Loger.Logining.logger.Error("Помилка видалення");
                    }


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
                        MessageBox.Show("Не вибрано пацієнта!!!");
                        Loger.Logining.logger.Info("Не вибрано пацієнта!!!");
                        return;
                    }
                    RefreshPatientView addPatientView = new RefreshPatientView(PatientList.ElementAt(SelectedIndex ?? +1));
                    addPatientView.Show();

                }, _canExecute)); ;
            }
        }




        #endregion


    }
}