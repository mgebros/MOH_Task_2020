using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOH.Common.Data.Entities
{
    public class Person
    {
        public int ID { get; set; }

        [MaxLength(11)]
        public string PrivateNo { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public Profession Profession { get; set; }



        public DateTime RegistrationDate { get; set; }
        public DateTime? RemoveDate { get; set; }
        public bool IsActive { get; set; }
    }


    public enum Profession
    {
        [Display(Name = "ცარიელი")]
        None,

        [Display(Name = "სტუდენტი")]
        Student,

        [Display(Name = "დეველოპერი")]
        Developer,

        [Display(Name = "მშენებელი")]
        Builder,

        [Display(Name = "ეკონომისტი")]
        Economist,

        [Display(Name = "ადვოკატი")]
        Lawyer
    }



}
