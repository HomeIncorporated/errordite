﻿using System.Collections.Generic;
using Castle.Core;
using CodeTrip.Core.Caching.Entities;
using CodeTrip.Core.Caching.Interceptors;
using CodeTrip.Core.Caching.Interfaces;
using CodeTrip.Core.Extensions;
using CodeTrip.Core.Interfaces;
using CodeTrip.Core.Session;
using Errordite.Core.Authorisation;
using Errordite.Core.Caching;
using Errordite.Core.Domain.Organisation;
using Errordite.Core.Indexing;
using Errordite.Core.Organisations;
using Raven.Abstractions.Data;

namespace Errordite.Core.Users.Commands
{
    [Interceptor(CacheInvalidationInterceptor.IoCName)]
    public class DeleteUserCommand : SessionAccessBase, IDeleteUserCommand
    {
        private readonly IAuthorisationManager _authorisationManager;

        public DeleteUserCommand(IAuthorisationManager authorisationManager)
        {
            _authorisationManager = authorisationManager;
        }

        public DeleteUserResponse Invoke(DeleteUserRequest request)
        {
            Trace("Starting...");

            var userId = User.GetId(request.UserId);
            var existingUser = Load<User>(userId);

            if (existingUser == null)
            {
                return new DeleteUserResponse(true)
                {
                    Status = DeleteUserStatus.UserNotFound
                };
            }

            _authorisationManager.Authorise(existingUser, request.CurrentUser);

            Session.Raven.Advanced.DocumentStore.DatabaseCommands.UpdateByIndex(CoreConstants.IndexNames.Issues,
                new IndexQuery
                {
                    Query = "OrganisationId:{0} AND UserId:{1}".FormatWith(request.CurrentUser.OrganisationId, userId)
                },
                new[]
                {
                    new PatchRequest
                    {
                        Name = "UserId",
                        Type = PatchCommandType.Set,
                        Value = User.GetId(request.CurrentUser.Id)
                    }
                });

            Delete(existingUser);

            Session.SynchroniseIndexes<Users_Search, Issues_Search>();

            return new DeleteUserResponse(userId: request.UserId, organisationId: request.CurrentUser.OrganisationId)
            {
                Status = DeleteUserStatus.Ok
            };
        }
    }

    public interface IDeleteUserCommand : ICommand<DeleteUserRequest, DeleteUserResponse>
    { }

    public class DeleteUserResponse : CacheInvalidationResponseBase
    {
        private readonly string _userId;
        private readonly string _organisationId;
        public DeleteUserStatus Status { get; set; }

        public DeleteUserResponse(bool ignoreCache = false, string userId = "", string organisationId = "")
            : base(ignoreCache)
        {
            _userId = userId;
            _organisationId = organisationId;
        }

        protected override IEnumerable<CacheInvalidationItem> GetCacheInvalidationItems()
        {
            return CacheInvalidation.GetUserInvalidationItems(_organisationId, _userId);
        }
    }

    public class DeleteUserRequest : OrganisationRequestBase
    {
        public string UserId { get; set; }
    }

    public enum DeleteUserStatus
    {
        Ok,
        UserNotFound
    }
}
