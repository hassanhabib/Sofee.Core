using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sofee.Core.Api.Models.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sofee.Core.Api.Tests.Unit.Services.Foundations.Languages
{
    public partial class LanguageServiceTests
    {
        [Fact]
        public async Task ShouldModifyLanguageAsync()
        {
            // given
            DateTimeOffset randomDate =
                GetRandomDateTimeOffset();

            Language randomLanguage =
                CreateRandomLanguage(randomDate);

            Language inputLanguage =
                randomLanguage;

            inputLanguage.UpdatedDate =
                randomDate.AddMinutes(1);

            Language storageLanguage =
                inputLanguage;

            Language updatedLanguage =
                inputLanguage;

            Language expectedLanguage =
                updatedLanguage.DeepClone();

            Guid inputLanguageId =
                inputLanguage.Id;

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDate);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectLanguageByIdAsync(
                    inputLanguageId))
                        .ReturnsAsync(storageLanguage);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateLanguageAsync(
                    inputLanguage))
                        .ReturnsAsync(updatedLanguage);

            // when
            Language actualLanguage =
                await this.languageService.
                    ModifyLanguageAsync(inputLanguage);

            // then
            actualLanguage.Should().BeEquivalentTo(
                expectedLanguage);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectLanguageByIdAsync(inputLanguageId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateLanguageAsync(inputLanguage),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}


