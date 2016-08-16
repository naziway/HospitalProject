using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Data;
using HospitalProject.ViewModel;

namespace HospitalProject.View
{
    /// <summary>
    /// Interaction logic for RefreshDoctorView.xaml
    /// </summary>
    public partial class RefreshDoctorView : Window
    {
        public RefreshDoctorView(DbDoctorModel data)
        {
            InitializeComponent();
            DataContext = new RefreshDoctorViewModel(data);
        }
    }
}
