using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;


namespace Data
{
    public class DbObstegenyaModel
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string Doctor { get; set; }
        public string DoctorName { get; set; }
        public string DoctorProf { get; set; }
        public string PatientName { get; set; }
        public string Patient { get; set; }
        public System.DateTime Date { get; set; }
        public System.TimeSpan TimeWith { get; set; }
        public System.TimeSpan TimeTo { get; set; }

    }
}