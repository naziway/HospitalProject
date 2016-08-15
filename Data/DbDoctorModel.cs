﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;

namespace Data
{
    public class DbDoctorModel : IDb<DbDoctorModel>
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Posada { get; set; }



        public List<DbDoctorModel> GetData()
        {
            List<DbDoctorModel> dbDoctors = null;

            using (HospitalEntities dbData = new HospitalEntities())
            {
                dbDoctors = dbData.Doctors.Select(
                      s => new DbDoctorModel() { Id = s.Id, FirstName = s.FirstName, LastName = s.LastName, Posada = s.Posada })
                      .ToList<DbDoctorModel>();
            }
            return dbDoctors;
        }

        public bool InsertData(DbDoctorModel data)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateData(DbDoctorModel data)
        {
            throw new System.NotImplementedException();
        }
    }
}