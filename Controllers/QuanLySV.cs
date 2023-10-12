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
    public class QuanLySVController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanLySVController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.QuanLySV.Include(s => s.Lop).Include(s => s.Khoa);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.QuanLySV == null)
            {
                return NotFound();
            }

            var ql = await _context.QuanLySV
                .Include(s => s.Lop)        
                .Include(s => s.Khoa)
                .FirstOrDefaultAsync(m => m.MaSV == id);
            if (ql == null)
            {
                return NotFound();
            }

            return View(ql);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            ViewData["TenLop"] = new SelectList(_context.Lop, "MaLop", "MaLop");
            ViewData["TenKhoa"] = new SelectList(_context.Khoa, "MaKhoa", "MaKhoa");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSV,TenSV,NgaySinh,SDT,TenLop,TenKhoa")] QuanLySV ql)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ql);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TenLop"] = new SelectList(_context.Lop, "MaLop", "MaLop", ql.TenLop);
            ViewData["TenKhoa"] = new SelectList(_context.Khoa, "MaKhoa", "MaKhoa", ql.TenKhoa);
            return View(ql);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.QuanLySV == null)
            {
                return NotFound();
            }

            var ql = await _context.QuanLySV.FindAsync(id);
            if (ql == null)
            {
                return NotFound();
            }
            ViewData["TenLop"] = new SelectList(_context.Lop, "MaLop", "MaLop", ql.TenLop);
            ViewData["TenKhoa"] = new SelectList(_context.Khoa, "MaKhoa", "MaKhoa", ql.TenKhoa);
            return View(ql);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSV,TenSV,NgaySinh,SDT,TenLop,TenKhoa")] QuanLySV ql)
        {
            if (id != ql.MaSV)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ql);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuanLySVExists(ql.MaSV))
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
            ViewData["TenLop"] = new SelectList(_context.Lop, "MaLop", "MaLop", ql.TenLop);
            ViewData["TenKhoa"] = new SelectList(_context.Khoa, "MaKhoa", "MaKhoa", ql.TenKhoa);
            return View(ql);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.QuanLySV == null)
            {
                return NotFound();
            }

            var ql = await _context.QuanLySV
                .Include(s => s.Lop)
                .Include(s => s.Khoa)
                .FirstOrDefaultAsync(m => m.MaSV == id);
            if (ql == null)
            {
                return NotFound();
            }

            return View(ql);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.QuanLySV == null)
            {
                return Problem("Entity set 'ApplicationDbContext.QuanLySV'  is null.");
            }
            var ql = await _context.QuanLySV.FindAsync(id);
            if (ql != null)
            {
                _context.QuanLySV.Remove(ql);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuanLySVExists(string id)
        {
          return (_context.QuanLySV?.Any(e => e.MaSV == id)).GetValueOrDefault();
        }
    }
}