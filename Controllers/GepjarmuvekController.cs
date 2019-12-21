using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Munkabeosztas_ASP_NET_Core.Data;
using Munkabeosztas_ASP_NET_Core.Models;

namespace Munkabeosztas_ASP_NET_Core.Controllers
{
    public class GepjarmuvekController : Controller
    {
        private readonly MunkabeosztasDbContext _context;

        public GepjarmuvekController(MunkabeosztasDbContext context)
        {
            _context = context;
        }

        // GET: Gepjarmuvek
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gepjarmuvek.ToListAsync());
        }

        // GET: Gepjarmuvek/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gepjarmuvek/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GepjarmuId,Tipus,Rendszam")] Gepjarmu gepjarmu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gepjarmu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gepjarmu);
        }

        // GET: Gepjarmuvek/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gepjarmu = await _context.Gepjarmuvek.FindAsync(id);
            if (gepjarmu == null)
            {
                return NotFound();
            }
            return View(gepjarmu);
        }

        // POST: Gepjarmuvek/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GepjarmuId,Tipus,Rendszam")] Gepjarmu gepjarmu)
        {
            if (id != gepjarmu.GepjarmuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gepjarmu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GepjarmuExists(gepjarmu.GepjarmuId))
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
            return View(gepjarmu);
        }

        private bool GepjarmuExists(int id)
        {
            return _context.Gepjarmuvek.Any(e => e.GepjarmuId == id);
        }
    }
}
