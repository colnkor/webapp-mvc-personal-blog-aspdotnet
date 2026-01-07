using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebBlog.Services.core;
using System.Threading.Tasks;

namespace PersonalWebBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleRepository _articleRepository;

        public HomeController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var articles = await _articleRepository.GetAllArticlesAsync();
            return View(articles);
        }

        [Route("article/{id}")]
        public async Task<IActionResult> Article(string id)
        {
            var article = await _articleRepository.GetArticleByIdAsync(id);
            if (article == null)
            {
                return RedirectToAction("Index");
            }
            return View(article);
        }
    }
}
