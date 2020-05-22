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
                RegistrationDate = DateTime.Now,
                IsActive = true
            };

            _context.Add(p);
            return _context.SaveChangesAsync();

        }



        public IEnumerable<PersonModel> GetPeople(SearchPersonModel spm)
        {
            var filter = _context.People.Where(p => p != null);

            if(!string.IsNullOrEmpty(spm.PrivateNo)) filter = filter.Where(p => p.PrivateNo == spm.PrivateNo); 
            if(!string.IsNullOrEmpty(spm.FirstName)) filter = filter.Where(p => p.FirstName == spm.FirstName); 
            if(!string.IsNullOrEmpty(spm.LastName)) filter = filter.Where(p => p.LastName == spm.LastName); 

            if(spm.AgeMin != null) filter = filter.Where(p => (DateTime.Now - p.BirthDate).Days / 365 >= spm.AgeMin); 
            if(spm.AgeMax != null) filter = filter.Where(p => (DateTime.Now - p.BirthDate).Days / 365 <= spm.AgeMax); 

            if(spm.RegDateFrom != null) filter = filter.Where(p => p.RegistrationDate > spm.RegDateFrom); 
            if(spm.RegDateTo != null) filter = filter.Where(p => p.RegistrationDate < spm.RegDateTo); 

            if(spm.RemoveDateFrom != null) filter = filter.Where(p => p.RemoveDate > spm.RemoveDateFrom); 
            if(spm.RemoveDateTo != null) filter = filter.Where(p => p.RemoveDate < spm.RemoveDateTo);

            var people = filter.ToList();

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
                Age = (DateTime.Now - pm.BirthDate).Days / 365,
                IsActive = pm.IsActive
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
                Age = (DateTime.Now - person.BirthDate).Days / 365,
                IsActive = person.IsActive
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
            p.IsActive = true;

            _context.Update(p);
            _context.SaveChanges();
        }



        public void Remove(int id)
        {
            var p = _context.People.FirstOrDefault(pp => pp.ID == id);

            p.IsActive = false;
            p.RemoveDate = DateTime.Now;

            _context.Update(p);
            _context.SaveChanges();

        }



        public bool PersonExists(int id)
        {
            return _context.People.Any(e => e.ID == id);
        }
    }
}
