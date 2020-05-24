using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
            var people = _context.People.Where(p =>
            string.IsNullOrEmpty(spm.PrivateNo) || p.PrivateNo == spm.PrivateNo &&
            string.IsNullOrEmpty(spm.FirstName) || p.FirstName == spm.FirstName &&
            string.IsNullOrEmpty(spm.LastName) || p.LastName == spm.LastName &&
            string.IsNullOrEmpty(spm.Phone) || p.Phone == spm.Phone &&

            !spm.Profession.HasValue || p.Profession == spm.Profession &&

            !spm.AgeMin.HasValue || (DateTime.Now - p.BirthDate).Days / 365 >= spm.AgeMin &&
            !spm.AgeMax.HasValue || (DateTime.Now - p.BirthDate).Days / 365 <= spm.AgeMax &&

            !spm.RegDateFrom.HasValue || p.RegistrationDate > spm.RegDateFrom &&
            !spm.RegDateTo.HasValue || p.RegistrationDate < spm.RegDateTo &&

            !spm.RemoveDateFrom.HasValue || p.RemoveDate > spm.RemoveDateFrom &&
            !spm.RemoveDateTo.HasValue || p.RemoveDate < spm.RemoveDateTo
            ).ToList();



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



        public void Deactivate(int id)
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



        public void RemoveDuplicates()
        {
            //  SQl except Age checkign
            /*
select PrivateNo from (
select COUNT(PrivateNo) as Quantity, PrivateNo from People
Where IsActive = 1
Group By PrivateNo) as cxrili
Where Quantity > 1
*/
            //  Taking similar Private Numbers
            var privateNos = _context.People
                .Where(p => p.IsActive == true && ((DateTime.Now - p.BirthDate).Days / 365) > 30)
                .GroupBy(p => p.PrivateNo)
                .Select(p => new { PrivateNo = p.Key, Quantity = p.Count() })
                .Where(p => p.Quantity > 1)
                .Select(p => p.PrivateNo);



            //  Taking every second and consequent ID by PrivateNos 
            List<int> ids = new List<int>();
            foreach(var no in privateNos)
            {
                ids.AddRange(
                    _context.People
                    .Where(p => p.PrivateNo == no && p.IsActive == true && ((DateTime.Now - p.BirthDate).Days / 365) > 30)
                    .Skip(1)
                    .Select(p => p.ID)
                    .ToList()
                    );
            }

            foreach(var id in ids)
                Deactivate(id);

            
        }

    }
}
