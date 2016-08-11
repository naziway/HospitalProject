using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using HospitalProject.Model;

namespace HospitalProject.ViewModel
{
    public enum Sourting
    {
        all,
        namePat
    }
    public class MainWindowViewModel : BaseViewModel
    {

        private Sourting source;

        private List<Obstegenya> Obsteg;

        private string request;

        public Sourting Source
        {
            get { return source; }
            set
            {
                source = value;
                OnPropertyChanged("Source");
            }
        }

        public string Request
        {
            get { return request; }
            set
            {
                request = value;
                OnPropertyChanged("Request");
            }
        }
        public List<Obstegenya> Data
        {
            get { return Obsteg; }
            set
            {
                Data = value;
                OnPropertyChanged("Data");
            }
        }


        public MainWindowViewModel()
        {
            Task<List<Obstegenya>> Obsteg;

            Obsteg = GetObsteTask();

            Obsteg.Wait();

            this.Obsteg = Obsteg.Result;

        }

        public async Task<List<Obstegenya>> GetObsteTask()
        {
            HospitalEntities data = new HospitalEntities();

            return data.Obstegenyas.ToList<Obstegenya>();
        }


    }
}