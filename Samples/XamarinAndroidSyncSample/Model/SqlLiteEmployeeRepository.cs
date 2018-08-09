using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Data.Sqlite;
using Dapper;

namespace XamarinAndroidSyncSample.Model
{
    public class SqlLiteEmployeeRepository : IEmployeeRepository
    {

        private SqliteConnection conn;

        public SqlLiteEmployeeRepository(SqliteConnection conn)
        {
            this.conn = conn;
        }

        void IEmployeeRepository.DeleteEmployee(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        Employee IEmployeeRepository.GetEmployee(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Employee> IEmployeeRepository.GetEmployees()
        {
            throw new NotImplementedException();
        }

        void IEmployeeRepository.SaveEmployee(Employee employee)
        {
                employee.EmployeeId = conn.Query<Guid>(
                                                        @"INSERT INTO Employee 
                                                            ( FirstName, LastName, PhoneNumber, HireDate, Comments ) 
                                                            VALUES 
                                                            ( @FirstName, @LastName, @PhoneNumber, @HireDate, @Comments );
                                                            select last_insert_rowid()", employee
                                                      )
                                           .First();
        }
    }
}