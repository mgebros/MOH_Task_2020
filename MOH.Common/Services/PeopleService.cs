using MOH.Common.Data;
using MOH.Common.Data.Entities;
using MOH.Common.Data.PersonModels;
using MOH.Common.IServices;
using System;
using System.Collections.Generic;
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
    }
}
