// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Threading.Tasks;
using Sofee.Core.Api.Brokers.Storages;
using Sofee.Core.Api.Models.Languages;

namespace Sofee.Core.Api.Services.Foundations.Languages
{
    public partial class LanguageService : ILanguageService
    {
        private readonly IStorageBroker storageBroker;

        public LanguageService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public ValueTask<Language> AddLanguageAsync(Language language) =>
        TryCatch(async () =>
        {
            ValidateLanguageOnAdd(language);

            return await this.storageBroker.InsertLanguageAsync(language);
        });
    }
}
