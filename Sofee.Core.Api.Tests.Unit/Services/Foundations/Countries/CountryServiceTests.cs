// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Moq;
using Sofee.Core.Api.Brokers.Loggings;
using Sofee.Core.Api.Brokers.Storages;
using Sofee.Core.Api.Models.Countries;
using Sofee.Core.Api.Services.Foundations.Countries;
using System;
using Tynamix.ObjectFiller;

namespace Sofee.Core.Api.Tests.Unit.Services.Foundations.Countries
{
    public partial class CountryServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICountryService countryService;

        public CountryServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.countryService = new CountryService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static DateTimeOffset GetRandomDateTime() =>
           new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static Country CreateRandomCountry() =>
           CreateCountryFiller().Create();

        private static Filler<Country> CreateCountryFiller()
        {
            var filler = new Filler<Country>();
            DateTimeOffset dates = GetRandomDateTime();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates);

            return filler;
        }
    }
}
