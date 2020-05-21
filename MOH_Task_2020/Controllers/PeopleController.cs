using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MOH.Common.Data;
using MOH.Common.Data.Entities;
using MOH.Common.Data.PersonModels;
using MOH.Common.IServices;
using MOH_Task_2020.Models;

namespace MOH_Task_2020.Controllers
{
    public class PeopleController : Controller
    {
        //private readonly MOHContext _context;
        private readonly IPeopleService _ps;

        //public PeopleController(MOHContext context)
        public PeopleController(IPeopleService ps)
        {
            _ps = ps;
        }

        //GET: People
        public async Task<IActionResult> Index()
        {
            return View(_ps.GetActivePeople());
        }

        // GET: People/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var person = await _context.People
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (person == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(person);
        //}

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrivateNo,FirstName,LastName,BirthDate,Phone,Profession")] PersonModel person)
        {
            if (ModelState.IsValid)
            {
                await _ps.Create(person);               

                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }



        //GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("GeneralError", new GeneralErrorViewModel("მიუთიტეთ იდენტიფიკატორი"));
            }

            var person = _ps.GetPerson((int)id);

            if (person == null)
            {
                return View("GeneralError", new GeneralErrorViewModel("პიროვნება ვერ მოიძებნა"));
            }

            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PrivateNo,FirstName,LastName,BirthDate,Phone,Profession")] PersonModel person)
        {
            if (id != person.ID)
            {
                return View("GeneralError", new GeneralErrorViewModel("არასწორი იდენტიფიკატორი"));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ps.Edit(person);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_ps.PersonExists(person.ID))
                    {
                        return View("GeneralError", new GeneralErrorViewModel("პიროვნება ვერ მოიძებნა"));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var person = await _context.People
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (person == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(person);
        //}

        // POST: People/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var person = await _context.People.FindAsync(id);
        //    _context.People.Remove(person);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

       
    }
}
