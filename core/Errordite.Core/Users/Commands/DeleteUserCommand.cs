﻿using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using Errordite.Core.Caching.Entities;
using Errordite.Core.Caching.Interceptors;
using Errordite.Core.Domain.Master;
using Errordite.Core.Extensions;
using Errordite.Core.Interfaces;
using Errordite.Core.Authorisation;
using Errordite.Core.Caching;
using Errordite.Core.Domain.Organisation;
using Errordite.Core.Organisations;
using Errordite.Core.Session;
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

            if (userId == request.CurrentUser.Id)
            {
                return new DeleteUserResponse
                {
                    Status = DeleteUserStatus.CantDeleteCurrentUser
                };
            }

            var existingUser = Load<User>(userId);

            if (existingUser == null)
            {
                return new DeleteUserResponse(true)
                {
                    Status = DeleteUserStatus.UserNotFound
                };
            }

            _authorisationManager.Authorise(existingUser, request.CurrentUser);

            var organisation = Session.MasterRaven.Load<Organisation>(existingUser.OrganisationId);

            if (organisation.PrimaryUserId == userId)
            {
                organisation.PrimaryUserId = request.CurrentUser.Id;
            }
                
            var userOrgMapping = Session.MasterRaven.Query<UserOrganisationMapping>().FirstOrDefault(u => u.EmailAddress == existingUser.Email);

            if (userOrgMapping != null)
            {
	            userOrgMapping.Organisations.Remove(organisation.Id);

				if(userOrgMapping.Organisations.Count == 0)
					Session.MasterRaven.Delete(userOrgMapping);
            } 

			Session.RavenDatabaseCommands.UpdateByIndex(CoreConstants.IndexNames.Issues,
                new IndexQuery
                {
                    Query = "UserId:{0}".FormatWith(userId)
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

            Session.SynchroniseIndexes<Indexing.Users, Indexing.Issues>();

            return new DeleteUserResponse(organisationId: request.CurrentUser.OrganisationId, email: existingUser.Email)
            {
                Status = DeleteUserStatus.Ok
            };
        }
    }

    public interface IDeleteUserCommand : ICommand<DeleteUserRequest, DeleteUserResponse>
    { }

    public class DeleteUserResponse : CacheInvalidationResponseBase
    {
        private readonly string _email;
        private readonly string _organisationId;
        public DeleteUserStatus Status { get; set; }

        public DeleteUserResponse(bool ignoreCache = false, string organisationId = "", string email = "")
            : base(ignoreCache)
        {
            _organisationId = organisationId;
            _email = email;
        }

        protected override IEnumerable<CacheInvalidationItem> GetCacheInvalidationItems()
        {
            return CacheInvalidation.GetUserInvalidationItems(_organisationId, _email);
        }
    }

    public class DeleteUserRequest : OrganisationRequestBase
    {
        public string UserId { get; set; }
    }

    public enum DeleteUserStatus
    {
        Ok,
        UserNotFound,
        CantDeleteCurrentUser
    }
}
