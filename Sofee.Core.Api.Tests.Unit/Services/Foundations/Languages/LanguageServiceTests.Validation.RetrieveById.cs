// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using Sofee.Core.Api.Models.Languages;
using Sofee.Core.Api.Models.Languages.Exceptions;
using Xunit;

namespace Sofee.Core.Api.Tests.Unit.Services.Foundations.Languages
{
    public partial class LanguageServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidLanguageId = Guid.Empty;

            var invalidLanguageException =
                new InvalidLanguageException();

            invalidLanguageException.AddData(
                key: nameof(Language.Id),
                values: "Id is required");

            var expectedLanguageValidationException = 
                new LanguageValidationException(invalidLanguageException);

            // when
            ValueTask<Language> retrieveLanguageByIdTask = 
                this.languageService.RetrieveLanguageByIdAsync(invalidLanguageId);

            // then
            await Assert.ThrowsAsync<LanguageValidationException>(() =>
                retrieveLanguageByIdTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLanguageValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectLanguageByIdAsync(It.IsAny<Guid>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls(); 
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionOnRetrieveByIdIfLanguageIsNotFoundLogItAsync()
        {
            // given
            Guid randomLanguageId = Guid.NewGuid();
            Language? noLanguage = null;

            var notFoundLanguageException =
                new NotFoundLanguageException(randomLanguageId);

            var expectedLanguageValidationException =
                new LanguageValidationException(notFoundLanguageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectLanguageByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(noLanguage);

            // when
            ValueTask<Language> retrieveLanguageByIdTask = 
                this.languageService.RetrieveLanguageByIdAsync(randomLanguageId);

            // then
            await Assert.ThrowsAsync<LanguageValidationException>(() =>
                retrieveLanguageByIdTask.AsTask());

            this.storageBrokerMock.Verify(broker => 
                broker.SelectLanguageByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLanguageValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
