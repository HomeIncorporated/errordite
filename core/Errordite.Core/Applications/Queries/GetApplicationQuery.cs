﻿using System.Collections.Generic;
using Castle.Core;
using CodeTrip.Core.Caching.Entities;
using CodeTrip.Core.Caching.Interceptors;
using CodeTrip.Core.Interfaces;
using Errordite.Core.Authorisation;
using Errordite.Core.Caching;
using Errordite.Core.Domain.Organisation;
using Errordite.Core.Organisations;
using ProtoBuf;
using SessionAccessBase = Errordite.Core.Session.SessionAccessBase;

namespace Errordite.Core.Applications.Queries
{
    [Interceptor(CacheInterceptor.IoCName)]
    public class GetApplicationQuery : SessionAccessBase, IGetApplicationQuery
    {
        private readonly IAuthorisationManager _authorisationManager;

        public GetApplicationQuery(IAuthorisationManager authorisationManager)
        {
            _authorisationManager = authorisationManager;
        }

        public GetApplicationResponse Invoke(GetApplicationRequest request)
        {
            Trace("Starting...");

            string applicationId = Application.GetId(request.Id);

            var application = Load<Application>(applicationId);

            if(application != null)
            {
                _authorisationManager.Authorise(application, request.CurrentUser);

                if (application.Notifications == null)
                    application.Notifications = new List<Notification>();
            }

            return new GetApplicationResponse
            {
                Application = application
            };
        }
    }

    public interface IGetApplicationQuery : IQuery<GetApplicationRequest, GetApplicationResponse>
    { }

    [ProtoContract]
    public class GetApplicationResponse
    {
        [ProtoMember(1)]
        public Application Application { get; set; }
    }

    public class GetApplicationRequest : CacheableOrganisationRequestBase<GetApplicationResponse>
    {
        public string Id { get; set; }
        public string OrganisationId { get; set; }

        protected override string GetCacheKey()
        {
            return CacheKeys.Applications.Key(OrganisationId, Id);
        }

        protected override CacheProfiles GetCacheProfile()
        {
            return CacheProfiles.Applications;
        }
    }
}
