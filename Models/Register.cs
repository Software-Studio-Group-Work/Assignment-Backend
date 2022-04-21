

namespace Backend.Models;

public class Register
{

    public string username { get; set; } = null!;

    public string password { get; set; }= null!;

    public string name { get; set; } = null!;

    public IFormFile  picture { get; set; } = null!;
    public string religion { get; set; } = null!;
    public string role { get; set; } = null!;
    public bool isBan { get; set; } 



}