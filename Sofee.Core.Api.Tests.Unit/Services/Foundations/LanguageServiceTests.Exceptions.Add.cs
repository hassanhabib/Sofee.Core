// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Moq;
using Sofee.Core.Api.Models.Languages;
using Sofee.Core.Api.Models.Languages.Exceptions;
using Xunit;

namespace Sofee.Core.Api.Tests.Unit.Services.Foundations
{
    public partial class LanguageServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
           // given
           DateTimeOffset randomDateTime = GetRandomDateTime();
           Language someLanguage = CreateRandomLanguage(randomDateTime);
           SqlException sqlException = GetSqlException();

            var failedLanguageStorageException =
                new FailedLanguageStorageException(sqlException);

            var expectedLanguageDependencyException =
                new LanguageDependencyException(failedLanguageStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(sqlException);

            // when
            ValueTask<Language> addLanguageTask =
                this.languageService.AddLanguageAsync(someLanguage);

            // then
            await Assert.ThrowsAsync<LanguageDependencyException>(() =>
                addLanguageTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedLanguageDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertLanguageAsync(It.IsAny<Language>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
