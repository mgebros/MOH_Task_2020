using MOH.Common.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOH.Jobaria
{
    public static class ConnectiorDI
    {
        public static IPeopleService _ps;



        public static void Call()
        {
            _ps.RemoveDuplicates();
        }

    }
}
