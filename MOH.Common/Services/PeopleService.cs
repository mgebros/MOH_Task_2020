using Microsoft.EntityFrameworkCore;
using MOH.Common.Data;
using MOH.Common.Data.Entities;
using MOH.Common.Data.PersonModels;
using MOH.Common.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MOH.Common.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly MOHContext _context;



        public PeopleService(MOHContext context)
        {
            _context = context;
        }



        public Task Create(PersonModel pm)
        {
            Person p = new Person()
            {
                BirthDate = pm.BirthDate,
                FirstName = pm.FirstName,
                LastName = pm.LastName,
                Phone = pm.Phone,
                PrivateNo = pm.PrivateNo,
                Profession = pm.Profession,
                RegistrationDate = DateTime.Now
            };

            _context.Add(p);
            return _context.SaveChangesAsync();

        }



        public IEnumerable<PersonModel> GetActivePeople()
        {
            var people = _context.People.ToList();

            List<PersonModel> pms = new List<PersonModel>(people.Select(pm => new PersonModel()
            {
                BirthDate = pm.BirthDate,
                FirstName = pm.FirstName,
                ID = pm.ID,
                LastName = pm.LastName,
                Phone = pm.Phone,
                PrivateNo = pm.PrivateNo,
                Profession = pm.Profession,
                RegistrationDate = pm.RegistrationDate,
                RemoveDate = pm.RemoveDate,
                Age = (DateTime.Now - pm.BirthDate).Days / 365
            }));

            return pms;
        }



        public PersonModel GetPerson(int id)
        {
            var person = _context.People.Find(id);

            if (person == null)
            {
                return null;
            }

            var pm = new PersonModel()
            {
                BirthDate = person.BirthDate,
                FirstName = person.FirstName,
                ID = person.ID,
                LastName = person.LastName,
                Phone = person.Phone,
                PrivateNo = person.PrivateNo,
                Profession = person.Profession,
                RegistrationDate = person.RegistrationDate,
                RemoveDate = person.RemoveDate,
                Age = (DateTime.Now - person.BirthDate).Days / 365
            };

            return pm;
        }



        public void Edit(PersonModel person)
        {
            var p = _context.People.Find(person.ID);

            p.BirthDate = person.BirthDate;
            p.FirstName = person.FirstName;
            p.LastName = person.LastName;
            p.Phone = person.Phone;
            p.PrivateNo = person.PrivateNo;
            p.Profession = person.Profession;

            _context.Update(p);
            _context.SaveChanges();

        }



        public bool PersonExists(int id)
        {
            return _context.People.Any(e => e.ID == id);
        }
    }
}
