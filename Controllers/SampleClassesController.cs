using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TutorialApplication.Data;
using TutorialApplication.Models;

namespace TutorialApplication.Controllers
{
    public class SampleClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SampleClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SampleClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.SampleClass.ToListAsync());
        }

        // GET: SampleClasses/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: SampleClasses/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String query)
        {
            return View("Index", await _context.SampleClass.Where( j => j.Property2.Contains(query)).ToListAsync());
        }

        // GET: SampleClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sampleClass = await _context.SampleClass
                .FirstOrDefaultAsync(m => m.id == id);
            if (sampleClass == null)
            {
                return NotFound();
            }

            return View(sampleClass);
        }

        [Authorize]
        // GET: SampleClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SampleClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Property,Property2")] SampleClass sampleClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sampleClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sampleClass);
        }

        [Authorize]
        // GET: SampleClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sampleClass = await _context.SampleClass.FindAsync(id);
            if (sampleClass == null)
            {
                return NotFound();
            }
            return View(sampleClass);
        }

        // POST: SampleClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Property,Property2")] SampleClass sampleClass)
        {
            if (id != sampleClass.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sampleClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SampleClassExists(sampleClass.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sampleClass);
        }

        [Authorize]
        // GET: SampleClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sampleClass = await _context.SampleClass
                .FirstOrDefaultAsync(m => m.id == id);
            if (sampleClass == null)
            {
                return NotFound();
            }

            return View(sampleClass);
        }

        // POST: SampleClasses/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sampleClass = await _context.SampleClass.FindAsync(id);
            _context.SampleClass.Remove(sampleClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SampleClassExists(int id)
        {
            return _context.SampleClass.Any(e => e.id == id);
        }
    }
}
