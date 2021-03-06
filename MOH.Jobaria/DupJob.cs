﻿using MOH.Common.IServices;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOH.Jobaria
{
    public class DupJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                ConnectiorDI.Call();
                Console.WriteLine("Duplicates removed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Environment.Exit(1);
            }
        }
    }
}
