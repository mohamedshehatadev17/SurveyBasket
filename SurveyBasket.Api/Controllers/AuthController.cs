
namespace SurveyBasket.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuthController(IAuthService authService,IOptions<JwtOptions> jwtOptions) : ControllerBase
	{
		private readonly JwtOptions _jwtOptions = jwtOptions.Value;
        private readonly IAuthService _authService = authService;

        [HttpPost("Login")]
		public async Task<IActionResult> LoginAsync(LoginRequest request,CancellationToken cancellationToken)
		{
			var authResult = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
			
			return authResult is null ? BadRequest("Invalid Email/Password"):Ok(authResult);
		}
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var authResult = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

            return authResult is null ? BadRequest("Invalid token") : Ok(authResult);
        }
    }
}
