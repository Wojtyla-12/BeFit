 
﻿using BeFitt.Data;
using BeFitt.Models;
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
    public class StatystykiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatystykiController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetUserId(); 

            var dataGraniczna = DateTime.Now.Date.AddDays(-28);

            var daneZBazy = await _context.Trening_K
                .Include(t => t.Sesja)
                .Include(t => t.Typy)
                .Where(t => t.Sesja.Dzien_Start >= dataGraniczna)
                .Where(t => t.CreatedById == userId)
                .ToListAsync();


            var statystyki = daneZBazy
                .GroupBy(t => t.Typy.Name)
                .Select(g => new Statystyki
                {
                    NazwaCwiczenia = g.Key,

                    Ile_razy = g.Count(),
                    Ile_powtorzen = g.Sum(x => x.Seria * x.Powtorzenia),

                    Srednia = Math.Round(g.Average(x => x.Ciezar), 1),
                    Maks = g.Max(x => x.Ciezar)
                })
                .ToList();

            return View(statystyki);
        }
    }
}

