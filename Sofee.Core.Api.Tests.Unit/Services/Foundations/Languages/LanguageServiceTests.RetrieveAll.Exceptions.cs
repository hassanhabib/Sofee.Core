// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Linq;
using FluentAssertions;
using Moq;
using Sofee.Core.Api.Models.Languages;
using Xunit;

namespace Sofee.Core.Api.Tests.Unit.Services.Foundations.Languages
{
    public partial class LanguageServiceTests
    {
        [Fact]
        public void ShouldReturnLanguages()
        {
            // given
            IQueryable<Language> randomLanguages = CreateRandomLanguages();
            IQueryable<Language> storageLanguages = randomLanguages;
            IQueryable<Language> expectedLanguages = randomLanguages;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllLanguages())
                    .Returns(storageLanguages);

            // when
            IQueryable<Language> actualLanguages =
                this.languageService.RetrieveAllLanguages();

            // then
            actualLanguages.Should().BeEquivalentTo(expectedLanguages);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllLanguages(), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
