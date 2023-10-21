using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QL_SinhVien.Data;
using QL_SinhVien.Models;

namespace QL_SinhVien.Controllers
{
    public class QuanLyMonHocController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanLyMonHocController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
              return _context.QuanLyMonHoc != null ? 
                          View(await _context.QuanLyMonHoc.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.QuanLyMonHoc'  is null.");
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.QuanLyMonHoc == null)
            {
                return NotFound();
            }

            var QuanLyMonHoc = await _context.QuanLyMonHoc
                .FirstOrDefaultAsync(m => m.MaMon == id);
            if (QuanLyMonHoc == null)
            {
                return NotFound();
            }

            return View(QuanLyMonHoc);
        }

        // GET: Khoa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Khoa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaMon,TenMon")] QuanLyMonHoc quanLyMonHoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quanLyMonHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quanLyMonHoc);
        }

        // GET: Khoa/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.QuanLyMonHoc == null)
            {
                return NotFound();
            }

            var quanLyMonHoc = await _context.QuanLyMonHoc.FindAsync(id);
            if (quanLyMonHoc == null)
            {
                return NotFound();
            }
            return View(quanLyMonHoc);
        }

        // POST: Khoa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaMon,TenMon")] QuanLyMonHoc quanLyMonHoc)
        {
            if (id != quanLyMonHoc.MaMon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quanLyMonHoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (KhoaExists(quanLyMonHoc.MaMon))
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
            return View(quanLyMonHoc);
        }

        // GET: Khoa/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.QuanLyMonHoc == null)
            {
                return NotFound();
            }

            var quanLyMonHoc = await _context.QuanLyMonHoc
                .FirstOrDefaultAsync(m => m.MaMon == id);
            if (quanLyMonHoc == null)
            {
                return NotFound();
            }

            return View(quanLyMonHoc);
        }

        // POST: Khoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.QuanLyMonHoc == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Khoa'  is null.");
            }
            var quanLyMonHoc = await _context.QuanLyMonHoc.FindAsync(id);
            if (quanLyMonHoc != null)
            {
                _context.QuanLyMonHoc.Remove(quanLyMonHoc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhoaExists(string id)
        {
          return (_context.QuanLyMonHoc?.Any(e => e.MaMon == id)).GetValueOrDefault();
        }
    }
}