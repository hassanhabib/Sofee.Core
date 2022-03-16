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
        public void ShouldReturnLanguages()
        {
            // given
            IQueryable<Language> randomLanguages = CreateRandomLanguages();
            IQueryable<Language> storageLanguages = randomLanguages;
            IQueryable<Language> expectedLanguages = randomLanguages.DeepClone();

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

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
