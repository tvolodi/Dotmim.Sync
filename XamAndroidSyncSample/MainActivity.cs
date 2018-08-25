using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Collections.Generic;
using XamAndroidSyncSample.DataServices;
using Autofac;
using System.Linq;

namespace XamAndroidSyncSample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity 
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            ListView syncDataListView = FindViewById<ListView>(Resource.Id.syncDataListView);

            List<EmployeeDtoOutput> employees;

            string[] syncDataArray; // new string[] { "test", "test2" };
            using (var scope = ServicesContainer.Container.BeginLifetimeScope())
            {
                var employeeService = scope.Resolve<IEmployeeService>();
                employees = employeeService.GetAll();
            }

            syncDataArray = employees.Select(dto => dto.EmployeeId.ToString()).ToArray();
                ArrayAdapter<string> syncDataAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, syncDataArray);
            // ListAdapter = new ArrayAdapter<string>(this, Resource.Id.list_item, syncDataArray);
            syncDataListView.Adapter = syncDataAdapter;

            //GridView gridView = (GridView)FindViewById(Resource.Id.syncGridView);

        }
    }
}

