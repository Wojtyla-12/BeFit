
﻿using BeFitt.Data;
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



    public class SesjasController : Controller
    {
        private readonly ApplicationDbContext _context;


        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        } 

        public SesjasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sesjas
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            var sesje = await _context.Sesja
                .Where(s => s.CreatedById == userId)
                .ToListAsync();

            return View(sesje);
        }

        // GET: Sesjas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var userId = GetUserId();


            var sesja = await _context.Sesja
                .FirstOrDefaultAsync(m => m.Id == id && m.CreatedById == userId);

            
            if (sesja == null) return NotFound();

            return View(sesja);
        }

        // GET: Sesjas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sesjas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SesjaDTO sesjaDTO)
        {
            if (ModelState.IsValid)
            {
                var sesja = new Sesja
                {
                    Dzien_Start = sesjaDTO.Dzien_Start,
                    Start_Czas = sesjaDTO.Start_Czas,
                    Koniec_Czas = sesjaDTO.Koniec_Czas,
                    Dzien_Koniec = sesjaDTO.Dzien_Koniec,

                    CreatedById = GetUserId()
                };

                _context.Add(sesja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sesjaDTO);
        }


        // GET: Sesjas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var userId = GetUserId();


            var sesja = await _context.Sesja.FirstOrDefaultAsync(s => s.Id == id && s.CreatedById == userId);

            if (sesja == null) return NotFound(); 

            return View(sesja);
        }

        // POST: Sesjas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sesja sesja)
        {
            if (id != sesja.Id) return NotFound();

            // ZMIANA 6: Upewniamy się, że nie podmieniono ID w formularzu na cudze
            // (Dla uproszczenia zakładamy, że pole CreatedById jest ukryte w formularzu lub pominięte)
            // Najbezpieczniej jest pobrać oryginał z bazy:

            var userId = GetUserId();
            var originalSesja = await _context.Sesja.AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id && s.CreatedById == userId);

            if (originalSesja == null) return NotFound(); // Próba hackowania

            // Przywracamy ID autora, żeby nie zginęło przy edycji
            sesja.CreatedById = userId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sesja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SesjaExists(sesja.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sesja);
        }

        // GET: Sesjas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var userId = GetUserId();

            // ZMIANA 7: Sprawdzamy właściciela
            var sesja = await _context.Sesja
                .FirstOrDefaultAsync(m => m.Id == id && m.CreatedById == userId);

            if (sesja == null) return NotFound();

            return View(sesja);
        }

        // POST: Sesjas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserId();

            // ZMIANA 8: Usuwamy tylko jeśli należy do nas
            var sesja = await _context.Sesja
                 .FirstOrDefaultAsync(m => m.Id == id && m.CreatedById == userId);

            if (sesja != null)
            {
                _context.Sesja.Remove(sesja);
                await _context.SaveChangesAsync();
            }
            // Jeśli sesja była null (cudza), po prostu przekierowujemy, nic nie robiąc
            return RedirectToAction(nameof(Index));
        }

        private bool SesjaExists(int id)
        {
            var userId = GetUserId();
            // ZMIANA 9: Metoda pomocnicza też musi sprawdzać usera!
            return _context.Sesja.Any(e => e.Id == id && e.CreatedById == userId);
        }
    }
}
