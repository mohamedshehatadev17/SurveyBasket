
namespace SurveyBasket.Api.ContractMapping
{
    public static class ContractMapping
    {
        public static PollResponse MapToResponse(this Poll poll)
        {
            return new()
            {
                Id = poll.Id,
                Description = poll.Description,
                Title = poll.Title,
            };
        }
        public static IEnumerable<PollResponse> MapToResponse(this IEnumerable<Poll> polls)
        {
            return polls.Select(MapToResponse);
        }
        public static Poll MapToPull(this CreatePollRequest request)
        {
            return new()
            {
                Title = request.Title,
                Description = request.Description
            };
        }
    }
}
