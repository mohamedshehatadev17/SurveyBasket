
using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly IPollService _pollService;
        public PollsController(IPollService pollService)
        {
            _pollService = pollService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var polls=_pollService.GetAll();
            return Ok(polls);
        }
        [HttpGet("{id:int}")] 
        public IActionResult Get(int id)
        {
            var Poll = _pollService.Get(id);

            return Poll is null ? NotFound():Ok(Poll);
        }
        [HttpPost]
        public IActionResult Add(Poll poll)
        {
            var newPoll= _pollService.Add(poll);
            return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id,Poll poll)
        {
            var isUpdated = _pollService.Update(id,poll);
            if (!isUpdated)
                return NotFound();
            
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var isDeleted =_pollService.Delete(id);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
    }













}
