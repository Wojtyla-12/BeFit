using BeFitt.Data;
using BeFitt.Models;
using BeFitt.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace BeFitt.Controllers
{
    [Authorize] 
    public class Trening_KController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Trening_KController(ApplicationDbContext context)
        {
            _context = context;
        }
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: Trening_K
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            var treningi = await _context.Trening_K
                .Include(t => t.Sesja)
                .Include(t => t.Typy) 
                .Where(t => t.CreatedById == userId) 
                .ToListAsync();

            return View(treningi);
        }

        // GET: Trening_K/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var userId = GetUserId();

            var trening_K = await _context.Trening_K
                .Include(t => t.Sesja)
                .Include(t => t.Typy)
                .FirstOrDefaultAsync(m => m.Id == id && m.CreatedById == userId);

            if (trening_K == null) return NotFound();

            return View(trening_K);
        }

        // GET: Trening_K/Create
        public IActionResult Create()
        {
            var userId = GetUserId();

            ViewData["SesjaId"] = new SelectList(_context.Sesja.Where(s => s.CreatedById == userId), "Id", "Dzien_Start");
            ViewData["TypyId"] = new SelectList(_context.Typy, "Id", "Name");

            return View();
        }

        // POST: Trening_K/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TreningDTO treningDTO) 
        {
            if (ModelState.IsValid)
            {
                var nowyTrening = new Trening_K
                {
                    SesjaId = treningDTO.SesjaId,
                    TypyId = treningDTO.TypyId, 
                    Ciezar = treningDTO.Ciezar,
                    Seria = treningDTO.Seria,
                    Powtorzenia = treningDTO.Powtorzenia,

                    CreatedById = GetUserId()
                };

                _context.Add(nowyTrening);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var userId = GetUserId();
            ViewData["SesjaId"] = new SelectList(_context.Sesja.Where(s => s.CreatedById == userId), "Id", "Dzien_Start", treningDTO.SesjaId);
            ViewData["TypyId"] = new SelectList(_context.Typy, "Id", "Name", treningDTO.TypyId);

            return View(treningDTO);
        }

        // GET: Trening_K/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var userId = GetUserId();

            var trening_K = await _context.Trening_K
                .FirstOrDefaultAsync(t => t.Id == id && t.CreatedById == userId);

            if (trening_K == null) return NotFound();

            ViewData["SesjaId"] = new SelectList(_context.Sesja.Where(s => s.CreatedById == userId), "Id", "Dzien_Start", trening_K.SesjaId);
            ViewData["TypyId"] = new SelectList(_context.Typy, "Id", "Name", trening_K.TypyId);

            return View(trening_K);
        }

        // POST: Trening_K/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trening_K trening_K)
        {
            if (id != trening_K.Id) return NotFound();

            var userId = GetUserId();

            var originalTrening = await _context.Trening_K.AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id && t.CreatedById == userId);

            if (originalTrening == null) return NotFound(); 

            trening_K.CreatedById = userId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trening_K);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Trening_KExists(trening_K.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["SesjaId"] = new SelectList(_context.Sesja.Where(s => s.CreatedById == userId), "Id", "Dzien_Start", trening_K.SesjaId);
            ViewData["TypyId"] = new SelectList(_context.Typy, "Id", "Name", trening_K.TypyId);

            return View(trening_K);
        }

        // GET: Trening_K/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var userId = GetUserId();

            var trening_K = await _context.Trening_K
                .Include(t => t.Sesja)
                .Include(t => t.Typy)
                .FirstOrDefaultAsync(m => m.Id == id && m.CreatedById == userId);

            if (trening_K == null) return NotFound();

            return View(trening_K);
        }

        // POST: Trening_K/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserId();

            // Usuwamy tylko jeśli należy do usera
            var trening_K = await _context.Trening_K
                .FirstOrDefaultAsync(t => t.Id == id && t.CreatedById == userId);

            if (trening_K != null)
            {
                _context.Trening_K.Remove(trening_K);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool Trening_KExists(int id)
        {
            var userId = GetUserId();
            // Sprawdzamy istnienie + własność
            return _context.Trening_K.Any(e => e.Id == id && e.CreatedById == userId);
        }
    }
}
