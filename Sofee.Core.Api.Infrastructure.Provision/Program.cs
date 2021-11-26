// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Threading.Tasks;
using Sofee.Core.Api.Infrastructure.Provision.Services.Processings.CloudManagements;

namespace Sofee.Core.Api.Infrastructure.Provision
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ICloudManagementProcessingService cloudManagementProcessingService =
                new CloudManagementProcessingService();

            await cloudManagementProcessingService.ProcessAsync();
        }
    }
}
