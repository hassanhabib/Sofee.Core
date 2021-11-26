// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.IO;
using Microsoft.Extensions.Configuration;
using Sofee.Core.Api.Infrastructure.Provision.Models.Configurations;

namespace Sofee.Core.Api.Infrastructure.Provision.Brokers.Configurations
{
    public class ConfigurationBroker : IConfigurationBroker
    {
        public CloudManagementConfiguration GetConfigurations()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(basePath: Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appSettings.json", optional: false)
                .Build();

            return configurationRoot.Get<CloudManagementConfiguration>();
        }
    }
}
