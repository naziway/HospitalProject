using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data;

namespace HospitalProject.Data
{
    public  class DbDoctor:IDb<DbDoctorModel>
    {
        private static List<DbDoctorModel> doctorList;

        public static List<DbDoctorModel> DoctorList
        {
            get
            {
                if (doctorList == null)
                    doctorList = new DbDoctor().GetData();
                return doctorList;
            }
        }
        public  List<DbDoctorModel> GetData()
        {
            List<DbDoctorModel> dbDoctors = null;

            using (HospitalEntities dbData = new HospitalEntities())
            {
                if (!dbData.Database.Exists())
                {
                    Loger.Logining.logger.Trace($"Невстановлено з'єднання з базою!!!");
                    return null;
                }
                try
                {
                    dbDoctors = dbData.Doctors.Select(
                      s => new DbDoctorModel() { Id = s.Id, FirstName = s.FirstName, LastName = s.LastName, Posada = s.Posada })
                      .ToList<DbDoctorModel>();
                }
                catch (Exception e)
                {
                    Loger.Logining.logger.Trace($"Завантажити данні Лікарів не вдалося Exception:{e.Message}");
                    return dbDoctors;
                }

            }
            return dbDoctors;
        }

        public  bool InsertData(DbDoctorModel data)
        {
            Doctor obs = new Doctor();
            obs.Id = GetData().Last().Id + 1;
            data.Id = obs.Id;
            obs.FirstName = data.FirstName;
            obs.LastName = data.LastName;
            obs.Posada = data.Posada;

            using (HospitalEntities dbData = new HospitalEntities())
            {
                try
                {
                    dbData.Doctors.Add(obs);
                    dbData.SaveChanges();
                    doctorList.Add(data);
                    AddDoctor?.Invoke(null, data);
                    return true;
                }
                catch (Exception e)
                {
                    Loger.Logining.logger.Trace($"Додати нового лікаря не вдалося Exception:{e.Message}");
                    return false;
                }
            }
        }

        public  bool UpdateData(DbDoctorModel data)
        {
            using (HospitalEntities dbData = new HospitalEntities())
            {
                var mod = dbData.Doctors.FirstOrDefault(c => c.Id == data.Id);
                var mod2 = doctorList.FirstOrDefault(c => c.Id == data.Id);
                if (mod == null) return false;
                try
                {
                    mod.FirstName = data.FirstName;
                    mod.LastName = data.LastName;
                    mod.Posada = data.Posada;
                    dbData.SaveChanges();
                    mod2.FirstName = data.FirstName;
                    mod2.LastName = data.LastName;
                    mod2.Posada = data.Posada;
                    UpdateDoctor?.Invoke(null, data);
                    return true;

                }
                catch (Exception e)
                {
                    Loger.Logining.logger.Trace($"Обновити данні Лікаря не вдалося Exception:{e.Message}");

                    return false;
                }
            }
        }

        public  bool DeleteData(DbDoctorModel data)
        {
            var doctor = new Doctor() { FirstName = data.FirstName, LastName = data.FirstName, Id = data.Id, Posada = data.Posada };

            using (HospitalEntities dbData = new HospitalEntities())
            {
                if (!dbData.Obstegenyas.Any(x => x.DoctorId == doctor.Id))
                    try
                    {
                        dbData.Entry(doctor).State = EntityState.Deleted;
                        dbData.SaveChanges();
                        doctorList.Remove(data);
                        DeleteDoctor?.Invoke(null, data);
                        return true;
                    }
                    catch (Exception e)
                    {
                        Loger.Logining.logger.Trace($"Видалити Лікаря не вдалося Exception:{e.Message}");
                        return false;
                    }
            }
            return false;
        }

        public static event EventHandler<DbDoctorModel> DeleteDoctor;
        public static event EventHandler<DbDoctorModel> UpdateDoctor;
        public static event EventHandler<DbDoctorModel> AddDoctor;

    }
}