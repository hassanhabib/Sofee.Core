// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using Moq;
using Sofee.Core.Api.Brokers.Loggings;
using Sofee.Core.Api.Brokers.Storages;
using Sofee.Core.Api.Models.Languages;
using Sofee.Core.Api.Services.Foundations.Languages;
using Tynamix.ObjectFiller;

namespace Sofee.Core.Api.Tests.Unit.Services.Foundations
{
    public partial class LanguageServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILanguageService languageService;

        public LanguageServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.languageService = new LanguageService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Language CreateRandomLanguage() =>
                CreateLanguageFiller().Create();

        private static Filler<Language> CreateLanguageFiller()
        {
            var filler = new Filler<Language>();
            Guid createdById = Guid.NewGuid();

            filler.Setup()
                .OnProperty(language => language.CreatedDate).Use(GetRandomDateTime())
                .OnProperty(language => language.UpdatedDate).IgnoreIt()
                .OnProperty(language => language.CreatedBy).Use(createdById)
                .OnProperty(language => language.UpdatedBy).IgnoreIt();

            return filler;
        }

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();
        private static int GetNegativeRandomNumber() => -1 * GetRandomNumber();
        private static string GetRandomMessage() => new MnemonicString().GetValue();
    }
}
