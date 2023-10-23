using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QL_SinhVien.Data;
using OfficeOpenXml;
using QL_SinhVien.Models;
using X.PagedList;

namespace qlnv.Controllers
{
    public class QuanLySVController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanLySVController(ApplicationDbContext context)
        {
            _context = context;
        }
         public async Task<IActionResult> Index( int? page, int? PageSize )
        {
            ViewBag.PageSize = new List<SelectListItem>()
        {
            new SelectListItem() {Value="3", Text = "3"},
            new SelectListItem() {Value="5", Text = "5"},
            new SelectListItem() {Value="10", Text = "10"},
            new SelectListItem() {Value="15", Text = "15"},
            new SelectListItem() {Value="25", Text = "25"},


        };
        int pagesize = (PageSize ?? 3);
        ViewBag.psize = pagesize;
        var model = _context.QuanLySV.ToList().ToPagedList (page ?? 1, pagesize);
        return View (model);
        }



        // GET: Nhanvien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.QuanLySV == null)
            {
                return NotFound();
            }

            var quanLySV = await _context.QuanLySV
                .FirstOrDefaultAsync(m => m.MaSV == id);
            if (quanLySV == null)
            {
                return NotFound();
            }

            return View(quanLySV);
        }

        // GET: Nhanvien/Create
        public IActionResult Create()
        {
            ViewData["TenLop"] = new SelectList(_context.Set<Lop>(), "MaLop", "TenLop");
            ViewData["TenKhoa"] = new SelectList(_context.Set<Khoa>(), "MaKhoa", "TenKhoa");
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSV,TenSV,NgaySinh,SDT,TenLop,TenKhoa")] QuanLySV quanLySV)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quanLySV);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TenLop"] = new SelectList(_context.Set<Lop>(), "MaLop", "TenLop", quanLySV.TenLop);
            ViewData["TenKhoa"] = new SelectList(_context.Set<Khoa>(), "MaKhoa", "TenKhoa", quanLySV.TenKhoa);
            return View(quanLySV);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.QuanLySV == null)
            {
                return NotFound();
            }

            var quanLySV = await _context.QuanLySV.FindAsync(id);
            if (quanLySV == null)
            {
                return NotFound();
            }
            return View(quanLySV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSV,TenSV,NgaySinh,SDT,TenLop,TenKhoa")] QuanLySV quanLySV)
        {
            if (id != quanLySV.MaSV)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quanLySV);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanvienExists(quanLySV.MaSV))
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
            return View(quanLySV);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.QuanLySV == null)
            {
                return NotFound();
            }

            var quanLySV = await _context.QuanLySV
                .FirstOrDefaultAsync(m => m.MaSV == id);
            if (quanLySV == null)
            {
                return NotFound();
            }

            return View(quanLySV);
        }

        // POST: Nhanvien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.QuanLySV == null)
            {
                return Problem("Entity set 'ApplicationDbContext.QuanLySV'  is null.");
            }
            var quanLySV = await _context.QuanLySV.FindAsync(id);
            if (quanLySV != null)
            {
                _context.QuanLySV.Remove(quanLySV);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanvienExists(string id)
        {
          return (_context.QuanLySV?.Any(e => e.MaSV == id)).GetValueOrDefault();
        }
         public IActionResult Download()
        {
            var fileName = "YourFileName" + ".xlsx";
            using (ExcelPackage excelPackage =new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "MaSV";
                worksheet.Cells["B1"].Value = "TenSV";
                worksheet.Cells["C1"].Value = "NgaySinh";
                worksheet.Cells["D1"].Value = "SDT";
                worksheet.Cells["E1"].Value = "TenLop";
                worksheet.Cells["F1"].Value = "TenKhoa";
                var personList = _context.QuanLySV.ToList();
                worksheet.Cells["A2"].LoadFromCollection(personList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File (stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
