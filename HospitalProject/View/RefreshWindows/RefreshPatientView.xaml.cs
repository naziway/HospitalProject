using System.Windows;
using Data;
using HospitalProject.ViewModel;

namespace HospitalProject.View.RefreshWindows
{
    /// <summary>
    /// Interaction logic for RefreshPatientView.xaml
    /// </summary>
    public partial class RefreshPatientView : Window
    {
        public RefreshPatientView(DbPatientModel data)
        {
            InitializeComponent();
            DataContext = new RefreshPatientViewModel(data);
        }
    }
}
