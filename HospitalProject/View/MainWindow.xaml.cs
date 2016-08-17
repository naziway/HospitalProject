using System.Windows;
using HospitalProject.ViewModel;

namespace HospitalProject.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
            this.Show();
        }
        
    }
}
