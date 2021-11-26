// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Threading.Tasks;

namespace Sofee.Core.Api.Infrastructure.Provision.Services.Processings.CloudManagements
{
    public interface ICloudManagementProcessingService
    {
        ValueTask ProcessAsync();
    }
}
