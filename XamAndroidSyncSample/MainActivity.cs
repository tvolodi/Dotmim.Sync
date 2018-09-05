using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Collections.Generic;
using XamAndroidSyncSample.DataServices;
using Autofac;
using System.Linq;
using System;
using System.Collections;
using Dotmim.Sync.Sqlite;
using XamAndroidSyncSample.Model;
using Dotmim.Sync.Web.Client;
using Dotmim.Sync;
using Java.Net;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace XamAndroidSyncSample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity 
    {
        private ArrayAdapter<string> syncDataAdapter;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            ListView syncDataListView = FindViewById<ListView>(Resource.Id.syncDataListView);

            List<EmployeeDtoOutput> employees = new List<EmployeeDtoOutput>();

            List<string> arrayList = new List<string>();
            // string[] syncDataArray; // new string[] { "test", "test2" };
            using (var scope = ServicesContainer.Container.BeginLifetimeScope())
            {
                try
                {
                    var employeeService = scope.Resolve<IEmployeeService>();
                    employees = await employeeService.GetAllAsync();
                } catch (Exception e)
                {
                    string msg = e.ToString();
                }
            }

            arrayList = employees.Select(dto => dto.EmployeeId.ToString()).ToList();
            syncDataAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arrayList);
            // ListAdapter = new ArrayAdapter<string>(this, Resource.Id.list_item, syncDataArray);
            syncDataListView.Adapter = syncDataAdapter;

            //GridView gridView = (GridView)FindViewById(Resource.Id.syncGridView);

            #region AddEmployeeButton
            Button addEmployeeButton = FindViewById<Button>(Resource.Id.addEmployeesButton);
            addEmployeeButton.Click += (o, e) =>
            {
                using (var scope = ServicesContainer.Container.BeginLifetimeScope())
                {
                    var employeeService = scope.Resolve<IEmployeeService>();
                    int addedRecQnt = employeeService.AddEmployees();
                    if (addedRecQnt > 0)
                    {
                        syncDataAdapter.Clear();
                        //    List<EmployeeDtoOutput> employeeDtoOutputs = await employeeService.GetAllAsync();

                        //    syncDataAdapter.AddAll(employeeDtoOutputs.Select(employeeDTO => employeeDTO.EmployeeId.ToString()).ToList());
                        //
                    }

                    Toast.MakeText(this, addedRecQnt.ToString(), ToastLength.Short).Show();
                }
            }; 
            #endregion

            // Button refreshLocalButton and its events handlers
            Button refreshLocalButton = FindViewById<Button>(Resource.Id.refreshLocalButton);
            
            // Click event
            refreshLocalButton.Click += async (o, e) =>
            {
                using (var scope = ServicesContainer.Container.BeginLifetimeScope())
                {
                    var employeeService = scope.Resolve<IEmployeeService>();
                    syncDataAdapter.Clear();

                    List<EmployeeDtoOutput> employeeDtoOutputs = await employeeService.GetAllAsync();

                    syncDataAdapter.AddAll(employeeDtoOutputs.Select(employeeDTO => employeeDTO.EmployeeId.ToString()).ToList());

                    Toast.MakeText(this, "Refreshed", ToastLength.Long).Show();
                }
            };

            // Button syncButton and its events handlers
            Button syncButton = FindViewById<Button>(Resource.Id.syncButton);

            // Click event
            syncButton.Click += async (o, e) =>
            {

                syncButton.Text = await ExecReq();

                //var clientProvider = new SqliteSyncProvider();
                //clientProvider.ConnectionString = SqLiteBaseRepository.DbFile;
                ////clientProvider.CreateConnection();

                //var proxyClientProvider = new WebProxyClientProvider(new Uri("http://192.168.1.220:5000/api/sync"));
                //var syncAgent = new SyncAgent(clientProvider, proxyClientProvider);
                //// syncAgent.SyncProgress += (s, a) => a.Message + a.PropertiesMessage;
                //try
                //{
                //    var syncContext = await syncAgent.SynchronizeAsync();
                //} catch (Exception ex)
                //{
                //    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                //}

            };

        }

        private void SyncAgent_SyncProgress(object sender, ProgressEventArgs e)
        {
            
        }

        private async Task<string> ExecReq()
        {
            string result = string.Empty;

            string urlString = "http://192.168.1.220:5000/api/sync";

            //URL url = new URL();
            //URLConnection urlConn = url.OpenConnection();

            Uri uri = new Uri(urlString);

            // Drop to OS here. Try/catch in no help. 
            // The little note: last versions of Android are not using HttpClient but 
            // URL.OpenConnection plus Read Stream

            //var httpReqHandler = new HttpRequestHandler(uri, CancellationToken.None);
            //var httpResult = await httpReqHandler.ProcessRequest<string>("", Dotmim.Sync.Enumerations.SerializationFormat.Json, CancellationToken.None);

            // OK response
            // Would this code put into HttpRequestHandler class and it will cause the drop to OS 
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync("http://192.168.1.220:5000/api/test", new StringContent(""));


            // Put start of response as button text
            if(response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
                int readLength = Math.Min(result.Length, 20);
                result = result.Substring(0, readLength);
            } else
            {
                result = "error" + response.StatusCode;
            }

            return result;
        }

        private async Task<string> ExecSync()
        {

            string result = string.Empty;



            var clientProvider = new SqliteSyncProvider();
            clientProvider.ConnectionString = SqLiteBaseRepository.DbFile;
            ////clientProvider.CreateConnection();

            var proxyClientProvider = new WebProxyClientProvider(new Uri("http://192.168.1.220:5000/api/sync"));
            var syncAgent = new SyncAgent(clientProvider, proxyClientProvider);
            //// syncAgent.SyncProgress += (s, a) => a.Message + a.PropertiesMessage;
            try
            {
                var syncContext = await syncAgent.SynchronizeAsync();
                result = "Synchronized";
            } catch (Exception ex)
            {
                int readLength = Math.Min(ex.Message.Length, 20);
                result = ex.Message.Substring(0, readLength);
            }


            ////URL url = new URL("http://192.168.1.220:5000/api/sync");
            ////URLConnection urlConn = url.OpenConnection();
            //var httpClient = new HttpClient();
            //var response = await httpClient.GetAsync("http://192.168.1.220:5000/api/sync");

            //if (response.IsSuccessStatusCode)
            //{
            //    result = await response.Content.ReadAsStringAsync();
            //    int readLength = Math.Min(result.Length, 20);
            //    result = result.Substring(0, readLength);
            //}
            //else
            //{
            //    result = "error" + response.StatusCode;
            //}

            return result;
        }
    }
}

