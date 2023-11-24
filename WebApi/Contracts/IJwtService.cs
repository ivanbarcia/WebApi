using WebApi.Model;

namespace WebApi.Contracts
{
	public interface IJwtService
	{
		AuthenticationResponse CreateToken(User user);
	}
}