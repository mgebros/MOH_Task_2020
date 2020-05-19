using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOH.Common.Data.Entities
{
    public class Person
    {
        public int ID { get; set; }

        [MaxLength(9)]
        public string PrivateNo { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }


        public DateTime BirthDate { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public Profession Profession { get; set; }



        public int Age { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? RemoveDate { get; set; }
    }


    public enum Profession
    {
        Student,
        Developer,
        Builder,
        Economist,
        Lawyer
    }



}
