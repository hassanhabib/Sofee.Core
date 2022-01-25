using Sofee.Core.Api.Models.Languages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sofee.Core.Api.Services.Foundations.Languages
{
    public interface ILanguageService
    {
        ValueTask<Language> AddLanguageAsync(Language language);
        ValueTask<Language> RetrieveLanguageByIdAsync(Guid languageId);
        IQueryable<Language> RetrieveAllLanguages();
        ValueTask<Language> ModifyLanguageAsync(Language language);
        ValueTask<Language> RemoveLanguageByIdAsync(Guid languageId);
    }
}
