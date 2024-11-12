namespace SurveyBasket.Api.Errors
{
	public class PollErrors
	{
		public static readonly Error PollNotFound = new Error("Poll.NotFound", "No Poll was found with the given ID");
	}
}
