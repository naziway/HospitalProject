using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data;

namespace HospitalProject.Data
{
    public class DbObstegenya : IDb<DbObstegenyaModel>
    {
        private static  List<DbObstegenyaModel> obstegenyaList;

        public static List<DbObstegenyaModel> ObstegenyaList
        {
            get
            {
                if (obstegenyaList == null)
                    obstegenyaList = new DbObstegenya().GetData();
                return obstegenyaList;
            }
        }
        public  List<DbObstegenyaModel> GetData()
        {
            List<DbObstegenyaModel> dbObstegenyaModel = null;

            using (HospitalEntities dbData = new HospitalEntities())
            {
                if (!dbData.Database.Exists())
                {
                    Loger.Logining.logger.Trace($"Невстановлено з'єднання з базою!!!");
                    return null;
                }

                var obstegenyas = dbData.Obstegenyas.ToList<Obstegenya>();
                var patient = dbData.Patients.ToList<Patient>();
                var doctor = dbData.Doctors.ToList<Doctor>();
                var join = obstegenyas.Join(doctor, x => x.DoctorId, y => y.Id, (b, a) => new
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

                dbObstegenyaModel = join.Join(patient, x => x.Patientid, y => y.Id, (b, a) => new DbObstegenyaModel()
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



        public  bool InsertData(DbObstegenyaModel data)
        {
            Obstegenya obs = new Obstegenya
            {
                Id = data.Id,
                DoctorId = data.DoctorId,
                PatientId = data.PatientId,
                Date = data.Date,
                TimeWith = data.TimeWith,
                TimeTo = data.TimeTo
            };
            using (HospitalEntities dbData = new HospitalEntities())
            {
                try
                {
                    dbData.Obstegenyas.Add(obs);
                    dbData.SaveChanges();
                    obstegenyaList.Add(data);
                    addNewObstegenya?.Invoke(null,data);
                    return true;
                }
                catch (Exception e)
                {
                    Loger.Logining.logger.Trace($"Додати данні обстежень не вдалося Exception:{e.Message}");
                    return false;
                }
            }
        }


        public  bool UpdateData(DbObstegenyaModel data)
        {
            throw new System.NotImplementedException();
        }

        public  bool DeleteData(DbObstegenyaModel data)
        {
            throw new NotImplementedException();
        }

        public static event EventHandler<DbObstegenyaModel> addNewObstegenya;
        
    }
}