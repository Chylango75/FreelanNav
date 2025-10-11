using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcFreelan.Models;
using MvcFreelan.Models.Freelan;
using MvcFreelan.Models.Mypays;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFreelan.Controllers
{
    public class MypayTypesController : Controller
    {
        private readonly AppDbContext _context;

        public MypayTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MypayTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MypayTypes.ToListAsync());
        }

        // GET: MypayTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mypayType = await _context.MypayTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mypayType == null)
            {
                return NotFound();
            }

            return View(mypayType);
        }

        // GET: MypayTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MypayTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MypayName,Created,Active")] MypayType mypayType)
        {
            try
            {
                if (mypayType.MypayName != null)
                {
                    mypayType.Created = DateTime.Now.ToString();
                    mypayType.Id = 0;
                    mypayType.Active = true;
                    ModelState.Clear();

                    if (ModelState.IsValid)
                    {
                        _context.Add(mypayType);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(mypayType);
            }
            catch(Exception ex)
            {
                var error = new ErrorViewModel();
                error.RequestId = ex.Message;
                return RedirectToAction("Index", "Admin", new { statusCode = 0, msg = ex.InnerException });
            }
        }

        // GET: MypayTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mypayType = await _context.MypayTypes.FindAsync(id);
            if (mypayType == null)
            {
                return NotFound();
            }
            return View(mypayType);
        }

        // POST: MypayTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MypayName,Created,Active")] MypayType mypayType)
        {
            try
            {
                if (id != mypayType.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mypayType);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MypayTypeExists(mypayType.Id))
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
                return View(mypayType);

            }
            catch (Exception ex)
            {
                var error = new ErrorViewModel();
                error.RequestId = ex.Message;
                return RedirectToAction("Index", "Admin", new { statusCode = 0, msg = ex.InnerException });
            }
        }

        // GET: MypayTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mypayType = await _context.MypayTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mypayType == null)
            {
                return NotFound();
            }

            return View(mypayType);
        }

        // POST: MypayTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mypayType = await _context.MypayTypes.FindAsync(id);
            if (mypayType != null)
            {
                _context.MypayTypes.Remove(mypayType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MypayTypeExists(int id)
        {
            return _context.MypayTypes.Any(e => e.Id == id);
        }
    }
}
