using System.Windows;
using HospitalProject.ViewModel;

namespace HospitalProject.View
{
    /// <summary>
    /// Interaction logic for AddPatientView.xaml
    /// </summary>
    public partial class AddPatientView : Window
    {
        public AddPatientView()
        {
            InitializeComponent();
            DataContext = new AddPatientViewModel();
        }
    }
}
