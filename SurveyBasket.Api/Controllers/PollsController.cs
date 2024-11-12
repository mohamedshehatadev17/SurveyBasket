
using Microsoft.AspNetCore.Authorization;
using SurveyBasket.Api.Errors;

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
            var result = await _pollService.GetAsync(id,cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(PollErrors.PollNotFound);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PollRequest request,CancellationToken cancellationToken )
        {
            var pollRequest = request.Adapt<Poll>();
            var newPoll = await _pollService.AddAsync(pollRequest,cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll.Adapt<PollResponse>());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request,CancellationToken cancellationToken)
        {
            var result = await _pollService.UpdateAsync(id, request, cancellationToken);

		 return result.IsSuccess ? NoContent() : NotFound(PollErrors.PollNotFound);

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
