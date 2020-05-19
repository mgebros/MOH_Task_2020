using MOH.Common.Data.PersonModels;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MOH.Common.IServices
{
    public interface IPeopleService
    {
        Task Create(PersonModel pm);
    }
}
