// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Sofee.Core.Api.Models.Countries;
using System.Threading.Tasks;

namespace Sofee.Core.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Country> InsertCounrtyAsync(Country country);
    }
}
