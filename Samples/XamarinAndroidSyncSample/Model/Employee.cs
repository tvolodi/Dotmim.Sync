﻿using System;
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
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        //public Byte[] ProfilePicture { get; set; }
        public String PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public String Comments { get; set; }
    }
}