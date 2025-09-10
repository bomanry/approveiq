using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using NPOI.SS.Formula.Functions;
using Quartz;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Persistence;

namespace BLH.ApproveIQ.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class SyncUsersWithEntraJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    //private readonly GraphServiceClient _graphServiceClient;
    private readonly IMapper _mapper;

    public SyncUsersWithEntraJob(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        //_graphServiceClient = graphServiceClient;
        _mapper = mapper;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        // var allUsersFromGraph = await LoadUserDataFromGraph(_graphServiceClient);
        //
        // var b2cUserList = _dbContext.Set<B2CUser>().ToList();
        //
        // for (var i = b2cUserList.Count - 1; i >= 0; i--)
        // {
        //     var b2cUser = b2cUserList[i];
        //
        //     var userFromGraph = allUsersFromGraph.SingleOrDefault(x => x.Id == b2cUser.Id.ToString());
        //
        //     if (userFromGraph == null)
        //         b2cUserList.RemoveAt(i);
        //     else
        //     {
        //         _mapper.Map(userFromGraph, b2cUser);
        //
        //         if (userFromGraph.Identities != null)
        //         {
        //             var emailAddressIdentity =
        //                 userFromGraph.Identities.SingleOrDefault(m => m.SignInType == "emailAddress");
        //
        //             if (emailAddressIdentity != null)
        //             {
        //                 b2cUser.UserPrincipalName = emailAddressIdentity.IssuerAssignedId;
        //             }
        //         }
        //
        //         allUsersFromGraph.Remove(userFromGraph);
        //     }
        // }
        //
        // b2cUserList.AddRange(allUsersFromGraph.Select(m => _mapper.Map<B2CUser>(m)));
        //
        // SyncUsers(_dbContext, b2cUserList);
    }
    
    private class B2CComparer : IEqualityComparer<B2CUser>
    {
        public bool Equals(B2CUser x, B2CUser y)
        {
            ArgumentNullException.ThrowIfNull(x);
            ArgumentNullException.ThrowIfNull(y);
            return x.Id == y.Id;
        }

        public int GetHashCode(B2CUser obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            ArgumentNullException.ThrowIfNull(obj.Id);
            return obj.Id.GetHashCode();
        }
    }
}
