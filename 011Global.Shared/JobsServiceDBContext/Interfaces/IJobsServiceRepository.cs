using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared.JobsServiceDBContext.Interfaces;

public interface IJobsServiceRepository
{
    public Task<Dictionary<string, GlobalJob>> GetJobs(string hostName);

    public Task UpdateStartedJob(string name, bool isRunning, DateTime lastStartDate);

    public Task UpdateStopedJob(string name, bool isRunning, DateTime lastStopDate);
}

