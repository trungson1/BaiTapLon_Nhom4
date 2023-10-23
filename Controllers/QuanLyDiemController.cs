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
    public class QuanLyDiemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanLyDiemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
              return _context.QuanLyDiem != null ? 
                          View(await _context.QuanLyDiem.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.QuanLyDiem'  is null.");
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.QuanLyDiem == null)
            {
                return NotFound();
            }

            var quanLyDiem = await _context.QuanLyDiem
                .Include(s => s.QuanLyMonHoc)        
                .Include(s => s.QuanLySV)
                .FirstOrDefaultAsync(m => m.TenMon == id);
            if (quanLyDiem == null)
            {
                return NotFound();
            }

            return View(quanLyDiem);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            ViewData["TenMon"] = new SelectList(_context.QuanLyMonHoc, "MaMon", "TenMon");
            ViewData["TenSV"] = new SelectList(_context.QuanLySV, "MaSV", "TenSV");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenMon,TenSV,Diem")] QuanLyDiem quanLyDiem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quanLyDiem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TenMon"] = new SelectList(_context.QuanLyMonHoc, "MaMon", "TenMon", quanLyDiem.TenMon);
            ViewData["TenSV"] = new SelectList(_context.QuanLySV, "MaSV", "TenSV", quanLyDiem.TenSV);
            return View(quanLyDiem);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.QuanLyDiem == null)
            {
                return NotFound();
            }

            var quanLyDiem = await _context.QuanLyDiem.FindAsync(id);
            if (quanLyDiem == null)
            {
                return NotFound();
            }
            ViewData["TenMon"] = new SelectList(_context.QuanLyMonHoc, "MaMon", "MaMon", quanLyDiem.TenMon);
            ViewData["TenSV"] = new SelectList(_context.QuanLySV, "MaSV", "MaSV", quanLyDiem.TenSV);
            return View(quanLyDiem);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TenMon,TenSV,Diem")] QuanLyDiem quanLyDiem)
        {
            if (id != quanLyDiem.TenMon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quanLyDiem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuanLyDiemExists(quanLyDiem.TenMon))
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
            ViewData["TenMon"] = new SelectList(_context.QuanLyMonHoc, "MaMon", "TenMon", quanLyDiem.TenMon);
            ViewData["TenSV"] = new SelectList(_context.QuanLySV, "MaSV", "TenSV", quanLyDiem.TenSV);
            return View(quanLyDiem);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.QuanLyDiem == null)
            {
                return NotFound();
            }

            var quanLyDiem = await _context.QuanLyDiem
                .Include(s => s.QuanLyMonHoc)
                .Include(s => s.QuanLySV)
                .FirstOrDefaultAsync(m => m.TenMon == id);
            if (quanLyDiem == null)
            {
                return NotFound();
            }

            return View(quanLyDiem);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.QuanLyDiem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.QuanLyDiem'  is null.");
            }
            var quanLyDiem = await _context.QuanLyDiem.FindAsync(id);
            if (quanLyDiem != null)
            {
                _context.QuanLyDiem.Remove(quanLyDiem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuanLyDiemExists(string id)
        {
          return (_context.QuanLyDiem?.Any(e => e.TenMon == id)).GetValueOrDefault();
        }
    }
}