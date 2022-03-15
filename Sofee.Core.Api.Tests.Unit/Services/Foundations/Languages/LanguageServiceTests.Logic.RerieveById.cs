// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sofee.Core.Api.Models.Languages;
using Xunit;

namespace Sofee.Core.Api.Tests.Unit.Services.Foundations.Languages
{
    public partial class LanguageServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveLanguageByIdAsync()
        {
            // given
            Guid randomLanguageId = Guid.NewGuid();
            Guid inputLanguageId = randomLanguageId;
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Language randomLanguage = CreateRandomLanguage(randomDateTime);
            Language storageLanguage = randomLanguage;
            Language expectedLanguage = storageLanguage.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectLanguageByIdAsync(inputLanguageId))
                    .ReturnsAsync(storageLanguage);

            // when
            Language actualLanguage =
                await this.languageService.RetrieveLanguageByIdAsync(inputLanguageId);

            //then
            actualLanguage.Should().BeEquivalentTo(expectedLanguage);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectLanguageByIdAsync(inputLanguageId),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
