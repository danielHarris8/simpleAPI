using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Meaurse.Models;
using Meaurse.Data;
using Microsoft.AspNetCore.Authorization;

namespace Meaurse.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class MeaurseController : ControllerBase
    {
      
        private readonly DataContext  _db;
        private readonly ILogger<MeaurseController> _logger;

        public MeaurseController(ILogger<MeaurseController> logger,DataContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<Value> Get()
        {
            return   _db.Meaurses.OrderByDescending(p => p.Id)
                       .FirstOrDefault();;
        }

        [HttpPost]
        [Authorize]
        public  ActionResult<Value> Add(float t, float h)
        {
            Value addData = new Value(){ temperature =t, humidity=h,time=DateTime.Now};
            _db.Add(addData);
           
            _db.SaveChanges();
             
            return addData;
        }

        // [Authorize]
        // [HttpGet]
        // public IEnumerable<string> Get()
        //     => new String[] {"John Doe", "Jane Doe"};
        
    }
}
