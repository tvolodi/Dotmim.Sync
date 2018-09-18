﻿using System;
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

namespace XamAndroidSyncSample.DataServices
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDtoOutput>> GetAllAsync();
        int AddEmployees();
    }

    public class EmployeeDtoOutput
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public Byte[] ProfilePicture { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string Comments { get; set; }
    }
}