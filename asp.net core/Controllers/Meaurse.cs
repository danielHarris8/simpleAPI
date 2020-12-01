using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using test.Models;

namespace test.Controllers
{
    [ApiController]
    [Route ("api/data")]
    public class Meaurse : ControllerBase {

        private readonly ILogger<Meaurse> _logger;
     

        public Meaurse (ILogger<Meaurse> logger) {
            _logger = logger;

           
        }

        [HttpGet]
        public IEnumerable<Data> Get () {
            Console.WriteLine("get it");
            using (StreamReader r = new StreamReader ("data.json")) {
                //Console.WriteLine("open");
                string json = r.ReadToEnd ();
                List<Data> items = JsonConvert.DeserializeObject<List<Data>> (json);
                //Console.WriteLine(item);
                return items;

            }
        }

        [HttpGet("last")]
        public ActionResult<Data> getLast()
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


        // ...api/data?t=20&h=60
        [HttpPost]
        public IEnumerable<Data> create (float t, float h) {
            List<Data> items;
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
                    return items;
            }

        }

    }
}
