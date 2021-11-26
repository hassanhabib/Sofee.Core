// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Sofee.Core.Api.Infrastructure.Provision.Models.Configurations;

namespace Sofee.Core.Api.Infrastructure.Provision.Brokers.Configurations
{
    public interface IConfigurationBroker
    {
        CloudManagementConfiguration GetConfigurations();
    }
}
