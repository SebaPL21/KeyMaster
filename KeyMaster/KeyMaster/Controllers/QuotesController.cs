using KeyMaster.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KeyMaster.Controllers
{
    [ApiController]
    [Route("api/quotes")]
    public class QuotesControler : ControllerBase
    {
   
        [HttpGet("fetch")]
        public ActionResult<QuotesModel> GetQuotes()
        {
            Random random = new Random();
            var rand = random.Next(151);
            using (StreamReader r = new StreamReader("C:\\Users\\sebas\\Documents\\KeyMaster\\KeyMaster\\KeyMaster\\Lessons\\quotes.json"))
            {
               var jsonString = r.ReadToEnd();
               var results = JsonConvert.DeserializeObject<List<QuotesModel>>(jsonString);
                if(results == null)
                    return StatusCode(StatusCodes.Status404NotFound);
               return results.First(x => x.Id == rand);
            }
        }
    };
}