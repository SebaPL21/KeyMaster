using KeyMaster.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KeyMaster.Controllers
{
    [Route("api/lesson")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("test")]
        public ActionResult<QuotesModel> GetTest()
        {
            Random random = new Random();
            var rand = random.Next(6);
            using (StreamReader r = new StreamReader("C:\\Users\\sebas\\Documents\\KeyMaster\\KeyMaster\\KeyMaster\\Lessons\\tests.json"))
            {
                var jsonString = r.ReadToEnd();
                var results = JsonConvert.DeserializeObject<List<QuotesModel>>(jsonString);

                if (results == null)
                    return StatusCode(StatusCodes.Status404NotFound);
                return results.First(x => x.Id == rand);
            }
        }
        
    }
}
