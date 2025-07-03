using _011Global.Shared.JobsServiceDBContext.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared.JobsServiceDBContext.Repos;

public class JobsServiceRepository : IJobsServiceRepository
{
    private readonly JobsServiceContext _context;
    public JobsServiceRepository(JobsServiceContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<string, GlobalJob>> GetJobs(string hostName)
    {
        return await _context.GlobalJobs.Where(g => g.MachineNameList.Contains(hostName)).ToDictionaryAsync(gk => gk.TypeName, gv => gv);
    }

    public async Task UpdateStartedJob(string typeName, bool isRunning, DateTime lastStartDate)
    {
        await _context.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
        {
            using (var trans = await _context.Database.BeginTransactionAsync())
                try
                {
                    var existingJob = await _context.GlobalJobs.SingleOrDefaultAsync(g => g.TypeName == typeName) ?? 
                                      throw new NullReferenceException($"There is no job with the name {typeName}");
                    
                    existingJob.IsRunning = isRunning;
                    existingJob.LastStartDate = lastStartDate;

                    await _context.SaveChangesAsync();
                    await trans.CommitAsync();
                }
                catch
                {
                    await trans.RollbackAsync();
                    throw;
                }
        });
    }

    public async Task UpdateStopedJob(string typeName, bool isRunning, DateTime lastStopDate)
    {
        await _context.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
        {
            using (var trans = await _context.Database.BeginTransactionAsync())
                try
                {
                    var existingJob = await _context.GlobalJobs.SingleOrDefaultAsync(g => g.TypeName == typeName) ?? 
                                      throw new NullReferenceException($"There is no job with the name {typeName}");

                    existingJob.IsRunning = isRunning;
                    existingJob.LastStopDate = lastStopDate;

                    await _context.SaveChangesAsync();
                    await trans.CommitAsync();
                }
                catch
                {
                    await trans.RollbackAsync();
                    throw;
                }
        });
    }
}
