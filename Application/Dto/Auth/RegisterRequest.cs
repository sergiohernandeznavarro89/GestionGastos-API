namespace Application.Dto;

public class RegisterRequest
{
    public string UserName { get; set; }
    public string UserLastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
