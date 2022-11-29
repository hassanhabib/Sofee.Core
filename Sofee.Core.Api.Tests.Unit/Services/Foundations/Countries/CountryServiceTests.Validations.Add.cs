// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using FluentAssertions;
using Moq;
using Sofee.Core.Api.Models.Countries;
using Sofee.Core.Api.Models.Countries.Exceptions;
using Sofee.Core.Api.Models.Languages.Exceptions;
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]

        public async Task ShouldThrowValidationExceptionOnAddIfCountryIsInvalidAndLogItAsync(
          string invalidString)
        {
            //given
            var invalidCountry = new Country
            {
                Name = invalidString,
                Iso3 = invalidString
            };

            var invalidCountryException = new InvalidCountryException();

            invalidCountryException.AddData(
                key: nameof(Country.Id),
                values: "Id is required");

            invalidCountryException.AddData(
                key: nameof(Country.Iso3),
                values: "Text is required");

            invalidCountryException.AddData(
                key: nameof(Country.Name),
                values: "Text is required");

            invalidCountryException.AddData(
                key: nameof(Country.CreatedDate),
                values: "Date is required");

            invalidCountryException.AddData(
                key: nameof(Country.UpdatedDate),
                values: "Date is required");

            invalidCountryException.AddData(
                key: nameof(Country.CreatedBy),
                values: "Id is required");

            invalidCountryException.AddData(
                key: nameof(Country.UpdatedBy),
                values: "Id is required");

            var expectedCountryValidationException =
                new CountryValidationException(invalidCountryException);

            //when
            ValueTask<Country> addCountryTask =
                this.countryService.AddCountryAsync(invalidCountry);

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
