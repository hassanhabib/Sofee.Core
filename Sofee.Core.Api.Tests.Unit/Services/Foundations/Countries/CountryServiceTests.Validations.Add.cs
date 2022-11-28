// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using FluentAssertions;
using Moq;
using Sofee.Core.Api.Models.Countries;
using Sofee.Core.Api.Models.Countries.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Sofee.Core.Api.Tests.Unit.Services.Foundations.Countries
{
    public partial class CountryServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfInputIsNullAndLogItAsync()
        {
            //given
            Country noCountry = null;
            var nullCountryExceptions = new NullCountryException();

            var expectedCountryValidationException =
                new CountryValidationException(nullCountryExceptions);

            //when
            ValueTask<Country> addCountryTask =
                this.countryService.AddCountryAsync(noCountry);

            CountryValidationException actualCountryValidationException =
                await Assert.ThrowsAsync<CountryValidationException>(addCountryTask.AsTask);

            //then
            actualCountryValidationException.Should().BeEquivalentTo(
                expectedCountryValidationException);

            this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedCountryValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertCountryAsync(It.IsAny<Country>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
