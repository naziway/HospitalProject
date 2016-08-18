using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Data;
using HospitalProject.Data;
using HospitalProject.View;

namespace HospitalProject.ViewModel
{
    public class DoctorListViewModel : BaseViewModel
    {
        private static List<DbDoctorModel> doctorList = null;
        private int? selectedIndex = null;

        public DoctorListViewModel()
        {
            DbDoctor.AddDoctor += DbDoctor_RefreshDoctor;
            DbDoctor.DeleteDoctor += DbDoctor_RefreshDoctor;
            DbDoctor.UpdateDoctor += DbDoctor_RefreshDoctor;
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
        public List<DbDoctorModel> DoctorList
        {
            get
            {
                if (doctorList == null)
                {
                    doctorList = new DbDoctorModel().GetData();
                    if (doctorList == null)
                    {
                        Loger.Logining.logger.Info("Данні Таблиці Доктор не завантажено!!!");
                    }
                    Loger.Logining.logger.Info("Данні Таблиці Доктор завантажено!!!");
                }
                return doctorList;
            }
        }

        #region Command

        private ICommand addDoctor;
        public ICommand AddDoctor
        {
            get
            {
                return addDoctor ?? (addDoctor = new CommandHandler(() =>
                {
                    AddDoctorView addPatientView = new AddDoctorView();
                    addPatientView.Show();

                }, _canExecute)); ;
            }
        }
        private ICommand deleteDoctor;
        public ICommand DeleteDoctor
        {
            get
            {
                return deleteDoctor ?? (deleteDoctor = new CommandHandler(() =>
                {

                    if (selectedIndex == null)
                    {
                        MessageBox.Show("Невибрано лікаря!!!");
                        Loger.Logining.logger.Info("Невибрано лікаря!!!");
                        return;
                    }
                    DbDoctorModel deleteDoc = DoctorList.ElementAt(SelectedIndex ?? +1);

                    if (new DbDoctor().DeleteData(deleteDoc))
                    {
                        MessageBox.Show("Видалено лікаря!!!");
                        Loger.Logining.logger.Info("Видалено лікаря!!!");
                        OnPropertyChanged("DoctorList");
                    }
                    else
                    {
                        MessageBox.Show("Помилка");
                        Loger.Logining.logger.Info("Помилка видалення лікаря");
                    }


                }, _canExecute)); ;
            }
        }
        private ICommand refreshDoctor;
        public ICommand RefreshDoctor
        {
            get
            {
                return refreshDoctor ?? (refreshDoctor = new CommandHandler(() =>
                {

                    if (selectedIndex == null)
                    {
                        MessageBox.Show("Не вибрано лікаря!!!");
                        return;
                    }
                    RefreshDoctorView addPatientView = new RefreshDoctorView(DoctorList.ElementAt(SelectedIndex ?? +1));
                    addPatientView.Show();

                }, _canExecute)); ;
            }
        }
        #endregion
        private void DbDoctor_RefreshDoctor(object sender, DbDoctorModel e)
        {
            doctorList = DbDoctor.DoctorList;
            OnPropertyChanged("DoctorList");
        }
    }
}