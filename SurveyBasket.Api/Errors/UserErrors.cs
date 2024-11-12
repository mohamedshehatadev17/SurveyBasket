namespace SurveyBasket.Api.Errors
{
	public static class UserErrors
	{
		public static readonly Error InvalidCredentials = new("User.InvalidCredentials", "Invalid Email/Password");
	}
}
