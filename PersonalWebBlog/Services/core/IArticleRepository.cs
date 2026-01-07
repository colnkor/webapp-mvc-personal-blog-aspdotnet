using PersonalWebBlog.Models;

namespace PersonalWebBlog.Services.core
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllArticlesAsync();
        Task<Article?> GetArticleByIdAsync(string id);
        Task SaveArticle(Article article);
        Task SaveArticleWithCheck(Article article);
        Task DeleteArticle(string id);
    }
}
