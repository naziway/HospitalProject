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
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool UpdateData(DbPatientModel data)
        {
            throw new System.NotImplementedException();
        }
    }
}