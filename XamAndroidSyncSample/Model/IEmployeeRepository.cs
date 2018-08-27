using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamAndroidSyncSample.Model
{
    public interface IEmployeeRepository
    {
        void SaveEmployee(Employee employee);
        int DeleteEmployee(int employeeId);
        Employee GetEmployee(int employeeId);
        Task<List<Employee>> GetEmployeesAsync();
    }
}