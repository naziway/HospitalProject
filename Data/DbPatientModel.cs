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

        public void InsertData(DbPatientModel data)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateData(DbPatientModel data)
        {
            throw new System.NotImplementedException();
        }
    }
}