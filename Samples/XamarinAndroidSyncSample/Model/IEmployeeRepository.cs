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

namespace XamarinAndroidSyncSample.Model
{
    public interface IEmployeeRepository
    {
        void SaveEmployee(Employee employee);
        void DeleteEmployee(Guid employeeId);
        Employee GetEmployee(Guid employeeId);
        IEnumerable<Employee> GetEmployees();
    }
}