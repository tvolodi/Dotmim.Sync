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
using Dapper;


namespace XamAndroidSyncSample.Model
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private SqliteConnection conn = null;

        public EmployeeRepository()
        {
            conn = SqLiteBaseRepository.SimpleDbConnection();

            try
            {
                conn.Open();

                string sqlDeleteTable = @"DROP TABLE Employee";
                conn.Execute(sqlDeleteTable);

                string sqlQueryText = @"CREATE TABLE IF NOT EXISTS Employee
                                    (                  
                                            EmployeeId INTEGER PRIMARY KEY,
                                            FirstName TEXT, 
                                            LastName  TEXT, 
                                            PhoneNumber TEXT, 
                                            HireDate TEXT, 
                                            Comments TEXT
                                    );
                                    ";
                conn.Execute(sqlQueryText);
            } catch (Exception e)
            {
                string eMsg = e.ToString();
            } finally
            {
                conn.Close();
            }
            
        }

        public int DeleteEmployee(int employeeId)
        {
            int result = 0;
            string sqlQueryText = @"DELETE FROM Employee WHERE EmployeeId = @EmployeeId";
            try
            {
                conn.Open();
                conn.Execute(sqlQueryText,
                                new { EmployeeId = employeeId }
                                );
            }
            catch (Exception e)
            {
                result = -1;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public Employee GetEmployee(int employeeId)
        {
            Employee employee = null;

            try
            {
                conn.Open();

                string sqlQueryText = @"SELECT FirstName, LastName, PhoneNumber, HireDate, Comments
                                    FROM Employee
                                    WHERE rowid = @EmployeeId";

                employee = conn.QueryFirst<Employee>(sqlQueryText,
                                                                new { EmployeeId = employeeId });
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return employee;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                conn.Open();
                string sqlQueryText = @"SELECT EmployeeId, FirstName, LastName, PhoneNumber, HireDate, Comments
                                    FROM Employee";
                employees = conn.Query<Employee>(sqlQueryText).ToList();
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return employees;
        }

        public void SaveEmployee(Employee employee)
        {
            try
            {
                conn.Open();
                string sqlQueryText = @"INSERT INTO Employee 
                                        ( FirstName, LastName, PhoneNumber, HireDate, Comments ) 
                                        VALUES 
                                        ( @FirstName, @LastName, @PhoneNumber, @HireDate, @Comments );
                                        select last_insert_rowid()";

                employee.EmployeeId = conn.Query<int>(sqlQueryText, employee).First();
            }
            finally
            {
                conn.Close();
            }

        }
    }
}