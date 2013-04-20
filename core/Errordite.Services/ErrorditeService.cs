﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;
using Errordite.Core.Configuration;
using Errordite.Core.Extensions;
using Errordite.Core.IoC;
using Errordite.Core.Web;
using Errordite.Services.Queuing;
using log4net.Config;
using System.Linq;

namespace Errordite.Services
{
    public interface IErrorditeService
    {
        void Start();
        void Stop();
    }

    public class ErrorditeService : IErrorditeService
    {
        private readonly IQueueProcessor _queueProcessor;
        private readonly ServiceConfiguration _serviceConfiguration;

        public ErrorditeService(IEnumerable<ServiceConfiguration> serviceConfigurations, IQueueProcessor queueProcessor)
        {
            _serviceConfiguration = serviceConfigurations.First(c => c.IsActive);
            _queueProcessor = queueProcessor;
        }

        public void Start()
        {
            Configure();
            _queueProcessor.Start();
        }

        public void Stop()
        {
            _queueProcessor.Stop();
        }

        private void Configure()
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config\log4net.config")));

            var config = new HttpSelfHostConfiguration("http://localhost:{0}".FormatWith(_serviceConfiguration.PortNumber));

            config.Services.Replace(typeof(IHttpControllerActivator), new WindsorHttpControllerActivator());
            config.DependencyResolver = new WindsorDependencyResolver(ObjectFactory.Container);
            config.MaxReceivedMessageSize = 655360;
            config.MaxBufferSize = 655360;
            //this has the effect of always defaulting to Json serialization as there are no Xml formatters registered
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SerializerSettings = WebApiSettings.JsonSerializerSettings;

            config.Routes.MapHttpRoute(
                name: "issueapi",
                routeTemplate: "api/{orgid}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "admin",
                routeTemplate: "admin/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();

            Console.WriteLine("The server is running on endpoint http://localhost:{0}...".FormatWith(_serviceConfiguration.PortNumber));
        }
    }
}
