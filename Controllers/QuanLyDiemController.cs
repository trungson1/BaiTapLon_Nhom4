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

namespace QL_SinhVien.Controllers
{
    public class QuanLyDiemController : Controller
    {
        private readonly ApplicationDbContext _context;
        public QuanLyDiemController(ApplicationDbContext context)
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
        var model = _context.QuanLyDiem.ToList().ToPagedList (page ?? 1, pagesize);
        return View (model);
        }



        // GET: Nhanvien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.QuanLyDiem == null)
            {
                return NotFound();
            }

            var quanLyDiem = await _context.QuanLyDiem
                .FirstOrDefaultAsync(m => m.Diem == id);
            if (quanLyDiem == null)
            {
                return NotFound();
            }

            return View(quanLyDiem);
        }

        // GET: Nhanvien/Create
        public IActionResult Create()
        {
            ViewData["TenMon"]= new SelectList (_context.QuanLyMonHoc,"MaMon","TenMon");
            ViewData["TenSV"]= new SelectList (_context.QuanLySV,"MaSV","TenSV");
            return View();
        }

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
            ViewData["TenMon"]= new SelectList (_context.QuanLyMonHoc,"MaMon","TenMon");
            ViewData["TenSV"]= new SelectList (_context.QuanLySV,"MaSV","TenSV");
            return View(quanLyDiem);
        }
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
            return View(quanLyDiem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TenMon,TenSV,Diem")] QuanLyDiem quanLyDiem)
        {
            if (id != quanLyDiem.Diem)
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
                    if (!QuanLyDiemExists(quanLyDiem.Diem))
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
            return View(quanLyDiem);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.QuanLyDiem == null)
            {
                return NotFound();
            }

            var quanLyDiem = await _context.QuanLyDiem
                .FirstOrDefaultAsync(m => m.Diem == id);
            if (quanLyDiem == null)
            {
                return NotFound();
            }

            return View(quanLyDiem);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.QuanLyDiem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.QuanLyDiem'  is null.");
            }
            var quanlyDiem = await _context.QuanLyDiem.FindAsync(id);
            if (quanlyDiem != null)
            {
                _context.QuanLyDiem.Remove(quanlyDiem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuanLyDiemExists(string id)
        {
          return (_context.QuanLyDiem?.Any(e => e.Diem == id)).GetValueOrDefault();
        }
        public IActionResult Download()
        {
            var fileName = "YourFileName" + ".xlsx";
            using (ExcelPackage excelPackage =new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "TenMon";
                worksheet.Cells["B1"].Value = "TenSV";
                worksheet.Cells["C1"].Value = "Diem";
                var personList = _context.QuanLyDiem.ToList();
                worksheet.Cells["A2"].LoadFromCollection(personList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File (stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
