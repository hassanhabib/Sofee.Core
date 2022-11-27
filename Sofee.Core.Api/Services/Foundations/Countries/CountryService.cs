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
    public partial class CountryService : ICountryService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public CountryService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Country> AddCountryAsync(Country country) =>
            TryCatch(async () =>
            {
            ValidateCountryNotNull(country);

            return await this.storageBroker.InsertCountryAsync(country);
            });
    }
}
