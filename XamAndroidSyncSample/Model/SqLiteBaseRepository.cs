using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Data.Sqlite;

namespace XamAndroidSyncSample.Model
{
    public class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get { return Android.OS.Environment.ExternalStorageDirectory + "\\SimpleDb.sqlite"; }
        }

        public static SqliteConnection SimpleDbConnection()
        {

            return new SqliteConnection("Data Source=" + DbFile);
        }
    }
}