

namespace SurveyBasket.Api.Contracts.Validations
{
	public class PollRequestValidator:AbstractValidator<PollRequest>
    {
        public PollRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("please add a title")
                .Length(3, 15);
            RuleFor(x => x.Summary)
                .NotEmpty()
                .Length(3, 1500);
            RuleFor(x => x.StartsAt)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));
            RuleFor(x => x)
                .Must(HasValidDates)
                .WithName(nameof(PollRequest.EndsAt))
                .WithMessage("{PropertyName} must be greater than or equals start date"); 
            
        }
        
        private bool HasValidDates (PollRequest pollRequest)
        {
            return pollRequest.EndsAt >= pollRequest.StartsAt;
        }
    }
}
