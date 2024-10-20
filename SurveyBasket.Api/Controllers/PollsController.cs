
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            var pollsResponse = polls.Adapt<IEnumerable<PollResponse> >();
            return Ok(pollsResponse);
        }
        [HttpGet("{id}")] 
        public IActionResult Get([FromRoute] int id)
        {
            var Poll = _pollService.Get(id);
            var pollResponse =Poll.Adapt<PollResponse>();

            return Poll is null ? NotFound():Ok(pollResponse);
        }
        [HttpPost]
        public IActionResult Add([FromBody] CreatePollRequest request)
        {
           
            var pollRequest = request.Adapt<Poll>();
            var newPoll= _pollService.Add(pollRequest);
            return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id,[FromBody] CreatePollRequest poll)
        {
            var pollRequest = poll.Adapt<Poll>();
            var isUpdated = _pollService.Update(id, pollRequest);
            if (!isUpdated)
                return NotFound();
            
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var isDeleted =_pollService.Delete(id);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
    }













}
