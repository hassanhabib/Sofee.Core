// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfProfileAlreadyExsitsAndLogItAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Language randomLanguage = CreateRandomLanguage(randomDateTime);
            Language alreadyExistsLanguage = randomLanguage;
            string randomMessage = GetRandomMessage();

            var duplicateKeyException =
               new DuplicateKeyException(randomMessage);

            var alreadyExistsLanguageException =
                new AlreadyExistsLanguageException(duplicateKeyException);

            var expectedLanguageDependencyValidationException =
                new LanguageDependencyValidationException(alreadyExistsLanguageException);

            this.dateTimeBrokerMock.Setup(broker =>
              broker.GetCurrentDateTimeOffset())
                  .Throws(duplicateKeyException);

            // when
            ValueTask<Language> addLanguageTask =
                this.languageService.AddLanguageAsync(alreadyExistsLanguage);

            // then
            await Assert.ThrowsAsync<LanguageDependencyValidationException>(() =>
                addLanguageTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertLanguageAsync(It.IsAny<Language>()),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLanguageDependencyValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowValidationExceptionOnAddIfReferenceErrorOccursAndLogItAsync()
        {
            // given 
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Language someLanguage = CreateRandomLanguage(randomDateTime);
            string randomMessage = GetRandomMessage();
            string exceptionMessage = randomMessage;

            var foreignKeyConstraintConflictException =
                new ForeignKeyConstraintConflictException(exceptionMessage);

            var invalidLangugageReferenceException =
                new InvalidLanguageReferenceException(foreignKeyConstraintConflictException);

            var expectedLanguageDependencyValidationException =
                new LanguageDependencyValidationException(invalidLangugageReferenceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(foreignKeyConstraintConflictException);

            // when
            ValueTask<Language> addLanguageTask =
                this.languageService.AddLanguageAsync(someLanguage);

            // then
            await Assert.ThrowsAsync<LanguageDependencyValidationException>(() =>
                addLanguageTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLanguageDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertLanguageAsync(someLanguage),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfDatabaseUpdateErrorOccursAndLogItAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Language someLanguage = CreateRandomLanguage(randomDateTime);

            var databaseUpdateException =
                new DbUpdateException();

            var failedLanguageStorageException =
                new FailedLanguageStorageException(databaseUpdateException);

            var expectedLanguageDependencyException =
                new LanguageDependencyException(failedLanguageStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(databaseUpdateException);

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
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLanguageDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertLanguageAsync(It.IsAny<Language>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given 
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Language someLanguage = CreateRandomLanguage(randomDateTime);
            var serviceException = new Exception();

            var failedLanguageServiceException =
                new FailedLanguageServiceException(serviceException);

            var expectedLanguageServiceException =
                new LanguageServiceException(failedLanguageServiceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(serviceException);

            // when
            ValueTask<Language> addLanguageTask =
                this.languageService.AddLanguageAsync(someLanguage);

            // then
            await Assert.ThrowsAsync<LanguageServiceException>(() =>
                addLanguageTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLanguageServiceException))),
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
