using System;
using System.Collections.Generic;
using System.Text;

namespace MOH.Common.Data.Entities
{
    public class Person
    {
        public int ID { get; set; }
        public string PrivateNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public int Profession { get; set; }
    }


    public enum Professions
    {

    }



}
