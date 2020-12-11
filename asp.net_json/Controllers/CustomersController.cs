using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using testJwtApi.Models;


namespace testJwtApi.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class CustomersController : ControllerBase
    {
       
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<Data> Get()
        {
            Console.WriteLine("Get Data");
            Data lastItem = null;
            using (StreamReader r = new StreamReader ("data.json")) {
                string json = r.ReadToEnd ();
                List<Data> items = JsonConvert.DeserializeObject<List<Data>> (json);
                if(items.Count>=1)
                {
                    lastItem = items[items.Count -1];
                    Console.WriteLine("temperature = {0}, humidity = {1}",lastItem.temperature,lastItem.humidity);
                }
                   
                return lastItem;   
            
            }
        }

        [HttpPost]
        [Authorize]
        public  ActionResult<Data> Add(float t, float h)
        {
            List<Data> items;
            Data lastItem = null;
            using (StreamReader rr = new StreamReader ("data.json")) {
                string json = rr.ReadToEnd ();
                items = JsonConvert.DeserializeObject<List<Data>> (json);
                items.Add (new Data () { temperature = t, humidity = h });
               
            }
            using (StreamWriter rw = new StreamWriter ("data.json")) {
                JsonSerializer serializer = new JsonSerializer ();
                //serialize object directly into file stream
                serializer.Serialize (rw, items);

                Console.WriteLine("Post Data");
                Console.WriteLine("temperature = {0}, humidity = {1}",t,h);
                lastItem = items[items.Count -1];
            }
            return lastItem;
        }

    }
}
