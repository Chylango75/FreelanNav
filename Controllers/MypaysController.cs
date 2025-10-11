using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcFreelan.Models.Freelan;
using MvcFreelan.Models.Mypays;

namespace MvcFreelan.Controllers
{
    public class MypaysController : Controller
    {
        private readonly AppDbContext _context;

        public MypaysController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Mypays
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mypays.ToListAsync());
        }

        // GET: Mypays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mypay = await _context.Mypays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mypay == null)
            {
                return NotFound();
            }

            return View(mypay);
        }

        // GET: Mypays/Create
        public async Task<IActionResult> Create()
        {
            var paytypes = await _context.MypayTypes
                                    .Select(p => new SelectListItem
                                    {
                                        Value = p.Id.ToString(),
                                        Text = p.MypayName
                                    })
                                    .ToListAsync();
         
            var modelo = new Mypay();
            modelo.Items = paytypes;
            modelo.AspUser = "123";
            
            return View(modelo);
        }

        // POST: Mypays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SelectedMypaytypeId,TotalMypay,DateCovered,DateAdded,Note,AspUser,Active")] Mypay mypay)
        {
                
            if (ModelState.IsValid)
            {
                _context.Add(mypay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var paytypes = await _context.MypayTypes
                                   .Select(p => new SelectListItem
                                   {
                                       Value = p.Id.ToString(),
                                       Text = p.MypayName
                                   })
                                   .ToListAsync();
            mypay.Items = paytypes;

            return View(mypay);
        }

        // GET: Mypays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mypay = await _context.Mypays.FindAsync(id);
            if (mypay == null)
            {
                return NotFound();
            }
            return View(mypay);
        }

        // POST: Mypays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SelectedMypaytypeId,TotalMypay,DateCovered,DateAdded,Note,AspUser,Active")] Mypay mypay)
        {
            if (id != mypay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mypay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MypayExists(mypay.Id))
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
            return View(mypay);
        }

        // GET: Mypays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mypay = await _context.Mypays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mypay == null)
            {
                return NotFound();
            }

            return View(mypay);
        }

        // POST: Mypays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mypay = await _context.Mypays.FindAsync(id);
            if (mypay != null)
            {
                _context.Mypays.Remove(mypay);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MypayExists(int id)
        {
            return _context.Mypays.Any(e => e.Id == id);
        }
    }
}
