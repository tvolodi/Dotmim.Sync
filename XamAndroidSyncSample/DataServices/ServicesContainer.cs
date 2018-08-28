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
using Autofac;
using Microsoft.Data.Sqlite;
using XamAndroidSyncSample.Model;

namespace XamAndroidSyncSample.DataServices
{
    public class ServicesContainer
    {
        static ServicesContainer()
        {
            InitContainer();
        }

        public static IContainer Container { get; set; }

        public static void InitContainer()
        {
            var builder = new ContainerBuilder();
            
            // Register Employee repository with SqliteConnection as parameter
            builder.Register(c => new EmployeeRepository(SqLiteBaseRepository.SimpleDbConnection())).As<IEmployeeRepository>();

//            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().WithParameter;
            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            // builder.RegisterType<SqliteConnection>().WithParameter(new TypedParameter(typeof(string), SqLiteBaseRepository.SimpleDbConnection.));
            Container = builder.Build();

        }

    }
}