using System.ComponentModel.DataAnnotations;

namespace WebApi.Model;

public class AuthenticationResponse
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}