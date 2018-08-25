using System.IO;
using Microsoft.Data.Sqlite;

namespace XamAndroidSyncSample.Model
{
    public class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get
            {
                return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.db3");
            }
        }

        public static SqliteConnection SimpleDbConnection()
        {            
            return new SqliteConnection("Data Source=" + DbFile);
        }
    }
}