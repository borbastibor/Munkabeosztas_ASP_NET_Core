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
    public class DolgozokController : Controller
    {
        private readonly MunkabeosztasDbContext _context;

        public DolgozokController(MunkabeosztasDbContext context)
        {
            _context = context;
        }

        // GET: Dolgozok
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dolgozok.ToListAsync());
        }

        // GET: Dolgozok/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dolgozok/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DolgozoId,Csaladnev,Keresztnev")] Dolgozo dolgozo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dolgozo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dolgozo);
        }

        // GET: Dolgozok/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dolgozo = await _context.Dolgozok.FindAsync(id);
            if (dolgozo == null)
            {
                return NotFound();
            }
            return View(dolgozo);
        }

        // POST: Dolgozok/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DolgozoId,Csaladnev,Keresztnev")] Dolgozo dolgozo)
        {
            if (id != dolgozo.DolgozoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dolgozo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DolgozoExists(dolgozo.DolgozoId))
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
            return View(dolgozo);
        }

        private bool DolgozoExists(int id)
        {
            return _context.Dolgozok.Any(e => e.DolgozoId == id);
        }
    }
}
