using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotmim.Sync.Web.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XamAndroidSyncSampleWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        // proxy to handle requests and send them to SqlSyncProvider
        private WebProxyServerProvider webProxyServer;

        // Injected thanks to Dependency Injection
        public SyncController(WebProxyServerProvider proxy)
        {
            webProxyServer = proxy;
        }

        [HttpGet]
        public int GetTestInt()
        {
            return 123;
        }

        // Handle all requests :)
        [HttpPost]
        public async Task Post()
        {
            try
            {
                await webProxyServer.HandleRequestAsync(this.HttpContext);
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }
    }
}