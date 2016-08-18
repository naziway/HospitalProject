using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data;

namespace HospitalProject.Data
{
    public  class DbPatient : IDb<DbPatientModel>
    {
        private static List<DbPatientModel> patientList;

        public static List<DbPatientModel> PatientList
        {
            get
            {
                if (patientList == null)
                    patientList = new DbPatient().GetData();
                return patientList;
            }
        }
        public  List<DbPatientModel> GetData()
        {
            List<DbPatientModel> dbPatient = null;

            using (HospitalEntities dbData = new HospitalEntities())
            {
                if (!dbData.Database.Exists())
                {
                    Loger.Logining.logger.Trace($"Невстановлено з'єднання з базою!!!");
                    return null;
                }
                dbPatient = dbData.Patients.Select(
                      s => new DbPatientModel() { Id = s.Id, FirstName = s.FirstName, LastName = s.LastName, BloodType = s.BloodType, DateBirth = s.DateBirth })
                      .ToList<DbPatientModel>();
            }

            return dbPatient;
        }

        public  bool InsertData(DbPatientModel data)
        {
            Patient obs = new Patient();
            obs.Id = GetData().Last().Id + 1;
            data.Id = obs.Id;
            obs.FirstName = data.FirstName;
            obs.LastName = data.LastName;
            obs.BloodType = data.BloodType;
            obs.DateBirth = data.DateBirth;
            using (HospitalEntities dbData = new HospitalEntities())
            {
                try
                {
                    dbData.Patients.Add(obs);
                    dbData.SaveChanges();
                    DbPatient.patientList.Add(data);
                    AddPatient?.Invoke(null,data);
                    return true;
                }
                catch (Exception e)
                {
                    Loger.Logining.logger.Trace($"Додати дані нового пацієнта не вдалося Exception:{e.Message}");
                    return false;
                }
            }
        }

        public  bool UpdateData(DbPatientModel data)
        {
            using (HospitalEntities dbData = new HospitalEntities())
            {
                var mod = dbData.Patients.FirstOrDefault(c => c.Id == data.Id);
                var mod2 = patientList.FirstOrDefault(c => c.Id == data.Id);
                if (mod == null) return false;
                try
                {
                    mod.FirstName = data.FirstName;
                    mod.LastName = data.LastName;
                    mod.BloodType = data.BloodType;
                    mod.DateBirth = data.DateBirth;
                    dbData.SaveChanges();
                    mod2.FirstName = data.FirstName;
                    mod2.LastName = data.LastName;
                    mod2.BloodType = data.BloodType;
                    mod2.DateBirth = data.DateBirth;
                    UpdatePatient?.Invoke(null, data);
                    return true;
                }
                catch (Exception e)
                {
                    Loger.Logining.logger.Trace($"Обновити дані  пацієнта не вдалося Exception:{e.Message}");
                    return false;
                }
            }
        }
        public  bool DeleteData(DbPatientModel data)
        {
            var patient = new Patient() { FirstName = data.FirstName, LastName = data.LastName, Id = data.Id, BloodType = data.BloodType, DateBirth = data.DateBirth };

            using (HospitalEntities dbData = new HospitalEntities())
            {
                if (!dbData.Obstegenyas.Any(x => x.PatientId == patient.Id))
                    try
                    {
                        dbData.Entry(patient).State = EntityState.Deleted;
                        dbData.SaveChanges();
                        DeletePatient?.Invoke(null,data);
                        return true;
                    }
                    catch (Exception e)
                    {
                        Loger.Logining.logger.Trace($"Невдале видалення Пацієнта Exception:{e.Message}");
                        return false;
                    }
            }
            return false;
        }

        public static event EventHandler<DbPatientModel> DeletePatient;
        public static event EventHandler<DbPatientModel> UpdatePatient;
        public static event EventHandler<DbPatientModel> AddPatient;

        
    }
}