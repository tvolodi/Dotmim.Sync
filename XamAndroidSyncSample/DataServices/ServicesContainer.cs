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
            builder.RegisterType<IEmployeeRepository>().As<IEmployeeRepository>();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            Container = builder.Build();

        }

    }
}