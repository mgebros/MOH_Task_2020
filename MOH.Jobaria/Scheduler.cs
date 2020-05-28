using MOH.Common.IServices;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace MOH.Jobaria
{
    public class Scheduler
    {
        public bool IsSchedulerRunning { get; private set; } = true;



        public async Task RunScheduler()
        {
            try
            {
                IsSchedulerRunning = true;

                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();

                // start 
                await scheduler.Start();



                //      JOBS
                IJobDetail job = JobBuilder.Create<DupJob>()
                    .WithIdentity(new JobKey(name: "jobDup", group: "jobDupGroup"))
                    .Build();


                //      TRIGGERS
                ITrigger trig = TriggerBuilder.Create()
                    .WithIdentity(new TriggerKey("trigDup", "trigDupGroup"))
                    .WithCronSchedule(Startup._cronExpression)
                    .ForJob(job)
                    .Build();



                //      SCHEDULE JOBS
                await scheduler.ScheduleJob(job, trig);

            }
            catch (Exception ex)
            {
                IsSchedulerRunning = false;
            }
        }
    }
}
