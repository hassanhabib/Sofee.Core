// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Threading.Tasks;
using Sofee.Core.Api.Brokers.Storages;
using Sofee.Core.Api.Models.Languages;

namespace Sofee.Core.Api.Services.Foundations.Languages
{
    public class LanguageService : ILanguageService
    {
        private readonly IStorageBroker storageBroker;

        public LanguageService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Language> AddLanguageAsync(Language language) =>
            await this.storageBroker.InsertLanguageAsync(language);
    }
}
