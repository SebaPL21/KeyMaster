using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Xml.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;

namespace KeyMaster.Controllers
{
    [ApiController]
    [Route("api/lesson")]
    public class LessonLoadControler : ControllerBase
    {
       // token jvt oAuth
        [HttpGet("fetch")]
        public ActionResult<LessonModel> GetLesson()
        {
            Random random = new Random();
            var rand = random.Next(150);
            using (StreamReader r = new StreamReader("C:\\Users\\sebas\\Documents\\KeyMaster\\KeyMaster\\KeyMaster\\Lessons\\quotes.json"))
            {
               var jsonString = r.ReadToEnd();
               var results = JsonConvert.DeserializeObject<List<LessonModel>>(jsonString);
                if(results == null)
                    return StatusCode(StatusCodes.Status404NotFound);
               return results.First(x => x.Id == rand);
            }
        }
    };
}