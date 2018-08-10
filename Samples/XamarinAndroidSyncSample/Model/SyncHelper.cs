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
using Dotmim.Sync;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.Web.Client;

namespace XamarinAndroidSyncSample.Model
{
    public class SyncHelper
    {

        private SqliteSyncProvider sqliteSyncProvider;
        private WebProxyClientProvider webProxyProvider;

        string[] tables = new string[] { "Employees" };

        public SyncHelper()
        {
            
            Init();
        }

        private void Init()
        {
            // Servers providers

            string connectionString = "127.0.0.1";

            webProxyProvider = new WebProxyClientProvider(
                 new Uri(connectionString));

            sqliteSyncProvider = new SqliteSyncProvider(SqLiteBaseRepository.DbFile);
        }

        public SyncAgent GetSyncAgent(bool useHttp = true)
        {
            if (useHttp)
            {
               return new SyncAgent(sqliteSyncProvider, webProxyProvider);
            }
            else
            {
                //switch (this.contosoType)
                //{
                //    case ConnectionType.Client_SqlServer:
                //        return new SyncAgent(sqlSyncProvider, masterSqlSyncProvider, tables);
                //    case ConnectionType.Client_Sqlite:
                //        return new SyncAgent(sqliteSyncProvider, masterSqlSyncProvider, tables);
                //    case ConnectionType.Client_MySql:
                //        return new SyncAgent(mySqlSyncProvider, masterSqlSyncProvider, tables);
                //}
            }

            return null;
        }

    }
}