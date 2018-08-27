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

        public async Task<List<EmployeeDtoOutput>> GetAllAsync()
        {
            return await Task.Run(async () =>
            {
                var mapConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Employee, EmployeeDtoOutput>();
                });

                IMapper mapper = mapConfig.CreateMapper();

                List<Employee> employees = await employeeRepository.GetEmployeesAsync();

                List<EmployeeDtoOutput> employeesDtoOutput = employees.Select(employee => mapper.Map<Employee, EmployeeDtoOutput>(employee)).ToList();

                return employeesDtoOutput;

            });

        }
    }
}