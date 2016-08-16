﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Data;
using HospitalProject.View;

namespace HospitalProject.ViewModel
{
    public class DoctorListViewModel : BaseViewModel
    {
        private List<DbDoctorModel> doctorList = null;
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

        public List<DbDoctorModel> DoctorList
        {
            get
            {
                if (doctorList == null)
                    doctorList = new DbDoctorModel().GetData();

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
                    addPatientView.ShowDialog();

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
                        return;
                    }
                    DbDoctorModel deleteDoc = DoctorList.ElementAt(SelectedIndex ?? + 1);

                    if (new DbDoctorModel().DeleteData(deleteDoc))
                        MessageBox.Show("Видалено лікаря!!!");
                    else
                        MessageBox.Show("Помилка");

                }, _canExecute)); ;
            }
        }





        #endregion

    }
}