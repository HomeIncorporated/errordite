﻿using System.Collections.Generic;
using System.Linq;
using Errordite.Core.Interfaces;
using Errordite.Core.Configuration;
using Errordite.Core.Domain.Error;
using Errordite.Core.Domain.Organisation;
using Errordite.Core.Indexing;
using Errordite.Core.Session;
using Raven.Client;
using Raven.Client.Linq;

namespace Errordite.Core.Issues.Queries
{
    public class GetAllApplicationIssuesQuery : SessionAccessBase, IGetAllApplicationIssuesQuery
    {
        private readonly ErrorditeConfiguration _configuration;

        public GetAllApplicationIssuesQuery(ErrorditeConfiguration configuration)
        {
            _configuration = configuration;
        }

        public GetAllApplicationIssuesResponse Invoke(GetAllApplicationIssuesRequest request)
        {
            Trace("Starting...");

            RavenQueryStatistics stats;

            var issues = Session.Raven.Query<IssueDocument, Indexing.Issues>().Statistics(out stats)
                .Where(i => i.ApplicationId == Application.GetId(request.ApplicationId))
                .Take(_configuration.MaxPageSize)
                .As<Issue>()
                .OrderBy(i => i.FriendlyId)
                .Select(issue => new IssueBase
                    {
                        Id = issue.Id,
                        Rules = issue.Rules,
                        LastRuleAdjustmentUtc = issue.LastRuleAdjustmentUtc,
                        ApplicationId = issue.ApplicationId
                    })
                .ToList();
            
            while(stats.TotalResults > issues.Count)
            {
                issues.AddRange(Session.Raven.Query<IssueDocument, Indexing.Issues>().Statistics(out stats)
                    .Where(issue => issue.ApplicationId == Application.GetId(request.ApplicationId))
                    .Skip(issues.Count)
                    .Take(_configuration.MaxPageSize)
                    .As<Issue>()
                    .OrderBy(i => i.FriendlyId)
                    .Select(issue => new IssueBase
                    {
                        Id = issue.Id,
                        Rules = issue.Rules,
                        LastRuleAdjustmentUtc = issue.LastRuleAdjustmentUtc,
                        ApplicationId = issue.ApplicationId
                    }));
            }

            Trace("Found a total of {0} issues for application Id:={1}", issues.Count, request.ApplicationId);

            return new GetAllApplicationIssuesResponse
            {
                Issues = issues
            };
        }
    }

    public interface IGetAllApplicationIssuesQuery : IQuery<GetAllApplicationIssuesRequest, GetAllApplicationIssuesResponse>
    { }

    public class GetAllApplicationIssuesResponse
    {
        public List<IssueBase> Issues { get; set; }
    }

    public class GetAllApplicationIssuesRequest
    {
        public string ApplicationId { get; set; }
    }
}
