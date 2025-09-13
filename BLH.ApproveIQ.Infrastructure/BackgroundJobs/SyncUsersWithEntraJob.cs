using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Quartz;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Persistence;
using User = BLH.ApproveIQ.Domain.Entities.User;

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
        // var userList = _dbContext.Set<User>().ToList();
        //
        // for (var i = userList.Count - 1; i >= 0; i--)
        // {
        //     var user = userList[i];
        //
        //     var userFromGraph = allUsersFromGraph.SingleOrDefault(x => x.Id == user.Id.ToString());
        //
        //     if (userFromGraph == null)
        //         userList.RemoveAt(i);
        //     else
        //     {
        //         _mapper.Map(userFromGraph, user);
        //
        //         if (userFromGraph.Identities != null)
        //         {
        //             var emailAddressIdentity =
        //                 userFromGraph.Identities.SingleOrDefault(m => m.SignInType == "emailAddress");
        //
        //             if (emailAddressIdentity != null)
        //             {
        //                 user.UserPrincipalName = emailAddressIdentity.IssuerAssignedId;
        //             }
        //         }
        //
        //         allUsersFromGraph.Remove(userFromGraph);
        //     }
        // }
        //
        // userList.AddRange(allUsersFromGraph.Select(m => _mapper.Map<User>(m)));
        //
        // SyncUsers(_dbContext, userList);
    }

    private class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            ArgumentNullException.ThrowIfNull(x);
            ArgumentNullException.ThrowIfNull(y);
            return x.Id == y.Id;
        }

        public int GetHashCode(User obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            ArgumentNullException.ThrowIfNull(obj.Id);
            return obj.Id.GetHashCode();
        }
    }
}
