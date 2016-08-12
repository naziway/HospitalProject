using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace DataBase
{
    public class DbObstegenyaModel:IDb<DbObstegenyaModel>
    {
        public int Id { get; set; }
        public string Doctor { get; set; }
        public string Patient { get; set; }
        public System.DateTime Date { get; set; }
        public System.TimeSpan TimeWith { get; set; }
        public System.TimeSpan TimeTo { get; set; }


        public List<DbObstegenyaModel> GetData()
        {

            List<DbObstegenyaModel> dbObstegenyaModel = null;

            using (HospitalEntities dbData = new HospitalEntities())
            {
                var obstegenyas = dbData.Obstegenyas.ToList<Obstegenya>();
                var patient = dbData.Patients.ToList<Patient>();
                var doctor = dbData.Doctors.ToList<Doctor>();

                var join = doctor.Join(obstegenyas, x => x.Id, y => y.Id, (b, a) => new
                {
                    Id =a.Id,
                    Doctor=b.FirstName+b.LastName+b.Posada,
                    Patient =a.PatientId,
                    Date = a.Date,
                    TimeWith=a.TimeWith,
                    TimeTo = a.TimeTo
                }).ToList();
                
                dbObstegenyaModel = join.Join(patient, x => x.Id, y => y.Id, (b, a) => new DbObstegenyaModel()
                {
                    Id = a.Id,
                    Doctor = b.Doctor,
                    Patient = a.FirstName + a.LastName + a.DateBirth,
                    Date = b.Date,
                    TimeWith = b.TimeWith,
                    TimeTo = b.TimeTo
                }).ToList<DbObstegenyaModel>();
            }

            return dbObstegenyaModel;



        }

        public void InsertData(DbObstegenyaModel data)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateData(DbObstegenyaModel data)
        {
            throw new System.NotImplementedException();
        }
    }
}