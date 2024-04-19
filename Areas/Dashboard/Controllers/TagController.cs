using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Areas.Dashboard.Controllers;
   [Area("Dashboard")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tags = _context.Tags.ToList();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string tagName)
        {
            Tag tag = new()
            {
                UpdatedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                TagName = tagName
            };
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var tag = _context.Tags.SingleOrDefault(x => x.Id == id);
            return View(tag);
        }

        [HttpPost]
        public IActionResult Edit(Tag tag)
        {
            tag.UpdatedDate = DateTime.Now;
            _context.Tags.Update(tag);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

