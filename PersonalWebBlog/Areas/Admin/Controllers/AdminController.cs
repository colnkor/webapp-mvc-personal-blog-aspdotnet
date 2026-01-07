using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalWebBlog.Models;
using PersonalWebBlog.Services.core;
using System.Security.Cryptography;
using System.Text;

namespace PersonalWebBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IArticleRepository _articleRepository;

        public AdminController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var articles = await _articleRepository.GetAllArticlesAsync();
            return View(articles);
        }

        [Route("new")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("new")]
        public async Task<IActionResult> New(Article model)
        { 
            byte[] bytes = Encoding.UTF8.GetBytes(model.CreatedAt.ToString() + model.Title);
            var hashBytes = SHA256.HashData(bytes);
            model.Id = Convert.ToHexStringLower(hashBytes);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Form is invalid");
                return View(model);
            }
            await _articleRepository.SaveArticle(model);
            return RedirectToAction("Edit", "Admin", model.Id);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var article = await _articleRepository.GetArticleByIdAsync(id);
            if (article == null)
            {
                return RedirectToAction("Index");
            }
            return View(article);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(string id, Article article)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Form is invalid");
                return View(article);
            }

            article.Id = id;
            try
            {
                await _articleRepository.SaveArticleWithCheck(article);
            } catch (Exception e)
            {
                return RedirectToAction("Index");
            }
            return View(article);
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _articleRepository.DeleteArticle(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            } 
            return RedirectToAction("Index");
        }
    }
}
