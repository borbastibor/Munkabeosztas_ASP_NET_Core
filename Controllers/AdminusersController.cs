using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Munkabeosztas_ASP_NET_Core.Data;
using Munkabeosztas_ASP_NET_Core.Models;

namespace Munkabeosztas_ASP_NET_Core.Controllers
{
    public class AdminusersController : Controller
    {
        private readonly MunkabeosztasDbContext _context;

        public AdminusersController(MunkabeosztasDbContext context)
        {
            _context = context;
        }

        // GET: Adminusers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Adminusers.ToListAsync());
        }

        // GET: Adminusers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adminusers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminuserId,Username")] Adminuser adminuser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminuser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminuser);
        }

        // GET: Adminusers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminuser = await _context.Adminusers.FindAsync(id);
            if (adminuser == null)
            {
                return NotFound();
            }
            return View(adminuser);
        }

        // POST: Adminusers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminuserId,Username")] Adminuser adminuser)
        {
            if (id != adminuser.AdminuserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminuser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminuserExists(adminuser.AdminuserId))
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
            return View(adminuser);
        }

        // GET: Adminusers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminuser = await _context.Adminusers
                .FirstOrDefaultAsync(m => m.AdminuserId == id);
            if (adminuser == null)
            {
                return NotFound();
            }

            return View(adminuser);
        }

        // POST: Adminusers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminuser = await _context.Adminusers.FindAsync(id);
            _context.Adminusers.Remove(adminuser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminuserExists(int id)
        {
            return _context.Adminusers.Any(e => e.AdminuserId == id);
        }
    }
}
