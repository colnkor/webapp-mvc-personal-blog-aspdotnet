using System.ComponentModel.DataAnnotations;

public class Login
{
    [Required(ErrorMessage = "Введите логин")]
    [Display(Name = "Логин")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Display(Name = "Запомнить меня")]
    public bool RememberMe { get; set; }
}