﻿namespace SurveyBasket.Api.Abstractions
{
	public static class ResultExtensions
	{
		public static ObjectResult ToProblem(this Result result,int statusCode )
		{
			if (result.IsSuccess)
				throw new InvalidOperationException("Cannot convert success result to problem");
			var Problem = Results.Problem(statusCode: statusCode);
			var problemDetails = Problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(Problem) as ProblemDetails;
			problemDetails!.Extensions = new Dictionary<string, object?>
				{
					{
						"errors",new[]{result.Error}
					}
				};
		
		return new ObjectResult(problemDetails);
		}
	}
}
