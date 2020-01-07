using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Munkabeosztas_ASP_NET_Core.Data;
using Munkabeosztas_ASP_NET_Core.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
            var listview = _context.Munkak
                .Include(m => m.Gepjarmu)
                .Include(m => m.DolgozoMunkak).ThenInclude(dm => dm.Dolgozo);

            HttpContext.Response.Headers.Add("refresh", "20; url=" + Url.Action("Index"));
            return View(await listview.ToListAsync());
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MunkaViewModel munka)
        {
            if (ModelState.IsValid)
            {
                // Új munka létrehozása és commit, hogy legyen munkaId, amire hivatkozok
                Munka ujmunka = new Munka
                {
                    Helyszin = munka.Helyszin,
                    Leiras = munka.Leiras,
                    Datum = munka.Datum,
                    GepjarmuId = int.Parse(munka.SelectedGepjarmu),
                    Gepjarmu = _context.Gepjarmuvek.Find(int.Parse(munka.SelectedGepjarmu))
                };
                _context.Set<Munka>().Add(ujmunka);
                await _context.SaveChangesAsync();
                // Több a többhöz kapcsolatok létrehozása
                foreach (var item in munka.DolgozoList)
                {
                    if (item.IsChecked)
                    {
                        var relship = new DolgozoMunka
                        {
                            MunkaId = ujmunka.MunkaId,
                            Munka = _context.Munkak.Find(ujmunka.MunkaId),
                            DolgozoId = item.DolgozoId,
                            Dolgozo = _context.Dolgozok.Find(item.DolgozoId)
                        };
                        _context.Set<DolgozoMunka>().Add(relship);
                        var dolgozo = _context.Dolgozok.Find(item.DolgozoId);
                        if (dolgozo != null)
                        {
                            dolgozo.DolgozoMunkak.Add(relship);
                            _context.Set<Dolgozo>().Update(dolgozo);
                        }
                        else
                        {
                            return NotFound();
                        }
                        ujmunka.DolgozoMunkak.Add(relship);
                        _context.Munkak.Update(ujmunka);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(munka);
        }

        // GET: Munkak/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var munka = await _context.Munkak
                .Include(m => m.Gepjarmu)
                .Include(m => m.DolgozoMunkak)
                .ThenInclude(dm => dm.Dolgozo)
                .FirstOrDefaultAsync(m => m.MunkaId == id);
            if (munka == null)
            {
                return NotFound();
            }
            List<DolgozoMunkaViewModel> dlist = GetDolgozokWithCheck();
            foreach (var item in munka.DolgozoMunkak)
            {
                foreach (var inneritem in dlist)
                {
                    if (item.DolgozoId == inneritem.DolgozoId)
                    {
                        inneritem.IsChecked = true;
                    }
                }
            }
            MunkaViewModel temp = new MunkaViewModel
            {
                MunkaId = munka.MunkaId,
                Datum = munka.Datum,
                Helyszin = munka.Helyszin,
                Leiras = munka.Leiras,
                SelectedGepjarmu = munka.Gepjarmu.GepjarmuId.ToString(),
                GepjarmuList = GetGepjarmuvek(),
                DolgozoList = dlist
            };
            return View(temp);
        }

        // POST: Munkak/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MunkaViewModel munka)
        {
            if (id != munka.MunkaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Munka rekord frissítése
                var editedmunka = await _context.Munkak
                    .Include(m => m.Gepjarmu)
                    .Include(m => m.DolgozoMunkak).ThenInclude(dm => dm.Dolgozo)
                    .FirstOrDefaultAsync(m => m.MunkaId == id);

                if (editedmunka == null)
                {
                    return NotFound();
                }

                editedmunka.Datum = munka.Datum;
                editedmunka.Helyszin = munka.Helyszin;
                editedmunka.Leiras = munka.Leiras;
                editedmunka.GepjarmuId = int.Parse(munka.SelectedGepjarmu);
                editedmunka.Gepjarmu = await _context.Gepjarmuvek.FindAsync(int.Parse(munka.SelectedGepjarmu));

                // Dolgozo-Munka kapcsolat frissítése
                foreach (var item in munka.DolgozoList)
                {
                    var relship = new DolgozoMunka
                    {
                        MunkaId = munka.MunkaId,
                        Munka = await _context.Set<Munka>().FindAsync(munka.MunkaId),
                        DolgozoId = item.DolgozoId,
                        Dolgozo = await _context.Set<Dolgozo>().FindAsync(item.DolgozoId)
                    };

                    bool relshipExists = await _context.Set<DolgozoMunka>()
                        .AnyAsync(dm => dm.DolgozoId == item.DolgozoId && dm.MunkaId == munka.MunkaId);

                    if (item.IsChecked && !relshipExists)
                    {
                        // Új kapcsolat létrehozása
                        await _context.Set<DolgozoMunka>().AddAsync(relship);
                        var dolgozo = await _context.Dolgozok.FindAsync(item.DolgozoId);
                        if (dolgozo != null)
                        {
                            dolgozo.DolgozoMunkak.Add(relship);
                            _context.Set<Dolgozo>().Update(dolgozo);
                        }
                        else
                        {
                            return NotFound();
                        }
                        editedmunka.DolgozoMunkak.Add(relship);

                    } else
                    {
                        if (!item.IsChecked && relshipExists)
                        {
                            // Meglévő kapcsolat törlése
                            var temp = await _context.Set<DolgozoMunka>()
                                .FindAsync(item.DolgozoId, munka.MunkaId);
                            editedmunka.DolgozoMunkak.Remove(temp);
                        }
                    }
                }
                try
                {
                    //_context.Munkak.Update(editedmunka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MunkaExists(editedmunka.MunkaId))
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
                .Include(m => m.DolgozoMunkak).ThenInclude(dm => dm.Dolgozo)
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
            var munka = await _context.Munkak
                .Include(m => m.Gepjarmu)
                .Include(m => m.DolgozoMunkak)
                .ThenInclude(md => md.Dolgozo)
                .SingleOrDefaultAsync(m => m.MunkaId == id);
            if (munka != null)
            {
                _context.Munkak.Remove(munka);
            }
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
