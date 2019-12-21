using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Munkabeosztas_ASP_NET_Core.Data;
using Munkabeosztas_ASP_NET_Core.Models;

namespace Munkabeosztas_ASP_NET_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly MunkabeosztasDbContext _context;

        public HomeController(MunkabeosztasDbContext context)
        {
            _context = context;
        }

        // GET: Munkak
        public async Task<IActionResult> Index()
        {
            var munkabeosztasDbContext = _context.Munkak.Include(m => m.Gepjarmu);
            return View(await munkabeosztasDbContext.ToListAsync());
        }

        // GET: Munkak/Create
        public IActionResult Create()
        {
            MunkaViewModel temp = new MunkaViewModel
            {
                GepjarmuList = GetGepjarmuvek(),
                DolgozoList = GetDolgozokWithCheck()
            };

            return View(temp);
        }

        // POST: Munkak/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MunkaId,Helyszin,Datum,Leiras,GepjarmuId")] Munka munka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(munka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GepjarmuId"] = new SelectList(_context.Gepjarmuvek, "GepjarmuId", "Rendszam", munka.GepjarmuId);
            return View(munka);
        }

        // GET: Munkak/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var munka = await _context.Munkak.FindAsync(id);
            if (munka == null)
            {
                return NotFound();
            }
            ViewData["GepjarmuId"] = new SelectList(_context.Gepjarmuvek, "GepjarmuId", "Rendszam", munka.GepjarmuId);
            return View(munka);
        }

        // POST: Munkak/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MunkaId,Helyszin,Datum,Leiras,GepjarmuId")] Munka munka)
        {
            if (id != munka.MunkaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(munka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MunkaExists(munka.MunkaId))
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
            ViewData["GepjarmuId"] = new SelectList(_context.Gepjarmuvek, "GepjarmuId", "Rendszam", munka.GepjarmuId);
            return View(munka);
        }

        // GET: Munkak/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var munka = await _context.Munkak
                .Include(m => m.Gepjarmu)
                .FirstOrDefaultAsync(m => m.MunkaId == id);
            if (munka == null)
            {
                return NotFound();
            }

            return View(munka);
        }

        // POST: Munkak/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var munka = await _context.Munkak.FindAsync(id);
            _context.Munkak.Remove(munka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MunkaExists(int id)
        {
            return _context.Munkak.Any(e => e.MunkaId == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IEnumerable<SelectListItem> GetGepjarmuvek()
        {
            List<SelectListItem> gepjarmuvek = _context.Gepjarmuvek.AsNoTracking()
                    .OrderBy(n => n.Tipus)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.GepjarmuId.ToString(),
                            Text = n.Tipus + " (" + n.Rendszam + ")"
                        }).ToList();
            return new SelectList(gepjarmuvek, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetDolgozok()
        {
            List<SelectListItem> dolgozok = _context.Dolgozok.AsNoTracking()
                    .OrderBy(n => n.Csaladnev)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.DolgozoId.ToString(),
                            Text = n.Csaladnev + " " + n.Keresztnev
                        }).ToList();
            return new SelectList(dolgozok, "Value", "Text").AsEnumerable();
        }

        private List<DolgozoMunkaViewModel> GetDolgozokWithCheck()
        {
            List<DolgozoMunkaViewModel> dolgozok = _context.Dolgozok.AsNoTracking()
                .OrderBy(n => n.Csaladnev)
                    .Select(n =>
                    new DolgozoMunkaViewModel
                    {
                        DolgozoId = n.DolgozoId,
                        Csaladnev = n.Csaladnev,
                        Keresztnev = n.Keresztnev,
                        IsChecked = false
                    }).ToList();
            return dolgozok;
        }
    }
}
