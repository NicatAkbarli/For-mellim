using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class ArticleViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ArticleViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Tüm kategorileri al
            var categories = await _context.Categories.ToListAsync();

            // Her kategori için ilgili makaleleri al
            var articlesByCategory = new Dictionary<string, List<Article>>();
            foreach (var category in categories)
            {
                var articles = await GetArticlesByCategoryAsync(category.Id);
                articlesByCategory.Add(category.CategoryName, articles);
            }

            return View("Article", articlesByCategory);
        }

        private async Task<List<Article>> GetArticlesByCategoryAsync(int categoryId)
        {
            // Kategori Id'sine göre ilgili makaleleri getir
            var articles = await _context.Articles
                .Where(a => a.CategoryId == categoryId)
                .ToListAsync();

            return articles;
        }
    }
}
