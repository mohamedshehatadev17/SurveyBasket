
using Microsoft.AspNetCore.Authorization;

namespace SurveyBasket.Api.Controllers
{

    [Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly IPollService _pollService;
        public PollsController(IPollService pollService)
        {
            _pollService = pollService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var polls= await _pollService.GetAllAsync(cancellationToken);
            var pollsResponse = polls.Adapt<IEnumerable<PollResponse> >();
            return Ok(pollsResponse);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
		{
            var Poll = await _pollService.GetAsync(id,cancellationToken);
            var pollResponse = Poll.Adapt<PollResponse>();

            return Poll is null ? NotFound() : Ok(pollResponse);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PollRequest request,CancellationToken cancellationToken )
        {
            var pollRequest = request.Adapt<Poll>();
            var newPoll = await _pollService.AddAsync(pollRequest,cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PollRequest poll,CancellationToken cancellationToken)
        {
            var pollRequest = poll.Adapt<Poll>();
            var isUpdated = await _pollService.UpdateAsync(id, pollRequest, cancellationToken);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _pollService.DeleteAsync(id, cancellationToken);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
        [HttpPut("{id}/togglePublish")]
        public async Task<IActionResult>TogglePublish([FromRoute] int id,CancellationToken cancellationToken)
        {
            var isUpdated = await _pollService.TogglePublishStatusAsync(id, cancellationToken);
            if(!isUpdated)
                return NotFound();
            return NoContent();
        }
	}













}
