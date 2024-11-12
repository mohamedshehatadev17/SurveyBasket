
using Microsoft.AspNetCore.Http.HttpResults;
using SurveyBasket.Api.Entities;
using SurveyBasket.Api.Errors;
using SurveyBasket.Api.Persistence;
using System.Collections.Generic;
using System.Threading;

namespace SurveyBasket.Api.Services
{
    public class PollService(ApplicationDbContext context) : IPollService
    {
		private readonly ApplicationDbContext _context =context;




		public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken) =>
			await _context.Polls.AsNoTracking().ToListAsync();

		public async Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken)
		{
			var poll = await _context.Polls.FindAsync(id);
			return poll is not null
				? Result.Success(poll.Adapt<PollResponse>())
				: Result.Failure<PollResponse>(PollErrors.PollNotFound);
		}


		public async Task<Poll> AddAsync(Poll poll, CancellationToken cancellationToken) 
		{
			  await _context.AddAsync(poll);
			await _context.SaveChangesAsync(cancellationToken);
			return poll;
		}

		public async Task<Result> UpdateAsync(int id, PollRequest poll, CancellationToken cancellationToken = default);
		{
			var currentPool = await _context.Polls.FindAsync(id);
			if (currentPool is null)
				return Result.Failure(PollErrors.PollNotFound);
			currentPool.Title = poll.Title;
			currentPool.Summary = poll.Summary;
			currentPool.StartsAt = poll.StartsAt;
			currentPool.EndsAt = poll.EndsAt;
			await _context.SaveChangesAsync(cancellationToken);
			return Result.Success();

		}

		public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken )
		{
			var poll = await GetAsync(id, cancellationToken);
			if (poll is null)
				return false;
			_context.Remove(poll);
			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}
		public async Task<bool> TogglePublishStatusAsync(int id, CancellationToken cancellation )
		{
			var poll = await GetAsync(id, cancellation);
			if (poll is null)
				return false;
			poll.IsPublished = !poll.IsPublished;
			return true;	
		}

	}
}
