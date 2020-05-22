using MOH.Common.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MOH.Common.Data.PersonModels
{
    public class SearchPersonModel
    {
        [DisplayName("პირადობა")]
        public string PrivateNo { get; set; }

        [DisplayName("სახელი")]
        public string FirstName { get; set; }

        [DisplayName("გვარი")]
        public string LastName { get; set; }

        [DisplayName("ასაკი -დან")]
        public int? AgeMin { get; set; }

        [DisplayName("ასაკი -მდე")]
        public int? AgeMax { get; set; }

        [DisplayName("ტელეფონი")]
        public string Phone { get; set; }

        [DisplayName("პროფესია")]
        public Profession? Profession { get; set; }

        [DisplayName("რეგისტრაცია -დან")]
        public DateTime? RegDateFrom { get; set; }

        [DisplayName("რეგისტრაცია -მდე")]
        public DateTime? RegDateTo { get; set; }

        [DisplayName("გაუქმება -დან")]
        public DateTime? RemoveDateFrom { get; set; }

        [DisplayName("გაუქმება -მდე")]
        public DateTime? RemoveDateTo { get; set; }
    }
}
