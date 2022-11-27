// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Sofee.Core.Api.Brokers.Loggings;
using Sofee.Core.Api.Brokers.Storages;
using Sofee.Core.Api.Models.Countries;
using System.Threading.Tasks;

namespace Sofee.Core.Api.Services.Foundations.Countries
{
    public class CountryService : ICountryService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public CountryService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Country> AddCountryAsync(Country country) =>
             await this.storageBroker.InsertCountryAsync(country);
    }
}
