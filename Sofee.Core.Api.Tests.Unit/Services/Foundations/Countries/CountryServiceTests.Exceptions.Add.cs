// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using FluentAssertions;
using Microsoft.Data.SqlClient;
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
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Country someCountry = CreateRandomCountry();
            SqlException sqlException = CreateSqlException();

            var expectedfailedCountryStorageException =
                new FailedCountryStorageException(sqlException);

            var countryDependencyException =
                new CountryDependencyException(expectedfailedCountryStorageException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertCountryAsync(It.IsAny<Country>())).ThrowsAsync(sqlException);

            //when
            ValueTask<Country> addCountryTask = this.countryService.AddCountryAsync(someCountry);

            CountryDependencyException actualcountryDependencyException =
                await Assert.ThrowsAsync<CountryDependencyException>(addCountryTask.AsTask);

            //then
            actualcountryDependencyException.Should().BeEquivalentTo(expectedfailedCountryStorageException);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertCountryAsync(It.IsAny<Country>()), Times.Once);

            this.loggingBrokerMock.Verify(broker=>
            broker.LogCritical(It.Is(SameExceptionAs(
                expectedfailedCountryStorageException))),Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();

        }
    }
}
