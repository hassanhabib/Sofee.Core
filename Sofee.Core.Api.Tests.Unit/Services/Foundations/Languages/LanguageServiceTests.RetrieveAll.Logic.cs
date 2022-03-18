// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using System.Linq;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Sofee.Core.Api.Models.Languages;
using Sofee.Core.Api.Models.Languages.Exceptions;
using Xunit;

namespace Sofee.Core.Api.Tests.Unit.Services.Foundations.Languages
{
    public partial class LanguageServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfSqlErrorOccurredAndLogIt()
        {
            // given
            IQueryable<Language> someLangues = CreateRandomLanguages();
            SqlException sqlException = GetSqlException();

            var failedLanguageStoragexception =
                new FailedLanguageStorageException(sqlException);

            var expectedLanguageDependencyException =
                new LanguageDependencyException(failedLanguageStoragexception);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllLanguages())
                    .Throws(sqlException);

            // when 
            Action retrieveAllLanguagesAction = () =>
                this.languageService.RetrieveAllLanguages();

            // then
            Assert.Throws<LanguageDependencyException>(retrieveAllLanguagesAction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllLanguages(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(
                    SameExceptionAs(
                        expectedLanguageDependencyException))),
                            Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }

}
