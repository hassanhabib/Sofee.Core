using Sofee.Core.Api.Brokers.Storages;
using Sofee.Core.Api.Models.Languages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sofee.Core.Api.Services.Foundations.Languages
{
    public class LanguageService : ILanguageService
    {
        private readonly IStorageBroker storage;

        public LanguageService(IStorageBroker storage)
        {
            this.storage = storage;
        }

        public ValueTask<Language> AddLanguageAsync(Language language)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Language> ModifyLanguageAsync(Language language)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Language> RemoveLanguageByIdAsync(Guid languageId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Language> RetrieveAllLanguages()
        {
            throw new NotImplementedException();
        }

        public ValueTask<Language> RetrieveLanguageByIdAsync(Guid languageId)
        {
            throw new NotImplementedException();
        }
    }
}
