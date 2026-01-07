using System.ComponentModel.DataAnnotations;

namespace PersonalWebBlog.Models
{
    public class Article
    {
        public string Id { get; set; } = "";

        [Required(ErrorMessage = "Введите заголовок")]
        [StringLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите содержание")]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
