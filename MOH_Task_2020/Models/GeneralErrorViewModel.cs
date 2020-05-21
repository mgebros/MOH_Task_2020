using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOH_Task_2020.Models
{
    public class GeneralErrorViewModel
    {
        public string Text { get; set; }


        public GeneralErrorViewModel(string text)
        {
            Text = text;
        }
    }
}
