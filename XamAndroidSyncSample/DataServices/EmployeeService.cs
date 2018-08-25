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
using AutoMapper;
using XamAndroidSyncSample.Model;

namespace XamAndroidSyncSample.DataServices
{
    public class EmployeeService : IEmployeeService
    {

        private IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public int AddEmployees()
        {
            int result = 0;
            for(int i = 0; i <= 4; i++)
            {
                Employee employee = new Employee
                {
                    FirstName = $"FirstName {i}",
                    HireDate = DateTime.Now,
                    LastName = $"LastName {i}",
                    PhoneNumber = $"{i}{i}{i}-{i}{i}-{i}{i}",
                    Comments = $"Test add employee cycle {i}"
                };
                employeeRepository.SaveEmployee(employee);
                result++;
            }

            return result;
        }

        public List<EmployeeDtoOutput> GetAll()
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDtoOutput>();
            });

            IMapper mapper = mapConfig.CreateMapper();

            List<EmployeeDtoOutput> employeesDtoOutput = employeeRepository.GetEmployees().Select(employee => mapper.Map<Employee, EmployeeDtoOutput>(employee)).ToList();

            return employeesDtoOutput;

        }
    }
}