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
        public async Task ShouldThrowValidationExceptionOnAddIfLanguageIsNullAndLogItAsync()
        {
            //given 
            Language invalidLanguage = null;

            var nullLanguageException =
                    new NullLanguageException();

            var expectedLanguageValidationException =
                    new LanguageValidationException(nullLanguageException);

            //when
            ValueTask<Language> addLanguageTask =
                 this.languageService.AddLanguageAsync(invalidLanguage);

            //then
            await Assert.ThrowsAsync<LanguageValidationException>(() =>
                addLanguageTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                   broker.InsertLanguageAsync(invalidLanguage),
                       Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLanguageValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfLanguageIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given 
            var invalidLanguage = new Language
            {
                ISO = invalidText,
                Text = invalidText
            };

            var invalidLanguageException =
                new InvalidLanguageException();

            invalidLanguageException.AddData(
                key: nameof(Language.Id),
                values: "Id is required");

            invalidLanguageException.AddData(
                key: nameof(Language.ISO),
                values: "Text is required");

            invalidLanguageException.AddData(
                key: nameof(Language.Text),
                values: "Text is required");

            invalidLanguageException.AddData(
                key: nameof(Language.CreatedDate),
                values: "Date is required");

            invalidLanguageException.AddData(
                key: nameof(Language.CreatedBy),
                values: "Id is required");

            invalidLanguageException.AddData(
                key: nameof(Language.UpdatedDate),
                values: "Date is required");

            invalidLanguageException.AddData(
                key: nameof(Language.UpdatedBy),
                values: "Id is required");

            var expectedLanguageValidationException =
                new LanguageValidationException(invalidLanguageException);

            // when 
            ValueTask<Language> addLanguageTask =
                this.languageService.AddLanguageAsync(invalidLanguage);

            // then
            await Assert.ThrowsAsync<LanguageValidationException>(() =>
                addLanguageTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLanguageValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertLanguageAsync(It.IsAny<Language>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreateAndUpdateDatesIsNotSameAndLogItAsync()
        {
            // given
            int randomNumber = GetRandomNumber();
            DateTimeOffset randomDateTime = GetRandomDateTime();

            Language radomLanguage =
                CreateRandomLanguage(randomDateTime);

            Language invalidLanguage = radomLanguage;

            invalidLanguage.UpdatedDate =
                invalidLanguage.CreatedDate.AddDays(randomNumber);

            var invalidLanguageException =
                new InvalidLanguageException();

            invalidLanguageException.AddData(
                key: nameof(Language.UpdatedDate),
                values: $"Date is not the same as {nameof(Language.CreatedDate)}");

            var expectedLanguageValidationException =
                new LanguageValidationException(invalidLanguageException);

            // when
            ValueTask<Language> addLanguageTask =
                this.languageService.AddLanguageAsync(invalidLanguage);

            // then
            await Assert.ThrowsAsync<LanguageValidationException>(() =>
                addLanguageTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLanguageValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
               broker.InsertLanguageAsync(It.IsAny<Language>()),
                   Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(MinutesBeforeOrAfter))]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotRecentAndLogItAsync(
           int minutesBeforeOrAfter)
        {
            // given
            DateTimeOffset randomDateTime =
                GetRandomDateTime();

            DateTimeOffset invalidDateTime =
                randomDateTime.AddMinutes(minutesBeforeOrAfter);

            Language randomLanguage =
                CreateRandomLanguage(invalidDateTime);

            Language invalidLanguage = randomLanguage;

            var invalidLanguageException =
                new InvalidLanguageException();

            invalidLanguageException.AddData(
                key: nameof(Language.CreatedDate),
                values: "Date is not recent");

            var expectedLanguageValidationException =
                new LanguageValidationException(invalidLanguageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTime);

            // when
            ValueTask<Language> addLanguageTask =
                this.languageService.AddLanguageAsync(invalidLanguage);

            // then
            await Assert.ThrowsAsync<LanguageValidationException>(() =>
                addLanguageTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLanguageValidationException))),
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
