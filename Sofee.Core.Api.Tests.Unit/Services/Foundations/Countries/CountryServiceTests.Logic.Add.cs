// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using FluentAssertions;
using Moq;
using Sofee.Core.Api.Models.Countries;
using System.Threading.Tasks;
using Xunit;

namespace Sofee.Core.Api.Tests.Unit.Services.Foundations.Countries
{
    public partial class CountryServiceTests
    {
        [Fact]
        public async Task ShouldAddCountryAsync()
        {
            //given
            Country randomCountry = CreateRandomCountry();
            Country inputCountry = randomCountry;
            Country persistedCountry = inputCountry;
            Country expectedCountry = persistedCountry;

            this.storageBrokerMock.Setup(broker =>
             broker.InsertCountryAsync(inputCountry))
                .ReturnsAsync(persistedCountry);

            //when
            Country actualCountry = await this.countryService
                .AddCountryAsync(inputCountry);

            //then
            actualCountry.Should().BeEquivalentTo(expectedCountry);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertCountryAsync(inputCountry), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();

        }
    }
}
