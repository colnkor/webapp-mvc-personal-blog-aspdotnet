using PersonalWebBlog.Models;
using PersonalWebBlog.Services.core;
using System.Text.Json;

namespace PersonalWebBlog.Services
{
    public class FileArticleRepository : IArticleRepository
    {
        private readonly string _folderPath;

        public FileArticleRepository(IWebHostEnvironment env)
        {
            _folderPath = Path.Combine(env.ContentRootPath, "Data", "Articles");
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            List<Article> articles = new List<Article>();
            var files = Directory.GetFiles(_folderPath);

            foreach (var file in files)
            {
                using var stream = File.OpenRead(file);
                var article = await JsonSerializer.DeserializeAsync<Article>(stream);

                if (article != null) articles.Add(article);
            }

            return articles;
        }

        public async Task<Article?> GetArticleByIdAsync(string id)
        {
            var filePath = Path.Combine(_folderPath, $"{id}.json");

            if (!File.Exists(filePath)) return null;

            using var stream = File.OpenRead(filePath);
            var article = await JsonSerializer.DeserializeAsync<Article>(stream);

            return article;
        }

        public async Task SaveArticle(Article article)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            using var stream = File.OpenWrite(Path.Combine(_folderPath, $"{article.Id}.json"));
            await JsonSerializer.SerializeAsync(stream, article, options);
        }

        public async Task SaveArticleWithCheck(Article article)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string filePath = Path.Combine(_folderPath, $"{article.Id}.json");
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Article (id: {article.Id}) to save is not found.");
            }
            using var stream = File.OpenWrite(Path.Combine(_folderPath, $"{article.Id}.json"));
            await JsonSerializer.SerializeAsync(stream, article, options);
        }

        public async Task DeleteArticle(string id)
        {
            string filePath = Path.Combine(_folderPath, $"{id}.json");
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Article (id: {id}) to delete is not found.");
            }
            File.Delete(filePath);
        }
    }
}
