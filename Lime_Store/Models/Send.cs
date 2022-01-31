using System.ComponentModel.DataAnnotations;

namespace Lime_Store.Models
{
    public class Send
    {
        [Required(ErrorMessage = "Не указано Имя")]
        public   string Name { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Required(ErrorMessage = "Не указан E-mail")]
        public   string Email { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public   string Message { get; set; }

    }
}
