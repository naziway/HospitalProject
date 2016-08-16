using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;

namespace Data
{
    public class DbObstegenyaModel : IDb<DbObstegenyaModel>
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


        public List<DbObstegenyaModel> GetData()
        {

            List<DbObstegenyaModel> dbObstegenyaModel = null;

            using (HospitalEntities dbData = new HospitalEntities())
            {
                var obstegenyas = dbData.Obstegenyas?.ToList<Obstegenya>();
                var patient = dbData.Patients?.ToList<Patient>();
                var doctor = dbData.Doctors?.ToList<Doctor>();

                var join = obstegenyas?.Join(doctor, x => x.DoctorId, y => y.Id, (b, a) => new
                {
                    Id = b.Id,
                    Doctorid = b.DoctorId,
                    Patientid = b.PatientId,
                    Doctor = a.FirstName,
                    DoctorName = a.LastName,
                    DoctorProf = a.Posada,
                    Patient = b.PatientId,
                    Date = b.Date,
                    TimeWith = b.TimeWith,
                    TimeTo = b.TimeTo
                }).ToList();

                dbObstegenyaModel = join?.Join(patient, x => x.Patientid, y => y.Id, (b, a) => new DbObstegenyaModel()
                {
                    Id = b.Id,
                    DoctorId = b.Doctorid,
                    PatientId = b.Patientid,
                    Doctor = b.Doctor,
                    DoctorName = b.DoctorName,
                    DoctorProf = b.DoctorProf,
                    Patient = a.FirstName,
                    PatientName = a.LastName,
                    Date = b.Date,
                    TimeWith = b.TimeWith,
                    TimeTo = b.TimeTo
                }).ToList<DbObstegenyaModel>();
            }
            return dbObstegenyaModel;
        }



        public bool InsertData(DbObstegenyaModel data)
        {
            Obstegenya obs = new Obstegenya();
            obs.Id = data.Id;
            obs.DoctorId = data.DoctorId;
            obs.PatientId = data.PatientId;
            obs.Date = data.Date;
            obs.TimeWith = data.TimeWith;
            obs.TimeTo = data.TimeTo;
            using (HospitalEntities dbData = new HospitalEntities())
            {

                try
                {
                    dbData.Obstegenyas.Add(obs);
                    dbData.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


        public bool UpdateData(DbObstegenyaModel data)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteData(DbObstegenyaModel data)
        {
            throw new NotImplementedException();
        }
    }
}