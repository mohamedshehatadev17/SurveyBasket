namespace SurveyBasket.Api.Services
{
    public interface IPollService
    {
        Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<PollResponse>> GetAsync(int  id, CancellationToken cancellationToken);
        Task<Poll> AddAsync(Poll poll, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(int id,PollRequest poll, CancellationToken cancellationToken = default);
		Task<bool> DeleteAsync(int id,CancellationToken cancellationToken=default);
        Task<bool> TogglePublishStatusAsync(int id,CancellationToken cancellation=default);

	}
}
