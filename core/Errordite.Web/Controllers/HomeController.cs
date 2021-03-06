﻿using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Mvc;
using Errordite.Core;
using Errordite.Core.Caching.Interfaces;
using Errordite.Core.Caching.Resources;
using Errordite.Core.Domain.Error;
using Errordite.Core.Domain.Master;
using Errordite.Core.Domain.Organisation;
using Errordite.Core.Indexing;
using Errordite.Core.IoC;
using Errordite.Core.Configuration;
using Errordite.Core.Identity;
using Errordite.Core.Notifications.EmailInfo;
using Errordite.Core.Raven;
using Errordite.Core.Session;
using Errordite.Core.Session.Actions;
using Errordite.Web.ActionFilters;
using Errordite.Web.Models.Home;
using Errordite.Core.Extensions;
using Errordite.Web.Extensions;
using Errordite.Web.Models.Navigation;
using Raven.Client.Indexes;

namespace Errordite.Web.Controllers
{
    public class HomeController : ErrorditeController
    {
		private readonly ErrorditeConfiguration _configuration;
		private readonly IAppSession _session;
		private readonly IShardedRavenDocumentStoreFactory _storeFactory;

        public HomeController(ErrorditeConfiguration configuration, 
			IShardedRavenDocumentStoreFactory storeFactory, 
			IAppSession session)
        {
	        _configuration = configuration;
	        _storeFactory = storeFactory;
	        _session = session;
        }

		[HttpGet, ExportViewData]
		public ActionResult SyncIndexes(string orgId)
		{
			Server.ScriptTimeout = 7200; //timeout in 2 hours

			var masterDocumentStore = _storeFactory.Create(RavenInstance.Master());

			if (orgId.IsNullOrEmpty())
			{
				Trace("Syncing Errordite Indexes");
				IndexCreation.CreateIndexes(new CompositionContainer(
					new AssemblyCatalog(typeof(Issues).Assembly), new ExportProvider[0]),
					masterDocumentStore.DatabaseCommands.ForDatabase(CoreConstants.ErrorditeMasterDatabaseName),
					masterDocumentStore.Conventions);
				Trace("Done Syncing Errordite Indexes");
			}

			foreach (var organisation in Core.Session.MasterRaven.Query<Organisation>().GetAllItemsAsList(100))
			{
				if (orgId.IsNotNullOrEmpty() && organisation.FriendlyId == orgId)
					continue;

				organisation.RavenInstance = Core.Session.MasterRaven.Load<RavenInstance>(organisation.RavenInstanceId);

				using (_session.SwitchOrg(organisation))
				{
					Trace("Syncing {0} Indexes", organisation.Name);
					_session.BootstrapOrganisation(organisation);
					Trace("Done Syncing {0} Indexes", organisation.Name);
				}
			}

			ConfirmationNotification("All indexes for all organisations have been updated");
			return RedirectToAction("index");
		}

        public ActionResult ClearCache()
        {
            ObjectFactory.GetObject<ICacheEngine>(CacheEngines.Memory).Clear();
            return Content("OK");
        }

        [ImportViewData]
        public ActionResult Index()
        {
            if (AppContext.AuthenticationStatus != AuthenticationStatus.Anonymous)
                return Redirect(Url.Dashboard());

            return View();
        }

		[HttpGet, ImportViewData, GenerateBreadcrumbs(BreadcrumbId.Contact)]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost, ExportViewData]
        public ActionResult Contact(ContactUsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectWithViewModel(viewModel, "contact");
            }

            var emailInfo = new NonTemplatedEmailInfo
            {
                To = _configuration.AdministratorsEmail,
                Subject = "Errordite: Contact Us",
                Body =
                    @"Message from: '{0}'<br />
                        <!--Contact Reason: '{1}'<br />-->
                        Email Address: '{2}'<br /><br />
                        Message: {3}"
                    .FormatWith(viewModel.Name, viewModel.Reason, viewModel.Email, viewModel.Message)
            };

            Core.Session.AddCommitAction(new SendMessageCommitAction(emailInfo, _configuration.GetNotificationsQueueAddress()));
			Core.Session.AddCommitAction(new SendMessageCommitAction(new ContactUsEmailInfo
				{
					To = viewModel.Email,
				}, _configuration.GetNotificationsQueueAddress()));

            ConfirmationNotification(Resources.Home.MessageReceived);
            return RedirectToAction("contact");
        }
    }
}
