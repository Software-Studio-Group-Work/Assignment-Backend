namespace Backend.Models;
using System.ComponentModel.DataAnnotations;
public class Login{

    [Required(ErrorMessage = "username is required.")]
     public string username { get; set; } = null!;
     
    [Required(ErrorMessage = "password is required.")]
    public string password { get; set; }= null!;

}