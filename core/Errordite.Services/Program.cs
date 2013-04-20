﻿using System;
using System.Diagnostics;
using Errordite.Core.Configuration;
using Errordite.Core.Extensions;
using Errordite.Core.IoC;
using Errordite.Services.IoC;
using Magnum.CommandLineParser;
using Topshelf;

namespace Errordite.Services
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var service = ParseInstanceName(Environment.CommandLine);

                Trace.Write("Attempting to start Errordite.Services${0}".FormatWith(service));

                if (service == null)
                {
                    Trace.Write("Failed to load service configuration from container");
                    return;
                }

                var configuration = ObjectFactory.GetObject<ServiceConfiguration>(service.ToString());

                //indicates this is the active configuration for this service
                configuration.IsActive = true;

                Trace.Write("Loaded configuration, Service:={0}, Queue:={1}".FormatWith(
                    configuration.ServiceName,
                    configuration.QueueAddress));

                ObjectFactory.Container.Install(new ServicesMasterInstaller(service.Value));

                HostFactory.Run(c =>
                {
                    c.StartAutomatically();
                    c.SetServiceName(configuration.ServiceName);
                    c.SetDisplayName(configuration.ServiceDisplayName);
                    c.SetDescription(configuration.ServiceDiscription);
                    c.DependsOnEventLog();
                    c.UseLog4Net(@"config\log4net.config");

                    if (configuration.Username.IsNullOrEmpty())
                        c.RunAsPrompt();
                    else
                        c.RunAs(@"{0}\{1}".FormatWith(Environment.MachineName, configuration.Username), configuration.Password);

                    c.Service<IErrorditeService>(s =>
                    {
                        s.ConstructUsing(builder => ObjectFactory.GetObject<IErrorditeService>());
                        s.WhenStarted(svc => svc.Start());
                        s.WhenStopped(svc =>
                        {
                            svc.Stop();
                            ObjectFactory.Container.Dispose();
                        });
                    });
                });
            }
            catch (Exception e)
            {
                Trace.Write(e.ToString());
            }
        }

        /// <summary>
        ///   Parses the command line
        /// </summary>
        /// <param name="commandLine"> The command line to parse </param>
        /// <returns> The command line elements that were found </returns>
        private static Service? ParseInstanceName(string commandLine)
        {
            var parser = new StringCommandLineParser();
            var result = parser.All(commandLine);

            while (result != null)
            {
                var element = result.Value as DefinitionElement;

                if (element != null && element.Key.ToLowerInvariant() == "instance")
                {
                    return (Service)Enum.Parse(typeof(Service), element.Value, true);
                }

                result = parser.All(result.Rest);
            }

            return null;
        }
    }
}
