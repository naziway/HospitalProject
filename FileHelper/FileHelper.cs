using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml.Serialization;


namespace FileHelper
{
    public static class FileHelper
    {
        private readonly static FileInfo file = new FileInfo("program.xml");

        private static Config config = new Config();

        public static string ConnectionString => config.connectionString;

        static FileHelper()
        {
            if (file.Exists)
                readConfig();
            else
                creteConfig();
        }
        private static void creteConfig()
        {
            config.connectionString = ConfigurationManager.ConnectionStrings["HospitalEntities"].ConnectionString;

            using (Stream writer = new FileStream("program.xml", FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                serializer.Serialize(writer, config);
            }
        }
        private static void readConfig()
        {
            try
            {
                using (Stream stream = new FileStream("program.xml", FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Config));
                    config = (Config)serializer.Deserialize(stream);
                }
            }
            catch (Exception)
            {
                creteConfig();
            }
        }
    }

    [Serializable]
    public class Config
    {
        public string connectionString;

    }
}