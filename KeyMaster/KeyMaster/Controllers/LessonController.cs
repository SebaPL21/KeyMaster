using KeyMaster.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KeyMaster.Controllers
{
    [Route("api/lesson")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<LessonModel> GetLesson(int id)
        {
            using (StreamReader r = new StreamReader("C:\\Users\\sebas\\Documents\\KeyMaster\\KeyMaster\\KeyMaster\\Lessons\\lessons.json"))
            {
                var jsonString = r.ReadToEnd();
                var results = JsonConvert.DeserializeObject<List<LessonModel>>(jsonString);

                if (results == null)
                    return StatusCode(StatusCodes.Status404NotFound);
                return results.First(x => x.Id == id);
            }
        }
    }
}
