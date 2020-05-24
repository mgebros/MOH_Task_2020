using MOH.Common.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOH.Jobaria
{
    public class ConnectiorDI
    {
        private readonly IPeopleService _ps;



        public ConnectiorDI(IPeopleService ps)
        {
            _ps = ps;
        }



        public void CallDI()
        {
            _ps.RemoveDuplicates();
        }

    }
}
