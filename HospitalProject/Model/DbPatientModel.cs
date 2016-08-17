using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data
{
    public class DbPatientModel : IDb<DbPatientModel>
    { public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BloodType { get; set; }
        
        public System.DateTime DateBirth { get; set; }

        public List<DbPatientModel> GetData()
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
                      s => new DbPatientModel() { Id = s.Id, FirstName = s.FirstName, LastName = s.LastName, BloodType = s.BloodType,DateBirth = s.DateBirth})
                      .ToList<DbPatientModel>();
            }

            return dbPatient;
        }

        public bool InsertData(DbPatientModel data)
        {
            Patient obs = new Patient();
            obs.Id = GetData().Last().Id + 1;
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
                    return true;
                }
                catch (Exception e)
                {
                    Loger.Logining.logger.Trace($"Додати дані нового пацієнта не вдалося Exception:{e.Message}");
                    return false;
                }
            }
        }

        public bool UpdateData(DbPatientModel data)
        {
            using (HospitalEntities dbData = new HospitalEntities())
            {
                var mod = dbData.Patients.FirstOrDefault(c => c.Id == data.Id);
                if (mod == null) return false;
                try
                {
                    mod.FirstName = data.FirstName;
                    mod.LastName = data.LastName;
                    mod.BloodType = data.BloodType;
                    mod.DateBirth = data.DateBirth;
                    dbData.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Loger.Logining.logger.Trace($"Обновити дані  пацієнта не вдалося Exception:{e.Message}");
                    return false;
                }
            }
        }
        public bool DeleteData(DbPatientModel data)
        {
            var patient = new Patient() { FirstName = data.FirstName, LastName = LastName, Id = data.Id, BloodType = data.BloodType,DateBirth = data.DateBirth};

            using (HospitalEntities dbData = new HospitalEntities())
            {
                if (!dbData.Obstegenyas.Any(x => x.PatientId == patient.Id))
                    try
                    {
                        dbData.Entry(patient).State = EntityState.Deleted;
                        dbData.SaveChanges();
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
    }
}