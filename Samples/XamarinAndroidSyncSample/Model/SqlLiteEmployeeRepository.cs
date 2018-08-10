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
using Dapper.Contrib;
using System.Data;

namespace XamarinAndroidSyncSample.Model
{
    public class SqlLiteEmployeeRepository : IEmployeeRepository
    {

        private SqliteConnection conn;

        public SqlLiteEmployeeRepository(SqliteConnection conn)
        {
            this.conn = conn;

            try
            {
                conn.Open();
                string sqlQueryText = @"CREATE TABLE IF NOT EXISTS Employee
                                        (
                                          EmployeeId TEXT PRIMARY KEY NOT NULL,
                                          FirstName TEXT, 
                                          LastName  TEXT, 
                                          PhoneNumber TEXT, 
                                          HireDate TEXT, 
                                          Comments TEXT
                                        );
                                        ";
            } catch (Exception e)
            {

            } finally
            {
                conn.Close();
            }
        }

        int IEmployeeRepository.DeleteEmployee(Guid employeeId)
        {
            int result = 0;
            string sqlQueryText = @"DELETE FROM Employee WHERE EmployeeId = @employeeId";
            try
            {
                conn.Open();
                conn.Execute(sqlQueryText,
                             new { employeeId = employeeId }
                             );
            } catch(Exception e)
            {
                result = -1;
            } finally
            {
                conn.Close();
            }

            return result;
        }

        Employee IEmployeeRepository.GetEmployee(Guid employeeId)
        {
            Employee employee = null;

            try
            {
                conn.Open();

                string sqlQueryText = @"SELECT FirstName, LastName, PhoneNumber, HireDate, Comments
                                        FROM Employee
                                        WHERE EmployeeId = @employeeId";

                employee = conn.QueryFirst<Employee>(sqlQueryText,
                                                              new { employeeId = employeeId });
            } catch(Exception e)
            {
                
            } finally
            {
                conn.Close();
            }

            return employee;
        }

        IEnumerable<Employee> IEmployeeRepository.GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                conn.Open();
                string sqlQueryText = @"SELECT FirstName, LastName, PhoneNumber, HireDate, Comments
                                        FROM Employee";
                employees = conn.Query<Employee>(sqlQueryText).ToList();
            } catch(Exception e)
            {

            } finally
            {
                conn.Close();
            }
            return employees;
        }

        void IEmployeeRepository.SaveEmployee(Employee employee)
        {
            try
            {
                conn.Open();
                string sqlQueryText = @"INSERT INTO Employee 
                                            ( FirstName, LastName, PhoneNumber, HireDate, Comments ) 
                                            VALUES 
                                            ( @firstName, @lastName, @phoneNumber, @hireDate, @comments );
                                            select last_insert_rowid()";

                employee.EmployeeId = conn.Query<Guid>(sqlQueryText, 
                                                        new {firstName = employee.FirstName,
                                                             lastName = employee.LastName,
                                                             phoneNumber = employee.PhoneNumber,
                                                             hireDate = employee.HireDate,
                                                             comments = employee.Comments}
                                                      )
                                           .First();
            } finally
            {
                conn.Close();
            }

        }
    }
}