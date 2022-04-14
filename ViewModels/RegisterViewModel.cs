using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Site.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        [Display(Name = "Логин")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Не указан электронный адрес")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Некорректный электронный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан год рождения", AllowEmptyStrings = true)]
        [Display(Name = "Год рождения")]
        [Range(1900, 2021, ErrorMessage = "{0} должен быть между {1} и {2} годом")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} должен иметь минимум {2} и максимум {1} символов", MinimumLength = 5)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}