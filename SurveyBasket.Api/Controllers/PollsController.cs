
using SurveyBasket.Api.ContractMapping;
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
            return Ok(polls.MapToResponse());
        }
        [HttpGet("{id:int}")] 
        public IActionResult Get([FromRoute] int id)
        {
            var Poll = _pollService.Get(id);

            return Poll is null ? NotFound():Ok(Poll.MapToResponse());
        }
        [HttpPost]
        public IActionResult Add([FromBody] CreatePollRequest poll)
        {
            var newPoll= _pollService.Add(poll.MapToPull());
            return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
        }
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id,[FromBody] CreatePollRequest poll)
        {
            var isUpdated = _pollService.Update(id,poll.MapToPull());
            if (!isUpdated)
                return NotFound();
            
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var isDeleted =_pollService.Delete(id);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
    }













}
