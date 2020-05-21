using MOH.Common.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOH.Common.Data.PersonModels
{
    public class PersonModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ პირადობა")]
        [DisplayName("პირადობა")]
        [MaxLength(9)]
        public string PrivateNo { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ სახელი")]
        [DisplayName("სახელი")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ გვარი")]
        [DisplayName("გვარი")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ თარიღი")]
        [DisplayName("დაბ. თარიღი")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ ტელეფონი")]
        [DisplayName("ტელეფონი")]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "გთხოვთ მიუთითოთ პროფესია")]
        [DisplayName("პროფესია")]
        public Profession Profession { get; set; }


        [DisplayName("რეგ. თარიღი")]
        public DateTime RegistrationDate { get; set; }

        [DisplayName("გაუქმების თარიღი")]
        public DateTime? RemoveDate { get; set; }

        [DisplayName("ასაკი")]
        public int Age { get; set; }
    }
}
