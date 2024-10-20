
namespace SurveyBasket.Api.Contracts.Validations
{
    public class CreatePollRequestValidator:AbstractValidator<CreatePollRequest>
    {
        public CreatePollRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("please add a title")
                .Length(3, 15);
            RuleFor(x => x.Description)
                .NotEmpty()
                .When(x=>x.Description.EndsWith("Thanks"))
                .Length(3, 100);
            
        }
    }
}
