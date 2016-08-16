using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            Doctor obs = new Doctor();
            obs.Id = GetData().Last().Id + 1;
            obs.FirstName = data.FirstName;
            obs.LastName = data.LastName;
            obs.Posada = data.Posada;

            using (HospitalEntities dbData = new HospitalEntities())
            {
                try
                {
                    dbData.Doctors.Add(obs);
                    dbData.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool UpdateData(DbDoctorModel data)
        {
            using (HospitalEntities dbData = new HospitalEntities())
            {
                var mod = dbData.Doctors.FirstOrDefault(c => c.Id == data.Id);
                if (mod == null) return false;
                try
                {

                    mod.FirstName = data.FirstName;
                    mod.LastName = data.LastName;
                    mod.Posada = data.Posada;
                    dbData.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool DeleteData(DbDoctorModel data)
        {
            var doctor = new Doctor() { FirstName = data.FirstName, LastName = LastName, Id = data.Id, Posada = data.Posada };

            using (HospitalEntities dbData = new HospitalEntities())
            {
                if (!dbData.Obstegenyas.Any(x => x.DoctorId == doctor.Id))
                    try
                    {
                        dbData.Entry(doctor).State = EntityState.Deleted;
                        dbData.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
            }
            return false;
        }
    }
}